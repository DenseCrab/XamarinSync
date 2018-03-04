using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SyncExample.Views;
using SyncExample.SQLite.DTOs.Implementations;
using SyncExample.SQLite;
using SyncExample.SQLite.DTOs;
using Plugin.Connectivity;
using SyncExample.API.Requests;
using SyncExample.API.Requests.Implementations;
using SyncExample.API;

namespace SyncExample.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<IMessageDTO> Messages { get; set; }
        public Command LoadMessagesCommand { get; set; }
        private readonly ILocalDatabaseContext _localDatabaseContext;
        private readonly IAPIContext _apiContext;

        public ItemsViewModel(ILocalDatabaseContext localDatabaseContext, IAPIContext apiContext)
        {
            _localDatabaseContext = localDatabaseContext;
            _apiContext = apiContext;
            Title = "Browse";
            Messages = new ObservableCollection<IMessageDTO>();
            LoadMessagesCommand = new Command(async () => await ExecuteLoadMessagesCommand());

            MessagingCenter.Subscribe<NewItemPage, MessageDTO>(this, "AddItem", async (obj, message) =>
            {
                Messages.Add(message);

                //TODO: STEP 1. Store to Realm as "Unsynced"
                message.SyncedOn = null;
                message.CreatedBy = "APP";
                message.CreatedOn = DateTime.UtcNow;
                _localDatabaseContext.MessageRepository.Insert(message);

                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        ISaveMessageRequest request = new SaveMessageRequest();
                        //populate request object parameters
                        request.ClientId = message.Id;
                        request.Text = message.Text;
                        request.Description = message.Description;

                        var response = await _apiContext.SaveMessage(request);
                        if (response != null && response.Success == true)
                        {
                            //TODO: STEP 3. ONLY IF STEP 2 was successfull - Update Realm as "Synced"
                            if (response.ClientId == message.Id)
                            {
                                message.ApiId = response.ApiId;
                                message.SyncedOn = DateTime.UtcNow;
                                message.UpdatedOn = DateTime.UtcNow;
                                message.UpdatedBy = "APP";
                                _localDatabaseContext.MessageRepository.Save(message);
                            }
                        }
                        else
                        {
                            //_messenger.ShowMessage(_localizeResources.GetResource(Constants.Localization.Error), _localizeResources.GetResource(Constants.Localization.Error), _localizeResources.GetResource(Constants.Localization.ErrorPositiveButton));
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: hockey app code can go here...				                
                        //_messenger.ShowMessage(_localizeResources.GetResource(Constants.Localization.Error), _localizeResources.GetResource(Constants.Localization.ErrorGenericError), _localizeResources.GetResource(Constants.Localization.ErrorPositiveButton));
                    }
                }
            });
        }

        async Task ExecuteLoadMessagesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Messages.Clear();
                var messages = _localDatabaseContext.MessageRepository.Items;
                foreach (var msg in messages)
                {
                    Messages.Add(msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}