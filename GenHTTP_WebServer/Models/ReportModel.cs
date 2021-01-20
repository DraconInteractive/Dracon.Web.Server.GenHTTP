using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP_WebServer.Models
{
    public record Report (string Name, DateTime Submitted, string Details);

    public record ReportModel (List<Report> Reports);
}
