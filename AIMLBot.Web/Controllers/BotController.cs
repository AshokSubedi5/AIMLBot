using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIMLBot.Business;

namespace AIMLBot.Web.Controllers
{
    public class BotController : Controller
    {
        // GET: Bot
        AIMLBLL aiml;

        public BotController()
        {
            aiml = new AIMLBLL();
        }


        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Api to get response output
        /// </summary>
        /// <param name="msg">user input request</param>
        /// <returns></returns>
        [HttpPost]
        public string Post(string msg)
        {
            var output = aiml.getOutput(msg);
            aiml.SaveUserInfo();
            return output;
        }
    }
}