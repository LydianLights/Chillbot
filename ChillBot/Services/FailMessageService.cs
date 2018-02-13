using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;

namespace ChillBot.Services
{
    public class FailMessageService
    {
        private Random rng = new Random();
        private Queue<int> lastMessageIndices = new Queue<int>();
        private static string[] failMessages =
        {
            "pls no",
            "wut",
            "uhhhhhhh",
            "lol wut",
            ":shrug:",
            ":no_good: :no_good: :no_good:",
            ":thinking: but how??",
            "pls",
            "ummm no",
            "hahaha no",
            "lol no",
            "*confused dog sounds*",
            "wat",
            "i dunno how lol",
            "how do??",
            "pls send help",
            "k",
            "FUUUUUUUUUUUUUUUUUUUUUU",
            "halp pls",
            "I am incapable of performing that action. Please try again.",
            "but why?",
            "y tho?",
            "ORLY?",
            "beep boop DENIED",
            "but mooooommmm",
            "but daaaaaaddddd",
            "but has anyone ever really been as far as to do that?",
            "brb one sec",
            "ok",
            "aaaaaaaaaaaaaa"
        };

        public FailMessageService() { }

        // TODO: Make data structure that does this and use it for greetings
        public string GetRandomFail()
        {
            int index;
            int rerollCount = 0;
            do
            {
                rerollCount++;
                index = rng.Next(failMessages.Length);
            } while (lastMessageIndices.Contains(index) && rerollCount < 50);

            lastMessageIndices.Enqueue(index);
            if (lastMessageIndices.Count > failMessages.Length / 2)
            {
                lastMessageIndices.Dequeue();
            }

            return failMessages[index];
        }
        public string GetRandomFail(SocketUser user)
        {
            return user.Mention + " " + GetRandomFail();
        }
    }
}
