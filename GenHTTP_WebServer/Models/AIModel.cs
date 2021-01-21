using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP_WebServer.Models
{
    public record AI(int ID, string Name, string Subtitle, string Descriptions, string Status, string Language, string[] Capabilities, string Link, string LinkName, string Extra);

    public record AIModel(List<AI> AIs);
}
