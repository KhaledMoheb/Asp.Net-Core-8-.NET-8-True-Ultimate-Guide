using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ConfigurationDemoSolution.Models;

namespace ConfigurationDemoSolution.Controllers
{
	public class HomeController : Controller
	{
		SocialMediaLink _socialMediaLinksOptions;

		public HomeController(IOptions<SocialMediaLink> socialMediaLinksOptions)
		{
			_socialMediaLinksOptions = socialMediaLinksOptions.Value;
		}

		[Route("/")]
		public IActionResult Index()
		{
			ViewBag.socialMediaLinks = _socialMediaLinksOptions;
            return View();
		}
	}
}
