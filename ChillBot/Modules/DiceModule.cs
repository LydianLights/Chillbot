using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ChillBot.Services;

namespace ChillBot.Modules
{
    public class DiceModule : ModuleBase<SocketCommandContext>
    {
        private RandomNumberService randService;
        private FailMessageService failMessages;

        public DiceModule(RandomNumberService randService, FailMessageService failMessages)
        {
            this.randService = randService;
            this.failMessages = failMessages;
        }

        #region Roll
        [Command("roll")]
        [Alias("dice")]
        [Summary("Rolls a dice. Supports standard d20 notation.")]
        public async Task RollAsync
        (
            [Remainder, Summary("d20 dice expression")] string diceExpression
        )
        {
            if (randService.TryParseD20Expression(diceExpression, out int diceResult, out int[][] diceRolls))
            {
                string[] splitByWhitespace = diceExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                StringBuilder whitespaceRemoved = new StringBuilder();
                foreach (string str in splitByWhitespace)
                {
                    whitespaceRemoved.Append(str);
                }
                diceExpression = whitespaceRemoved.ToString();
                StringBuilder reply = new StringBuilder($"{Context.User.Mention} rolled {diceExpression} => {diceResult.ToString()}\n");
                for (int i = 0; i < diceRolls.Length; i++)
                {
                    reply.Append("{ ");
                    for (int j = 0; j < diceRolls[i].Length; j++)
                    {
                        reply.Append(diceRolls[i][j]);
                        if (j < diceRolls[i].Length - 1)
                        {
                            reply.Append(", ");
                        }
                    }
                    reply.Append(" }");
                    if (i < diceRolls.Length - 1)
                    {
                        reply.Append("\n");
                    }
                }
                await ReplyAsync(reply.ToString());
            }
            else
            {
                await ReplyAsync(failMessages.GetRandomFail(Context.User));
            }
        }

        [Command("roll")]
        [Alias("dice")]
        [Summary("Rolls a dice. Supports standard d20 notation.")]
        public async Task RollAsync()
        {
            await RollAsync("d20");
        }
        #endregion
    }
}
