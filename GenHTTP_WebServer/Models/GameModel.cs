using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP_WebServer.Models
{
    public record Game (int ID, bool Eternal, string Name, string Subtitle, string Description, string Status, string ReleaseDate, string Language, string Platform, string[] Features, string Link, string LinkName, string Extra, string Embed);

    public record GameModel (List<Game> Games);
}
