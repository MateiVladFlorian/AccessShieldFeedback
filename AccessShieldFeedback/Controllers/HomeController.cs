using System.Diagnostics;
using AccessShieldFeedback.Models;
using AccessShieldFeedback.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccessShieldFeedback.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> logger;
        readonly IFeedbackService feedbackService;

        public HomeController(IFeedbackService feedbackService, ILogger<HomeController> logger)
        {
            this.feedbackService = feedbackService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody]FeedbackModel feedbackModel)
        {
            bool done = await feedbackService.AddOrUpdateFeedbackAsync(feedbackModel);
            if (done) return Ok();
            else return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks(int? page)
        {
            FeedbackQueryFilter queryFilter = new FeedbackQueryFilter();
            FeedbackPageModel pageModel = await feedbackService.GetAllFeedbacksAsync(queryFilter, page);
            return Ok(pageModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
