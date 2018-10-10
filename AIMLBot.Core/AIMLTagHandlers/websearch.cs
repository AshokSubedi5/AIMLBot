using System;
using System.Xml;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace AIMLBot.Core.AIMLTagHandlers
{
    /// <summary>
    /// The websearch element search data from wiki    
    ///     
    /// for now its only search data from wiki 
    /// </summary>
    public class websearch : AIMLBot.Core.Utils.AIMLTagHandler
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
        public websearch(AIMLBot.Core.Bot bot,
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
            if (this.templateNode.Name.ToLower() == "websearch")
            {
                return GetSearchResult(this.templateNode.InnerText);
            }
            else if(this.templateNode.Name.ToLower() == "searchandlearn")
            {
                return GetAndSaveSearchResult(this.templateNode.InnerText);
            }
            return string.Empty;
        }
       

      

        private string GetSearchResult(string query)
        {
            string address = $"https://api.duckduckgo.com/?q={query}&format=xml&pretty=1";
            HttpWebRequest rssFeed = (HttpWebRequest)WebRequest.Create(address);
            HttpWebResponse response = (HttpWebResponse)rssFeed.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string srString = sr.ReadToEnd();
            //parse and get result
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(srString);
            var root = doc.DocumentElement;
            var resultText = root.GetElementsByTagName("AbstractText")[0].InnerText;
            var resultSource = root.GetElementsByTagName("AbstractSource")[0].InnerText;
            if (string.IsNullOrEmpty(resultText))
            {
                //now search on related topic
                var relatedTopics = doc.SelectNodes("/DuckDuckGoResponse/RelatedTopics/RelatedTopic/Text");
                if (relatedTopics.Count > 0)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, relatedTopics.Count);
                    return relatedTopics[randomNumber].InnerText + " [" + resultSource + "]";
                }

                return "Your search - " + query + " - did not match any documents.";
            }
            else
                return resultText + " [" + resultSource + "]";
           
        }
        private string GetAndSaveSearchResult(string query)
        {
            string address = $"https://api.duckduckgo.com/?q={query}&format=xml&pretty=1";
            HttpWebRequest rssFeed = (HttpWebRequest)WebRequest.Create(address);
            HttpWebResponse response = (HttpWebResponse)rssFeed.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string srString = sr.ReadToEnd();
            //parse and get result
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(srString);
            var root = doc.DocumentElement;
            var resultText = root.GetElementsByTagName("AbstractText")[0].InnerText;
            var resultSource = root.GetElementsByTagName("AbstractSource")[0].InnerText;
            if (string.IsNullOrEmpty(resultText))
            {
                //now search on related topic
                var relatedTopics = doc.SelectNodes("/DuckDuckGoResponse/RelatedTopics/RelatedTopic/Text");
                if (relatedTopics.Count > 0)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, relatedTopics.Count);
                    this.user.Predicates.addSetting(query, relatedTopics[randomNumber].InnerText);
                    return relatedTopics[randomNumber].InnerText + " [" + resultSource + "]";
                }

                return "Your search - " + query + " - did not match any documents.";
            }
            else
            {
                this.user.Predicates.addSetting(query, resultText);
                return resultText + " [" + resultSource + "]";
            }
            

        }
    }
}
