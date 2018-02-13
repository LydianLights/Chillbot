using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ChillBot.Services
{
    // TODO: Make a class to keep track of dice roll expressions to clean up code and make handling display to user easier
    public class RandomNumberService
    {
        private static Random initRNG = new Random();
        private static int seed = initRNG.Next();

        private const string d20Term = @"([+-]?)(\d*)(d?)(\d+)";
        private static Regex d20TermRegex = new Regex(d20Term, RegexOptions.IgnoreCase);
        private static Regex d20ExpressionRegex = new Regex($@"^(?:{d20Term})+$", RegexOptions.IgnoreCase);

        public Random RNG { get; private set; }

        public RandomNumberService()
        {
            // Just for fun
            seed++;
            RNG = new Random(seed);
        }

        // Returns false if invalid expression
        // Evaluated expression stored in result
        // List of dice rolls stored in results
        // out vars are undefined if parse fails
        public bool TryParseD20Expression(string diceExpression, out int result, out int[][] rolls)
        {
            string[] splitByWhitespace = diceExpression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder whitespaceRemoved = new StringBuilder();
            foreach (string str in splitByWhitespace)
            {
                whitespaceRemoved.Append(str);
            }
            diceExpression = whitespaceRemoved.ToString();
            result = 0;
            rolls = new int[][] { };
            if (d20ExpressionRegex.IsMatch(diceExpression))
            {
                // Capture groups of regex terms:
                // [0] = entire term
                // [1] = +/- sign
                // [2] = number of dice
                // [3] = 'd'
                // [4] = size of dice
                MatchCollection matches = d20TermRegex.Matches(diceExpression);
                rolls = new int[matches.Count][];
                for (int termIndex = 0; termIndex < matches.Count; termIndex++)
                {
                    int termSum = 0;
                    bool isNegative = (matches[termIndex].Groups[1].Value == "-");
                    bool isConstant = (matches[termIndex].Groups[3].Value != "d");
                    if (isConstant)
                    {
                        bool constParsed = Int32.TryParse(matches[termIndex].Groups[4].Value, out termSum);
                        if (!constParsed) { return false; }
                        rolls[termIndex] = new int[] { termSum };
                    }
                    else
                    {
                        int numDice;
                        int diceType;
                        if (matches[termIndex].Groups[2].Value == "")
                        {
                            numDice = 1;
                        }
                        else
                        {
                            bool numParsed = Int32.TryParse(matches[termIndex].Groups[2].Value, out numDice);
                            if (!numParsed) { return false; }
                        }
                        bool typeParsed = Int32.TryParse(matches[termIndex].Groups[4].Value, out diceType);
                        if (!typeParsed || diceType == 0) { return false; }

                        rolls[termIndex] = new int[numDice];
                        for (int rollIndex = 0; rollIndex < numDice; rollIndex++)
                        {
                            int roll = RNG.Next(diceType) + 1;
                            rolls[termIndex][rollIndex] = roll;
                            termSum += roll;
                        }
                    }
                    result += isNegative ? -termSum : termSum;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryParseD20Expression(string diceExpression, out int result)
        {
            return TryParseD20Expression(diceExpression, out result, out int[][] rolls);
        }
    }
}
