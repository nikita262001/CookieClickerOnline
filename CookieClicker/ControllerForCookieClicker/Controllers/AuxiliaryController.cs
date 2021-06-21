using ControllerForCookieClicker.Different;
using ControllerForCookieClicker.Qiwi;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using Serilog;
using System;
using CookieClicker.ClassLibrary.Models;

namespace ControllerForCookieClicker.Controllers
{
    public class AuxiliaryController : Controller
    {
        DataBaseCookieContext db;
        public AuxiliaryController()
        {
            db = new DataBaseCookieContext();
        }
        // GET: /Auxiliary/
        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /Auxiliary/Welcome/ 
        public string Welcome()
        {
            return JsonSerializer.Serialize(new { Name = "Test", Count = 10, });
        }

        // GET: /HelloWorld/Welcome?name=Rick&numtimes=4 
        // Requires using System.Text.Encodings.Web;
        public string WelcomeName(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }

        [HttpPost]
        public void WebhookQiwiPayments([FromBody] WebhookQiwiPaymentsModel qiwi)
        {
            if (qiwi.payment != null)
            {
                try
                {
                    QiwiRequest.SaveInDataBaseQiwiPayments(qiwi.payment);
                    QiwiRequest.DistributionOfCurrencyAmongAccounts();
                    Log.Information("QiwiRequest.Work Success.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "QiwiRequest.Work Error.");
                }
            }
        }
    }
}
