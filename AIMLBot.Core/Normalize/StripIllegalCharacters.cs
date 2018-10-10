using System;
using System.Text.RegularExpressions;
using System.Text;

namespace AIMLBot.Core.Normalize
{
    /// <summary>
    /// Strips any illegal characters found within the input string. Illegal characters are referenced from
    /// the bot's Strippers regex that is defined in the setup XML file.
    /// </summary>
    public class StripIllegalCharacters : AIMLBot.Core.Utils.TextTransformer
    {
        public StripIllegalCharacters(AIMLBot.Core.Bot bot, string inputString) : base(bot, inputString)
        { }

        public StripIllegalCharacters(AIMLBot.Core.Bot bot)
            : base(bot) 
        { }

        protected override string ProcessChange()
        {
            return this.bot.Strippers.Replace(this.inputString, " ");
        }
    }
}
