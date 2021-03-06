﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ChillBot.ApiData;
using ChillBot.Models;

namespace ChillBot.Services
{
    public class GiphyService
    {
        private const string apiUrl = "https://api.giphy.com/v1";
        private GiphySecrets secrets;

        public GiphyService(GiphySecrets secrets)
        {
            this.secrets = secrets;
        }

        public async Task<GiphyImage> GetRandomImageAsync()
        {
            return await GetImage(null);
        }

        public async Task<GiphyImage> GetImage(string tag)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest("/gifs/random", Method.GET);
            request.AddParameter("api_key", secrets.ApiKey);
            if (tag != null)
            {
                request.AddParameter("tag", tag);
            }
            request.AddParameter("rating", "PG-13");
            var response = await client.ExecuteTaskAsync(request) as RestResponse;
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            GiphyImage result = JsonConvert.DeserializeObject<GiphyImage>(jsonResponse["data"].ToString());
            return result;
        }
    }
}
