using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP_WebServer.Models;

namespace GenHTTP_WebServer.Controllers
{
    public class ReportController
    {
        private static List<Report> _Reports = new List<Report>()
        {
            new Report("Report 1", new DateTime(2020, 12, 10), "Issue reported with these details...s.s."),
            new Report("Report 2", new DateTime(2021, 1, 5), "Something went wrong")
        };

        public IHandlerBuilder Index()
        {
            var model = new ReportModel(_Reports);

            return ModScriban.Page(Resource.FromAssembly("reports.html"), (r, h) => new ViewModel<ReportModel>(r, h, model))
                .Title("Reports");
        }
    }
}
