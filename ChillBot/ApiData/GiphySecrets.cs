using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ChillBot.ApiData
{
    public class GiphySecrets
    {
        public string ApiKey { get; private set; }

        [JsonConstructor]
        public GiphySecrets(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
