using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP_WebServer.Models
{
    public record System (int ID, string Name, string Subtitle, string Description, string[] Components, string Language);

    public record SystemModel (List<System> Systems);
}
