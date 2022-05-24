using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.DataBase.Sorts;

namespace DoctorsAppointments.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index() => View();


        public async Task<IActionResult> ViewDoctors(string name, Guid profile, int page = 1, SortDoctors sortOrder = SortDoctors.ProfileNameAsc)
        {
            int pageSize = 5;

            // Фильтрация
            IQueryable<Doctor> doctors = db.Doctors.Include(d => d.Profile);
            if (profile != Guid.Empty)
                doctors = doctors.Where(p=>p.ProfileId == profile);
            if (!string.IsNullOrEmpty(name))
            {

                    doctors = doctors.Where(p => (name.Contains(p.FirstName!) | name.Contains(p.Patronymic!) | name.Contains(p.LastName!)) ||
                                                    (name.Contains(p.FirstName!) && name.Contains(p.Patronymic!) && name.Contains(p.LastName!)));
                
            }
            

            doctors = sortOrder switch
            {
                SortDoctors.FirstNameAsc => doctors.OrderBy(n=>n.FirstName),
                SortDoctors.FirstNameDesc => doctors.OrderByDescending(n=>n.FirstName),
                SortDoctors.PatronymicAsc => doctors.OrderBy(p=>p.Patronymic),
                SortDoctors.PatronymicDesc => doctors.OrderByDescending(p=>p.Patronymic),
                SortDoctors.LastNameAsc => doctors.OrderBy(n=>n.LastName),
                SortDoctors.LastNameDesc => doctors.OrderByDescending(n=>n.LastName),
                SortDoctors.DateAgeAsc => doctors.OrderBy(n=>n.DateAge),
                SortDoctors.DateAgeDesc => doctors.OrderByDescending(n=>n.DateAge),
                SortDoctors.ProfileNameDesc => doctors.OrderByDescending(p=>p.Profile!.Name),
                SortDoctors.ExperienceAsc => doctors.OrderBy(e=>e.Experience),
                SortDoctors.ExperienceDesc => doctors.OrderByDescending(e=>e.Experience),
                _ => doctors.OrderBy(p=>p.Profile!.Name)

            };

            var count = await doctors.CountAsync();
            var items = await doctors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var sort = new SortDoctorsViewModel(sortOrder);
            var filter = new FilterViewModel(db.Profiles.ToList(), profile, name);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, sort, filter);


           

            return View(viewModel);
        }

        public async Task<IActionResult> ViewPatients(int page = 1)
        {
            int pageSize = 5;
            IQueryable<Patient> source = db.Patients;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> ViewProfiles(int page = 1)
        {
            int pageSize = 5;
            IQueryable<Profile> source = db.Profiles;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> ViewAppointments(int page = 1)
        {
            int pageSize = 5;
            IQueryable<Appointment> source = db.Appointments.Include(p=>p.Patient).Include(d=>d.Doctor).Include(pr=>pr.Doctor.Profile);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
            return View(viewModel);
        }

        

    }
}
