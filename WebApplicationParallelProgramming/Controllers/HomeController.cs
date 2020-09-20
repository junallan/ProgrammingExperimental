using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationParallelProgramming.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var postTask = Task<string>.Factory.StartNew(GetStatsByPost);
            var trafficTask = Task<string>.Factory.StartNew(GetStatsByTrafficSource);
            var audienceTask = Task<string>.Factory.StartNew(GetStatsByAudience);

            //GetStatsByPost();
            //GetStatsByTrafficSource();
            //GetStatsByAudience();

            await Task.WhenAll(postTask, trafficTask, audienceTask);
            stopwatch.Stop();

            //ViewData["result"] = $"Total time taken for processing the request in seconds: {stopwatch.Elapsed.Seconds}";
            ViewData["result"] = postTask.Result + "<br/>" + trafficTask.Result + "<br/>" + audienceTask.Result
                + "<br/>" + "<br/>"
                + "Total time taken for processing the request in seconds : " + stopwatch.Elapsed.Seconds;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GetStatsByPost()
        {
            Thread.Sleep(5000);
            return "Thread Id: " + Thread.CurrentThread.ManagedThreadId.ToString() + "<br/>"
                + "Finsihed getting stats by post at" + DateTime.Now.ToString();
        }
        public string GetStatsByTrafficSource()
        {
            Thread.Sleep(3000);
            return "Thread Id: " + Thread.CurrentThread.ManagedThreadId.ToString() + "<br/>"
                + "Finsihed getting stats by post at" + DateTime.Now.ToString();
        }
        public string GetStatsByAudience()
        {
            Thread.Sleep(2000);
            return "Thread Id: " + Thread.CurrentThread.ManagedThreadId.ToString() + "<br/>"
                  + "Finsihed getting stats by post at" + DateTime.Now.ToString();
        }
    }
}