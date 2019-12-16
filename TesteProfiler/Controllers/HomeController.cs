using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TesteProfiler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Experiment()
        {
            var profiler = MiniProfiler.Current;
            using (profiler.Step("Set page title"))
            {
                ViewBag.Title = "Home Page";
            }

            using (profiler.Step("Testing..."))
            {
                using (profiler.Step("first request"))
                {
                    // simulate fetching a url
                    using (profiler.CustomTiming("http", "GET http://google.com"))
                    {
                        Thread.Sleep(10);
                    }
                }
                using (profiler.Step("second request"))
                {
                    // simulate fetching a url
                    using (profiler.CustomTiming("http", "GET http://stackoverflow.com"))
                    {
                        Thread.Sleep(20);
                    }

                    using (profiler.CustomTiming("redis", "SET \"mykey\" 10"))
                    {
                        Thread.Sleep(5);
                    }
                }
            }

            // now something that loops
            for (int i = 0; i < 15; i++)
            {
                using (profiler.CustomTiming("redis", "SET \"mykey\" 10"))
                {
                    Thread.Sleep(i);
                }
            }

            return View();
        }
    }
}