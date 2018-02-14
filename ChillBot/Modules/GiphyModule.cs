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

        #region Random
        [Command]
        [Summary("Gets a random gif from Giphy.")]
        public async Task RandomAsync()
        {
            GiphyImage img = await giphy.GetRandomImageAsync();
            await ReplyAsync(img.Image_Url);
        }
        #endregion

        #region RandomByTag
        [Command]
        [Summary("Gets a gif from Giphy using the provided tag.")]
        public async Task RandomByTagAsync
        (
            [Remainder, Summary("The tag to search for.")] string query
        )
        {
            GiphyImage img = await giphy.GetImage(query);
            await ReplyAsync(img.Image_Url);
        }
        #endregion
    }
}
