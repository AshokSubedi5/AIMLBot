using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace AIMLBot.Core.AIMLTagHandlers
{
    /// <summary>
    /// The quote element get random quote from server
    /// of the AIML interpreter.
    /// 
    /// The quote element does not have any content. 
    /// </summary>
    public class quote : AIMLBot.Core.Utils.AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public quote(AIMLBot.Core.Bot bot,
                        AIMLBot.Core.User user,
                        AIMLBot.Core.Utils.SubQuery query,
                        AIMLBot.Core.Request request,
                        AIMLBot.Core.Result result,
                        XmlNode templateNode)
            : base(bot, user, query, request, result, templateNode)
        {
        }

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "quote")
            {
                return GetQuote();
            }
            return string.Empty;
        }

        private string GetQuote()
        {
            string address = "http://fullerdatasvc.azurewebsites.net/fortune/";
            HttpWebRequest rssFeed = (HttpWebRequest)WebRequest.Create(address);
            HttpWebResponse response = (HttpWebResponse)rssFeed.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            
            return sr.ReadToEnd();
        }
    }
}
