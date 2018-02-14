using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ChillBot.ApiData
{
    public class BotSecrets
    {
        private const string apiSecretsFilePath = @"bot-secrets.json";
        [JsonRequired]
        public DiscordSecrets Discord { get; private set; }
        public GiphySecrets Giphy { get; private set; }

        [JsonConstructor]
        private BotSecrets(DiscordSecrets discord, GiphySecrets giphy)
        {
            Discord = discord;
            Giphy = giphy;
        }

        public static async Task<BotSecrets> LoadFromFile()
        {
            string fileContents;
            try
            {
                fileContents = await File.ReadAllTextAsync(apiSecretsFilePath);
            }
            catch (Exception e)
            {
                string m = $"{apiSecretsFilePath} could not be found. It is required to connect to the Discord API. See documentation for details.";
                throw new FileNotFoundException(m, apiSecretsFilePath, e);
            }
            try
            {
                return JsonConvert.DeserializeObject<BotSecrets>(fileContents);
            }
            catch (Exception e)
            {
                string m = $"An error occured while reading {apiSecretsFilePath}. There may be a syntax error or missing required key. See documentation for details.";
                throw new FileLoadException(m, apiSecretsFilePath, e);
            }
        }
    }
}
