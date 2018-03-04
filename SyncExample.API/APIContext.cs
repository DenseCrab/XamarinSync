using Newtonsoft.Json;
using SyncExample.API.Requests;
using SyncExample.API.Responses;
using SyncExample.API.Responses.Implementations;
using SyncExample.SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyncExample.API
{
    public class APIContext : IAPIContext
    {
        readonly string _endpoint;
        private ILocalDatabaseContext _localDatabase;

        public APIContext(ILocalDatabaseContext localDatabase)
        {
            _endpoint = Constants.APIEndpoint;
            _localDatabase = localDatabase;
        }

        public async Task<IGetUnsyncedMessagesResponse> GetUnsyncedMessages(IGetUnsyncedMessagesRequest request)
        {
            GetUnsyncedMessagesResponse response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(request);

                    client.BaseAddress = new Uri(_endpoint);

                    HttpResponseMessage result = await client.PostAsync(Constants.GetUnsyncedMessages, new StringContent(json, Encoding.UTF8));
                    response = JsonConvert.DeserializeObject<GetUnsyncedMessagesResponse>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception e)
            {
                response = new GetUnsyncedMessagesResponse();
                response.Exception = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ISaveMessagesResponse> SaveMessages(ISaveMessagesRequest request)
        {
            ISaveMessagesResponse response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(request);

                    client.BaseAddress = new Uri(_endpoint);
                    HttpResponseMessage result = await client.PostAsync(Constants.SaveMessages, new StringContent(json, Encoding.UTF8));
                    response = JsonConvert.DeserializeObject<ISaveMessagesResponse>(await result.Content.ReadAsStringAsync());
                }

            }
            catch (Exception e)
            {
                response = new SaveMessagesResponse();
                response.Exception = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ISaveMessageResponse> SaveMessage(ISaveMessageRequest request)
        {
            ISaveMessageResponse response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(request);

                    client.BaseAddress = new Uri(_endpoint);
                    HttpResponseMessage result = await client.PostAsync(Constants.SaveMessage, new StringContent(json, Encoding.UTF8));
                    response = JsonConvert.DeserializeObject<ISaveMessageResponse>(await result.Content.ReadAsStringAsync());
                }

            }
            catch (Exception e)
            {
                response = new SaveMessageResponse();
                response.Exception = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}