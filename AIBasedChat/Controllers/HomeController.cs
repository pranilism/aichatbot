using AIBasedChat.Models;
using AIMLbot;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIBasedChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new ChatViewModel { BotSaid = string.Empty });
        }

        [HttpPost]
        public IActionResult AskBot(ChatInput input)
        {
            Bot AI = new Bot();
            AI.loadSettings();
            AI.DefaultPredicates.updateSetting("name", input.Name);
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = false;
            User myuser = new User(input.Name, AI);
            AI.isAcceptingUserInput = true;
            Request r = new Request(input.Message, myuser, AI);
            Result res = AI.Chat(r);
            return Json(new ChatViewModel { BotSaid = res.Output });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}