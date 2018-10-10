using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIMLBot.ConsoleApp
{
    class Program
    {
        static AIML bot;
        static void Main(string[] args)
        {
            string input = "start";
            bot = new AIML();
            while (input != "exit")
            {
                input = bot.getInput();
                var output = bot.getOutput(input);               
                Console.WriteLine("Bot: " + output);

                //save userInfo
                bot.SaveUserInfo();
            }
        }
    }
}
