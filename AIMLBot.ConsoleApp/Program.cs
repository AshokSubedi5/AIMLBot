using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLBot.Business;


namespace AIMLBot.ConsoleApp
{
    class Program
    {
        static AIMLBLL bot;
        static void Main(string[] args)
        {
            string input = "start";
            bot = new AIMLBLL();
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
