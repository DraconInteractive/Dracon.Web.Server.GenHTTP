using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Modules.Controllers;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP_WebServer.Models;


namespace GenHTTP_WebServer.Controllers
{
    class EternalController
    {
        private static List<Game> _Games = new List<Game>()
        {
            new Game
            (
                0,
                true,
                "Eternal: Ember",
                "Fantasy RPG",
                "The first of the Eternal series, Ember represents the culmination of my game development knowledge." +
                "<br>I am continuously reworking this project, in both design and mechanical implementation. Rather than a long term goal of completion, I have accepted its role as a testing field for the combination of all my new knowledge. " +
                "<br>That being said, I DO hope to one day have a polished product to showcase, as its current implementation is extremely unstable." +
                "<br><br>The first iteration of this project was in the form of a third person RPG, inspired by Dragon Age: Origins. Looking back, I actually achieved many of the required mechanics for this, from the camera movement, inventory systems, UI, statistics, enemy AI, targetting, basic combat, spells and quest systems." +
                "<br>Iteration 1 fell down in graphical quality however. I have never really been a fan of pixel art, and I was attempting a pseudo-realistic fantasy art style. While I had some good assets, I was very new to level design and animation implementation, so the game felt very ... raw." +
                "<br>This is what led me to put the project on hiatus for ~6 months, while I pursued other avenues of growth." +
                "<br><br>Iteration 2 led me in a similar direction, however I ended up focusing less on core mechanics, and more on solid level design. Level design is an area I have long struggled with, although recently I have realised that a lot of that was my lack of confidence in my own designs. My focus on level design in iteration two was rewarded with a noticable uptick in level quality, but when I attempted to move from here to narrative design, I once again felt that I was falling short. Thus, after about 3 months of work, the project entered another hiatus. " +
                "<br><br>Iteration 3 completely remade the game. I decided to move away from the realistic art style, and close combat to a more strategic game view. " +
                "<br>I theorised (somewhat correctly) that a more abstract art-style and level flow would allow me greater freedom of creation, as well as a smaller workload for each area." +
                "<br>The result of this has been quite fun, and has led to the development of a personal art-style that features low-poly level models, coupled with higher detail focii, such as the character and enemy models. I'm using very neutral colors for land geometry, while making interactable objects glow noticable. I've always been a fan of bloom." +
                "<br><br>As I continue this iteration, I am also integrating the Eternal Network into the player journey. This has led to some interesting paths of growth, in database infrastructure, use of Microsoft Azure and some advanced networking (skills I was luckily able to hone at my job with St John).",
                "In Development",
                "Unknown",
                "C# (Unity)",
                "PC - Windows",
                new string[] {"Third Person", "Strategic gameplay", "Abstract art style", "Deep and immersive story", "'Eternal Network' integration"},
                "http://www.draconinteractive.com.au/eternal-ember/",
                "Website",
                "",
                new List<(string,string)> { },
                "logo.png",
                new GameComponent[]
                {
                    new GameComponent
                    (
                        "Character Camera",
                        "A Third-Person follow cam, able to be orbited around the player. The system uses direct movement for accuracy, but dampens the result for player comfort.",
                        new List<(string, string)>()
                        {
                            ("Follow", "using djagjasgjkasdgklj<br>sddfjasdfjhasdghasgd<br>asdjkghaskjdhga"),
                            ("Dampen","using klsdgksagd<br>sadkjdgasdgjh<br>sadhasgklhjasdg"),
                            ("Predict Movement", "using djagjasgjkasdgklj<br>sddfjasdfjhasdghasgd<br>asdjkghaskjdhga"),
                            ("Cinematics","using klsdgksagd<br>sadkjdgasdgjh<br>sadhasgklhjasdg")
                        }
                    ),
                    new GameComponent
                    (
                        "Character Movement",
                        "Player movement is based entirely on the Unity NavMesh system. The player navigates via clicking with either the left or right mouse button on the level. The character will navigate to the area if possible. If the click is on an enemy, or an interactable item, the player will also begin the interactive process.",
                        new List<(string, string)>()
                        {
                            ("Select Destination", "using djagjasgjkasdgklj<br>sddfjasdfjhasdghasgd<br>asdjkghaskjdhga"),
                            ("Detect Interaction","using klsdgksagd<br>sadkjdgasdgjh<br>sadhasgklhjasdg"),
                            ("Animate", "using djagjasgjkasdgklj<br>sddfjasdfjhasdghasgd<br>asdjkghaskjdhga"),
                            ("Collider","using klsdgksagd<br>sadkjdgasdgjh<br>sadhasgklhjasdg")
                        }
                    ),
                    new GameComponent
                    (
                        "Items",
                        "Ember uses a traditional RPG style item system. Items have a variety of statistics depending on their type, such as weapon, consumable or crafting material.<br>? They can be stored in enemy drops, the player inventory, crafting stations, NPC's and containers such as chests.",
                        new List<(string, string)>()
                        {
                            ("test", "test")
                        }
                    ),
                    new GameComponent
                    (
                        "Inventory",
                        "Currently, I am using a very loose inventory system, as I'm not fond of how restrictive modern inventory systems can be. The inventory is slot based, allows item stacking, and its only limitation is its size (64 slots). Interestingly, that was just how many slots fit the UI, and wasnt connected to the size of the data being stored.<br>The current item system stores item template data within the game, however future versions will update items from a central server, where I can remotely tweak item values, and add new items. This means the player will require an active internet connection, which I have deemed feasible (the game wont be constantly drawing such as live multiplayer, just updating specs at start and save).",
                        new List<(string, string)>()
                        {
                            ("test", "test")
                        }
                    )
                }
            ),
            new Game
            (
                1,
                true,
                "Eternal: Conflict",
                "Scifi FPS",
                "Set a millenium after the events of Eternal: Ember, Conflict is an intense shooter that explores the evolution of the grand themes of Ember; Politics, religion, art and society being but a few.",
                "In Development",
                "Unknown",
                "C# (Unity)",
                "PC - Windows, XBox Series X",
                new string [] {"First Person scifi combat", "A variety of weapons and character enhancements", "'Eternal Network' integration"},
                "http://www.draconinteractive.com.au/eternal-conflict/",
                "Website",
                "",
                new List<(string,string)> {("VIDEO", "https://www.youtube.com/embed/1eOEcRqIXc4") },
                "logo.png",
                new GameComponent[]{ }
            ),
            new Game
            (
                2,
                true,
                "Eternal: Online",
                "Idle Village Builder",
                "This project was designed to be the 'glue' of the Eternal Series.<br /><br />Where the Draconic Link system allows the Eternal games to share data such as items and information between them, Eternal: Online is the game that takes the most use of this.<br />The core concept was for this project to be like a 'fort', or a 'trading post' for your other games. You would build this town in Eternal: Online, and as it grew, you could trade with it in Eternal: Ember and Eternal: Conflict. Perhaps even visit it, see your hard work. My final vision is to scope this for mobile use, as an idle game or a clicker. ",
                "In Development",
                "Unknown",
                "C# (Unity)",
                "PC - Windows, Android, iOS, WebGL",
                new string [] {"Build your base, and unlock new resources and mechanics", "Sync your progress with other Eternal Games", "Let it build in the background while you play"},
                "http://www.draconinteractive.com.au/eternal-online.html",
                "Website",
                "",
                new List<(string,string)> { },
                "logo.png",
                new GameComponent[]{ }
            )
        };
    
        public IHandlerBuilder Index ()
        {
            var model = new GameModel(_Games);

            return ModScriban.Page(Resource.FromAssembly("eternal_index.html"), (r, h) => new ViewModel<GameModel>(r, h, model))
                            .Title("Games");
        }

        public IHandlerBuilder Details([FromPath] int id)
        {
            var model = _Games.Where(m => m.ID == id).First();

            return ModScriban.Page(Resource.FromAssembly("game.html"), (r, h) => new ViewModel<Game>(r, h, model))
                .Title($"{model.Name}");
        }

        public static List<(string, string)> Links()
        {
            var n = new List<(string, string)>();

            foreach (var a in _Games)
            {
                n.Add(("./details/" + a.ID + "/", a.Name));
            }

            return n;
        }
    }
}
