using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DoctorsAppointments.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class HomeController : Controller
    {
        
       [Authorize(Roles = "admin")]
        public IActionResult Index() 
        {
            return View();
        
        }

    }
}
