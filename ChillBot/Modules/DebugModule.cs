using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ChillBot.Services;

namespace ChillBot.Modules
{
    [Group("debug")]
    public class DebugModule : ModuleBase<SocketCommandContext>
    {
        #region Info
        [Command("info")]
        [Summary("Shows some debug info.")]
        public async Task InfoAsync()
        {
            string m =
                "```" +
                $"Name: {Context.Client.CurrentUser.Username}\n" +
                $"ID: {Context.Client.CurrentUser.Id}\n" +
                $"Shard: {Context.Client.ShardId}\n" +
                $"Status: {Context.Client.ConnectionState}\n" +
                $"Latency: {Context.Client.Latency}\n" +
                $"Current Server: {Context.Guild?.Name}\n" +
                $"Server ID: {Context.Guild?.Id}\n" +
                $"Server Owner: {Context.Guild?.Owner.Username}\n" +
                $"Current Channel: {Context.Channel.Name}\n" +
                $"Channel ID: {Context.Channel.Id}\n" +
                $"Private Channel: {Context.IsPrivate}" +
                "```";
            await ReplyAsync(m);
        }
        #endregion
    }
}
