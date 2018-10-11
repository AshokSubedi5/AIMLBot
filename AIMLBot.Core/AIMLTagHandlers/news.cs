using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace AIMLBot.Core.AIMLTagHandlers
{
    /// <summary>
    /// The news element search news from bbc website
    /// of the AIML interpreter.
    /// 
    /// The news element does not have any content. 
    /// </summary>
    public class news : AIMLBot.Core.Utils.AIMLTagHandler
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
        public news(AIMLBot.Core.Bot bot,
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
            if (this.templateNode.Name.ToLower() == "news")
            {
                bool includeDescription = false;
                if (this.templateNode.Attributes.Count == 1)
                {
                    if (this.templateNode.Attributes[0].Name.ToLower() == "description")
                    {
                        if (this.templateNode.Attributes[0].Value.ToLower() == "true")
                        {
                            includeDescription = true;
                        }
                    }
                }
                return GetNews(includeDescription);
            }
                return string.Empty;
        }



        private string GetNews(bool includeDescription)
        {
            string rssAddress = "http://newsrss.bbc.co.uk/rss/newsonline_world_edition/front_page/rss.xml";
            HttpWebRequest rssFeed = (HttpWebRequest)WebRequest.Create(rssAddress);
            HttpWebResponse response = (HttpWebResponse)rssFeed.GetResponse();
            XmlDocument feedAsXML = new XmlDocument();
            feedAsXML.Load(response.GetResponseStream());

            // to hold list of headlines
            StringBuilder result = new StringBuilder();

            if (feedAsXML.HasChildNodes)
            {
                XmlNodeList headlines = feedAsXML.GetElementsByTagName("item");
                foreach (XmlNode item in headlines)
                {
                    result.Append(item.ChildNodes[0].InnerText);
                    if (includeDescription)
                    {
                        result.Append(" (" + item.ChildNodes[1].InnerText + ")");
                    }
                    result.Append(", ");
                }
            }
            result.Append("[BBC News]");
            return result.ToString();
        }
    }
}
