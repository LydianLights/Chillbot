using System;
using System.Collections.Generic;
using System.Text;
using ChillBot.ApiData;

namespace ChillBot.Services
{
    public class GiphyService
    {
        private GiphySecrets secrets;

        public GiphyService(GiphySecrets secrets)
        {
            this.secrets = secrets;
        }
    }
}
