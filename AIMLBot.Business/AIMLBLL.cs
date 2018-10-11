using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLBot.Core;
using System.IO;
using System.Xml;

namespace AIMLBot.Business
{
    public class AIMLBLL
    {
        //variables             
        private Bot aimlBot;
        private User myUser;

        public AIMLBLL(string userId = null)
        {
            if (userId == null)
                userId = "Guest";
            aimlBot = new Bot();
            myUser = new User(userId, aimlBot);
            string userInfoPath = Path.Combine(this.aimlBot.PathToUserFiles, myUser.UserID + ".xml");
            if (File.Exists(userInfoPath))
                myUser.Predicates.loadSettings(userInfoPath);
            // AimlBot.saveToBinaryFile(Path.Combine(this.AimlBot.PathToAIML, "compiledAIML.dat"));
        }

        /// <summary>
        /// Given an input string, finds a response using AIMLbot lib
        /// </summary>
        /// <returns></returns>
        public string getInput()
        {
            Console.Write("You: ");
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Given an input string, finds a response using AIMLbot lib
        /// </summary>
        /// <param name="input">user input</param>
        /// <returns></returns>
        public String getOutput(String input)
        {
            try
            {
                Request r = new Request(input, myUser, aimlBot);
                Result res = aimlBot.Chat(r);
                return (res.Output);
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }

        }

        /// <summary>
        /// Save User Info
        /// </summary>
        public void SaveUserInfo()
        {
            //save user info  
            string userInfoPath = Path.Combine(this.aimlBot.PathToUserFiles, myUser.UserID + ".xml");
            XmlDocument myNewUserSettings = myUser.Predicates.DictionaryAsXML;
            myNewUserSettings.Save(userInfoPath);
        }
    }
}
