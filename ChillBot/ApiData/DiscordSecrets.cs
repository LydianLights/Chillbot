using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ChillBot.ApiData
{
    public class DiscordSecrets
    {
        [JsonRequired]
        public string ProductionToken { get; private set; }
        [JsonRequired]
        public string DevelopmentToken { get; private set; }

        [JsonConstructor]
        public DiscordSecrets(string productionToken, string developmentToken)
        {
            ProductionToken = productionToken;
            DevelopmentToken = developmentToken;
        }
    }
}
