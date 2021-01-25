using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP_WebServer.Models
{
    public record GameComponent (string Name, string Description, List<(string,string)> Part);

    public record Game (int ID, bool Eternal, string Name, string Subtitle, string Description, string Status, string ReleaseDate, string Language, string Platform, string[] Features, string Link, string LinkName, string Extra, List<(string,string)> Embed, string Logo, GameComponent[] Components);

    public record GameModel (List<Game> Games);
}
