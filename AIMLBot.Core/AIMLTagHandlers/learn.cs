using System;
using System.Xml;
using System.Text;
using System.IO;

namespace AIMLBot.Core.AIMLTagHandlers
{
    /// <summary>
    /// The learn element instructs the AIML interpreter to retrieve a resource specified by a URI, 
    /// and to process its AIML object contents.
    /// </summary>
    public class learn : AIMLBot.Core.Utils.AIMLTagHandler
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
        public learn(AIMLBot.Core.Bot bot,
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
            if (this.templateNode.Name.ToLower() == "learn")
            {
                // currently only AIML files in the local filesystem can be referenced
                // ToDo: Network HTTP and web service based learning
                if (this.templateNode.InnerText.Length > 0)
                    if (this.templateNode.Attributes.Count == 1)
                        if (this.templateNode.Attributes[0].Name.ToLower() == "name")
                            if (this.templateNode.Attributes[0].Value.ToLower() == "fromfile")
                                loadFromFile();
                            else if (this.templateNode.Attributes[0].Value.ToLower() == "fromtag")
                                loadFromTag();
            }
            else if (this.templateNode.Name.ToLower() == "forget")
            {
                if (this.templateNode.InnerText.Length > 0)
                    this.user.Predicates.removeSetting(this.templateNode.InnerText);
            }
            return string.Empty;
        }



        void loadFromFile()
        {
            string path = this.templateNode.InnerText;
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(this.templateNode.InnerText, "log.aiml"));
                fi = new FileInfo(path);
            }
            if (fi.Exists)
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(path);
                    this.bot.loadAIMLFromXML(doc, path);
                }
                catch
                {
                    this.bot.writeToLog("ERROR! Attempted (but failed) to <learn> some new AIML from the following URI: " + path);
                }
            }
        }
        void loadFromTag()
        {
            var learnData = this.templateNode.InnerText.Split(',');
            this.user.Predicates.addSetting(learnData[0].Trim(), learnData[1].Trim());
            this.user.Predicates.grabSetting(this.templateNode.Attributes[0].Value);

        }
    }
}
