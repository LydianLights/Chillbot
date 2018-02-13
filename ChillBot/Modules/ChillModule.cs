using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ChillBot.Services;

namespace ChillBot.Modules
{
    public class ChillModule : ModuleBase<SocketCommandContext>
    {
        private RandomNumberService randService;

        public ChillModule(RandomNumberService randService)
        {
            this.randService = randService;
        }

        #region Echo
        [Command("echo")]
        [Alias("say")]
        [Summary("Echos the given message.")]
        public async Task SayAsync
        (
            [Remainder, Summary("The message to echo.")] string message
        )
        {
            await ReplyAsync(message);
        }
        #endregion

        #region Greet
        private string[] greetings =
        {
            "Hello, {0}.",
            "Hello {0}!",
            "Hi {0}!",
            "Hi there, {0}!",
            "Hiya {0}!",
            "Hey {0}!",
            "Hey there {0}!",
            "Ohai {0}",
            "Ohai thar {0}",
            "Heyo {0}",
            "Hullo, {0}!",
            "Hullo thar {0}!",
            "How's it going, {0}?",
            "How goes it, {0}?",
            "Hail {0}!!",
            "Greetings, {0}."
        };

        [Command("hello")]
        [Alias("hi", "hey")]
        [Summary("Greets the command user.")]
        public async Task GreetAsync()
        {
            string randomGreeting = greetings[randService.RNG.Next(greetings.Length)];
            string response = String.Format(randomGreeting, Context.User.Mention);
            await ReplyAsync(response);
        }
        #endregion
    }
}
