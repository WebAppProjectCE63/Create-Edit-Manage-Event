using Microsoft.AspNetCore.Mvc;

namespace WebApplicationProject.Controllers
{
    public class ProfileController: Controller
    {
        public IActionResult profilepage()
        {
            return View();
        }
    }
}
