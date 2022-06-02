using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.DataBase.Sorts;
using DoctorsAppointments.Models.ViewModels;
using DoctorsAppointments.Models.ViewModels.SortsViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DoctorsAppointments.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        IQueryable<Doctor>? doctors = null;
        IQueryable<Patient>? patients = null;
        IQueryable<Profile>? profiles = null;
        IQueryable<Appointment>? appointments = null;


        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        //[Authorize(Roles = "admin, patient")]
        public async Task<IActionResult> Index(Guid id) 
        {
            id = AccountController.idUser;


            Patient? patient = await db.Patients.FirstOrDefaultAsync(p => p.UserKey == id);
            Doctor? doctor = await db.Doctors.FirstOrDefaultAsync(p => p.UserKey == id);
            User? user = await db.Users.FirstOrDefaultAsync(u=>u.Id == id && (u.RoleId == 1 || u.RoleId == 2 || u.RoleId == 3));
            doctors = db.Doctors.Include(d => d.Profile).OrderBy(p=>p.Profile.Name);
            IndexViewModel viewModel = null;

           //if(user != null)
                viewModel = new IndexViewModel(patient, doctor, user, doctors);
            

            return View(viewModel);
        
        }


        [Authorize(Roles = "admin, patient")]
        public async Task<IActionResult> ViewDoctors(string name, int profile, int page = 1, SortDoctors sortOrder = SortDoctors.ProfileNameAsc)
        {
            int pageSize = 5;

            // Фильтрация
            doctors = db.Doctors.Include(d => d.Profile);
           
            if (profile != 0)
                doctors = doctors.Where(p=>p.ProfileId == profile);
            if (!string.IsNullOrEmpty(name))
            {

                Extensions.Filter(name, ref doctors, ref patients, ref appointments);

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
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == AccountController.idUser);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, sort, filter, user);


           

            return View(viewModel);
        }

        [Authorize(Roles = "admin, doctor")]
        public async Task<IActionResult> ViewPatients(string name, int page = 1, SortPatients sortOrder = SortPatients.GenderAsc)
        {
            int pageSize = 5;

            patients = db.Patients;

            if (!string.IsNullOrEmpty(name)) 
            {
                Extensions.Filter(name, ref doctors, ref patients, ref appointments);
            }

            patients = sortOrder switch 
            {
                SortPatients.FirstNameAsc => patients.OrderBy(n=>n.FirstName),
                SortPatients.FirstNameDesc => patients.OrderByDescending(n=>n.FirstName),
                SortPatients.PatronymicAsc => patients.OrderBy(n=>n.Patronymic),
                SortPatients.PatronymicDesc => patients.OrderByDescending(n=>n.Patronymic),
                SortPatients.LastNameAsc => patients.OrderBy(n=>n.LastName),
                SortPatients.LastNameDesc => patients.OrderByDescending(n=>n.LastName),
                SortPatients.DateAgeAsc => patients.OrderBy(n=>n.DateAge),
                SortPatients.DateAgeDesc => patients.OrderByDescending(n=>n.DateAge),
                SortPatients.GenderDesc => patients.OrderByDescending(n=>n.Genders),
                SortPatients.PassportAsc => patients.OrderBy(n=>n.Passport),
                SortPatients.PassportDesc => patients.OrderByDescending(n=>n.Passport),
                SortPatients.PolisAsc => patients.OrderBy(n=>n.Polis),
                SortPatients.PolisDesc => patients.OrderByDescending(n=>n.Polis),
                SortPatients.SnilsAsc => patients.OrderBy(n=>n.Snils),
                SortPatients.SnilsDesc => patients.OrderByDescending(n=>n.Snils),
                _=>patients.OrderBy(n=>n.Genders)
            };

            var count = await patients.CountAsync();
            var items = await patients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var sort = new SortPatientsViewModel(sortOrder);
            var filter = new FilterViewModel(name);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == AccountController.idUser);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, sort, filter, user);

            return View(viewModel);

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ViewProfiles(int page = 1, SortProfiles sortOrder = SortProfiles.NameAsc)
        {
            int pageSize = 5;
            profiles = db.Profiles;

            profiles = sortOrder switch
            {
                SortProfiles.NameDesc => profiles.OrderByDescending(n => n.Name),
                _ => profiles.OrderBy(n => n.Name)
            };

            var count = await profiles.CountAsync();
            var items = await profiles.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var sort = new SortProfilesViewModel(sortOrder);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == AccountController.idUser);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, sort, user);
            return View(viewModel);
        }


        [Authorize(Roles = "admin, patient, doctor")]
        public async Task<IActionResult> ViewAppointments(string name, int page = 1, SortAppointments sortOrder = SortAppointments.DateAppointmentAsc)
        {
            int pageSize = 5;
            var patient = await db.Patients.FirstOrDefaultAsync(p => p.UserKey == AccountController.idUser);
            var doctor = await db.Doctors.FirstOrDefaultAsync(d => d.UserKey == AccountController.idUser);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == AccountController.idUser);
            if (patient != null)
            {
                appointments = db.Appointments.Include(p => p.Patient).Include(d => d.Doctor).Include(pr => pr.Doctor!.Profile).Where(a => a.PatientId == patient.Id);
            }
            else if (patient != null) 
            {
                appointments = db.Appointments.Include(p => p.Patient).Include(d => d.Doctor).Include(pr => pr.Doctor!.Profile).Where(a => a.DoctorId == doctor.Id);
            }
            else
                appointments = db.Appointments.Include(p => p.Patient).Include(d => d.Doctor).Include(pr => pr.Doctor.Profile);

            if (!string.IsNullOrEmpty(name)) 
            {
                Extensions.Filter(name, ref doctors, ref patients, ref appointments);
            }

            appointments = sortOrder switch 
            {
                SortAppointments.PatientFirstNameAsc => appointments.OrderBy(n=>n.Patient!.FirstName),
                SortAppointments.PatientFirstNameDesc => appointments.OrderByDescending(n=>n.Patient!.FirstName),
                SortAppointments.PatientPatronymicAsc => appointments.OrderBy(n=>n.Patient!.Patronymic),
                SortAppointments.PatientPatronymicDesc => appointments.OrderByDescending(n=>n.Patient!.Patronymic),
                SortAppointments.PatientLastNameAsc => appointments.OrderBy(n=>n.Patient!.LastName),
                SortAppointments.PatientLastNameDesc => appointments.OrderByDescending(n=>n.Patient!.LastName),
                SortAppointments.PatientPolisAsc => appointments.OrderBy(n=>n.Patient!.Polis),
                SortAppointments.PatientPolisDesc => appointments.OrderByDescending(n=>n.Patient!.Polis),
                SortAppointments.DoctorFirstNameAsc => appointments.OrderBy(n => n.Doctor!.FirstName),
                SortAppointments.DoctorFirstNameDesc => appointments.OrderByDescending(n => n.Doctor!.FirstName),
                SortAppointments.DoctorPatronymicAsc => appointments.OrderBy(n => n.Doctor!.Patronymic),
                SortAppointments.DoctorPatronymicDesc => appointments.OrderByDescending(n => n.Doctor!.Patronymic),
                SortAppointments.DoctorLastNameAsc => appointments.OrderBy(n => n.Doctor!.LastName),
                SortAppointments.DoctorLastNameDesc => appointments.OrderByDescending(n => n.Doctor!.LastName),
                SortAppointments.DoctorProfileNameAsc => appointments.OrderBy(n=>n.Doctor!.Profile!.Name),
                SortAppointments.DoctorProfileNameDesc => appointments.OrderByDescending(n=>n.Doctor!.Profile!.Name),
                SortAppointments.DateAppointmentDesc => appointments.OrderByDescending(n=>n.DateAppointment),
                _ => appointments.OrderBy(n => n.DateAppointment)



            };

            var count = await appointments.CountAsync();
            var items = await appointments.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var sort = new SortAppointmentsViewModel(sortOrder);
            var filter = new FilterViewModel(name);
            

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, sort, filter, user);

            return View(viewModel);
        }

        

    }
}
