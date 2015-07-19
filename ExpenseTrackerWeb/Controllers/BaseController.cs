using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{

    public enum EnumMessageType
    {
        INFO,
        ERROR
    }

    public class BaseController : Controller
    {
        protected string GetApiURL() 
        {


        }

        protected void ShowMessage(string msgText, EnumMessageType msgType)
        {
            TempData["MessageText"] = msgText;
            TempData["MessageType"] = msgType;
        }

    }
}