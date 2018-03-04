using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using SyncExample.API;
using SyncExample.API.Models;
using SyncExample.API.Models.Implementations;
using SyncExample.API.Requests.Implementations;
using SyncExample.SQLite;
using SyncExample.SQLite.DTOs.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncExample
{
    public sealed class Syncing
    {
        public bool IsConnected;
        private readonly ILocalDatabaseContext _localDatabaseContext;
        private readonly IAPIContext _apiContext;
        private static string UPDATED_BY_SYNCDATA = "SyncData";

        public Syncing(ILocalDatabaseContext localDatabaseContext, IAPIContext apiContext)
        {
            _localDatabaseContext = localDatabaseContext;
            _apiContext = apiContext;
            IsConnected = CrossConnectivity.Current.IsConnected;
            CrossConnectivity.Current.ConnectivityChanged += ConnectionChanged;
        }

        public void ConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (e.IsConnected)
            {
                Task.Factory.StartNew(async () => { await SyncData(); }); //Start syncing process if connected
            }
        }

        public async Task SyncData()
        {
            bool realmUpdated = false;
            if (IsConnected)
            {
                DateTime? lastSyncedOn = _localDatabaseContext.UserRepository.GetLastSynced();
                DateTime lastSyncedDate = lastSyncedOn.HasValue ? lastSyncedOn.Value : DateTime.UtcNow.AddYears(-1);

                //GET UNSYNCED MESSAGES FROM API (where CreatedOn > lastSyncedOn)
                GetUnsyncedMessagesRequest request = new GetUnsyncedMessagesRequest();
                request.StartDate = DateTime.SpecifyKind(lastSyncedDate, DateTimeKind.Utc);
                var getUnsyncedMessagesResponse = await _apiContext.GetUnsyncedMessages(request);

                if (getUnsyncedMessagesResponse != null && getUnsyncedMessagesResponse.Success)
                {
                    foreach (var message in getUnsyncedMessagesResponse.MessageList)
                    {
                        //Create new SQLite entry
                        var messageDTO = new MessageDTO();

                        if (!message.ClientId.HasValue) //IF clientId = null we must search for a record in SQLite with the ApiId 
                        {
                            //record was created on Website and NOT using MobileApp - Look for record with APIPrimaryKey
                            var messageFoundByAPIPK = _localDatabaseContext.MessageRepository.GetByAPIPrimaryKey(message.Id);
                            if (messageFoundByAPIPK != null)
                            {
                                messageDTO.Id = messageFoundByAPIPK.Id; //Set Id                             
                            }
                            else
                            {
                                //record by APIPrimaryKey NOT FOUND - add it into SQLite as Brand New Record
                                messageDTO.Id = 0;
                                messageDTO.CreatedBy = UPDATED_BY_SYNCDATA;
                                messageDTO.CreatedOn = DateTime.UtcNow;
                            }
                        }
                        else //ClientId IS POPULATED 
                        {
                            //ClientId is populated - update that same record that we have in our SQLite DB
                            var messageFoundByClientId = _localDatabaseContext.MessageRepository.GetByClientId(message.ClientId.Value);
                            if (messageFoundByClientId != null)
                            {
                                //found-record by ClientId
                                messageDTO.Id = messageFoundByClientId.Id; //MobileApp PrimaryKey
                            }
                            else
                            {   //this scenario would happen if:
                                //1 - user had app on phone and created tracker entries and syncned the to API and then removed app and re-installed app on same phone
                                //2 - user had app on phone and created tracker entries and syncned the to API and then phone broke and user got new phone and installed app on new phone
                                messageDTO.Id = message.ClientId.Value; //MobileApp PrimaryKey ClientId
                                messageDTO.CreatedBy = UPDATED_BY_SYNCDATA;
                                messageDTO.CreatedOn = DateTime.UtcNow;
                            }
                        }

                        messageDTO.ApiId = message.Id; //API PrimaryKey
                        messageDTO.Text = message.Text;
                        messageDTO.Description = message.Description;
                        messageDTO.SyncedOn = DateTime.UtcNow;
                        messageDTO.UpdatedBy = UPDATED_BY_SYNCDATA;
                        messageDTO.UpdatedOn = DateTime.UtcNow;

                        if (messageDTO.Id == 0)
                            _localDatabaseContext.MessageRepository.Insert(messageDTO);
                        else
                            _localDatabaseContext.MessageRepository.Save(messageDTO);

                        realmUpdated = true;

                    }

                    if (realmUpdated)
                    {
                        lastSyncedOn = DateTime.UtcNow;

                        bool sucess = _localDatabaseContext.UserRepository.UpdateLastSyncedOn(lastSyncedOn.Value);
                        if (!sucess)
                        {
                            //_messenger.ShowMessage(_localizeResources.GetResource(Constants.Localization.ErrorLocalStorage), _localizeResources.GetResource(Constants.Localization.ErrorLocalStorageNotSaved), _localizeResources.GetResource(Constants.Localization.ErrorPositiveButton));
                        }
                    }

                }

                //STEP 3. QUERY  - "Unsynced" data from Realm  
                //STEP 4 -A PUSH   - "Unsynced" data to API               
                SaveMessagesRequest requestUnsynced = new SaveMessagesRequest();

                //get Unsynced Injection Trackers
                var unsyncedMessages = _localDatabaseContext.MessageRepository.GetUnsyncedItems;
                requestUnsynced.MessageDtos = new List<IMessageModel>();

                foreach (var unsyncedMsg in unsyncedMessages)
                {
                    MessageModel messageModel = new MessageModel();

                    messageModel.Text = unsyncedMsg.Text;
                    messageModel.Description = unsyncedMsg.Description;
                    messageModel.ClientId = unsyncedMsg.Id; ;

                    requestUnsynced.MessageDtos.Add(messageModel);
                }



                var unsyncedRecordsToSendToAPI = false;

                if (requestUnsynced.MessageDtos.Count > 0)
                {
                    unsyncedRecordsToSendToAPI = true;
                }

                //MAKE API CALL TO SAVE ALL UNSYNCED DATA
                //STEP 4 -B PUSH   - "Unsynced" data to API 

                if (unsyncedRecordsToSendToAPI)
                {
                    var response = await _apiContext.SaveMessages(requestUnsynced);

                    if (response != null && response.Success)
                    {
                        //5. UPDATE - Realm with "Synced"

                        for (int i = 0; i < response.MessageList.Count; i++)
                        {
                            var apiMessage = response.MessageList[i];

                            foreach (var recordToSetSyncedOn in unsyncedMessages)
                            {
                                if (recordToSetSyncedOn.Id == apiMessage.ClientId)
                                {
                                    recordToSetSyncedOn.ApiId = apiMessage.ApiId;
                                    recordToSetSyncedOn.SyncedOn = DateTime.UtcNow;
                                    recordToSetSyncedOn.UpdatedOn = DateTime.UtcNow;
                                    _localDatabaseContext.MessageRepository.Save(recordToSetSyncedOn);
                                    break;
                                }
                            }
                        }

                        //6. UPDATE - SynceDate should be UTC NOW
                        bool savedSyncedOn = _localDatabaseContext.UserRepository.UpdateLastSyncedOn(DateTime.UtcNow);

                        if (!savedSyncedOn)
                        {
                            //_messenger.ShowMessage(_localizeResources.GetResource(Constants.Localization.ErrorLocalStorage), _localizeResources.GetResource(Constants.Localization.ErrorLocalStorageNotSaved), _localizeResources.GetResource(Constants.Localization.ErrorPositiveButton));
                        }

                    }
                    else
                    {
                        //_messenger.ShowMessage(_localizeResources.GetResource(Constants.Localization.Error), _localizeResources.GetResource(Constants.Localization.Error) + " SyncingData-SaveAllTrackingData.Sucess=FALSE- " + response.Exception.ToString(), _localizeResources.GetResource(Constants.Localization.ErrorPositiveButton));
                    }

                }
            }
        }

    }
}