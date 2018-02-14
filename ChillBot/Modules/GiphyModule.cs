using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ChillBot.Services;
using ChillBot.Models;

namespace ChillBot.Modules
{
    [Group("giphy")]
    [Alias("gif")]
    public class GiphyModule : ModuleBase<SocketCommandContext>
    {
        private GiphyService giphy;

        public GiphyModule(GiphyService giphy)
        {
            this.giphy = giphy;
        }

        #region
        [Command("random")]
        [Alias("rand", "r")]
        [Summary("Gets a random gif from Giphy")]
        public async Task RandomAsync()
        {
            GiphyImage img = await giphy.GetRandomImageAsync();
            await ReplyAsync(img.Image_Url);
        }
        #endregion
    }
}
