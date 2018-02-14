using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using ChillBot.ApiData;
using ChillBot.Services;

namespace ChillBot
{
    public class Bot
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        private BotSecrets botSecrets;
        private GiphyService giphy;

        public async Task StartupAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            botSecrets = await BotSecrets.LoadFromFile();
            giphy = new GiphyService(botSecrets.Giphy); 
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .AddSingleton(giphy)
                .AddSingleton<RandomNumberService>()
                .AddSingleton<FailMessageService>()
                .BuildServiceProvider();

            await InstallCommandsAsync();

            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, botSecrets.Discord.DevelopmentToken);
            await client.StartAsync();

            // Task doesn't return until program exit
            await Task.Delay(-1);
        }

        private async Task InstallCommandsAsync()
        {
            // Add command modules from this assembly
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Ignore messages from non-human users
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            // Execute the command.
            var context = new SocketCommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
