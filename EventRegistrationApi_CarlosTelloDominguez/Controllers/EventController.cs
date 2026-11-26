using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationApi_CarlosTelloDominguez.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
