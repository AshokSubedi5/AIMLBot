using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AIMLBot.Core;

namespace AIMLBot.ConsoleApp
{
    public class AIML
    {
        const string UserId = "guest";
        private Bot AimlBot;
        private User myUser;       


        public AIML()
        {
            Console.Title = "AIML Bot";
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            AimlBot = new Bot();           
            myUser = new User(UserId, AimlBot);           
        }

        
        // Given an input string, finds a response using AIMLbot lib
        public string getInput()
        {           
            Console.Write("You: ");
            string input = Console.ReadLine();
            return input;
        }
        public String getOutput(String input)
        {
            try
            {
                Request r = new Request(input, myUser, AimlBot);
                Result res = AimlBot.Chat(r);                
                return (res.Output);
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }

        }

        public void SaveUserInfo()
        {
            //save user info  
            string userInfoPath = Path.Combine(this.AimlBot.PathToUserFiles, UserId + ".xml");
            XmlDocument myNewUserSettings = myUser.Predicates.DictionaryAsXML;
            myNewUserSettings.Save(userInfoPath);
        }
    }
}
