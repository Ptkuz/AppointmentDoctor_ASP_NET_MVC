using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;

namespace DoctorsAppointments.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context) 
        { 
            db = context;
        }

        public async Task<IActionResult> ViewDoctors(int page = 1) 
        {
            int pageSize = 5;
            IQueryable<Doctor> source = db.Doctors.Include(d=>d.Profile);
            var count = await source.CountAsync();
            var items = await source.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
            return View(viewModel);
        }


    }
}
