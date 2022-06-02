using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace DoctorsAppointments.Controllers
{





    public class DataController : Controller
    {

        ApplicationContext db;
        public DataController(ApplicationContext context)
        {
            db = context;
        }



        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateDoctor()
        {
            Doctor doctor = new Doctor() { DateAge = DateTime.Now };
            Patient patient = new Patient();
            User user = new User();

            ViewBag.Profiles = new SelectList(db.Profiles, "Id", "Name");

            IndexViewModel indexViewModel = new IndexViewModel(patient, doctor, user, null);

            return View(indexViewModel);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(IndexViewModel indexViewModel)
        {
            indexViewModel.User.Id = Guid.NewGuid();
            indexViewModel.User.RoleId = 2;
            indexViewModel.Doctor.Id = Guid.NewGuid();
            indexViewModel.Doctor.UserKey = indexViewModel.User.Id;
            db.Users.Add(indexViewModel.User);
            db.Doctors.Add(indexViewModel.Doctor);

            await db.SaveChangesAsync();
            return RedirectToAction("ViewDoctors", "Home");

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateProfile() => View();


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewProfiles", "Home");

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreatePatient()
        {
            Doctor doctor = new Doctor();
            Patient patient = new Patient() { DateAge = DateTime.Now };
            User user = new User();

            ViewBag.Genders = new SelectList(AllGenders(), "Value", "Text");

            IndexViewModel indexViewModel = new IndexViewModel(patient, doctor, user, null);

            return View(indexViewModel);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePatient(IndexViewModel indexViewModel)
        {
            indexViewModel.User.Id = Guid.NewGuid();
            indexViewModel.User.RoleId = 3;
            indexViewModel.Patient.Id = Guid.NewGuid();
            indexViewModel.Patient.UserKey = indexViewModel.User.Id;
            db.Users.Add(indexViewModel.User);
            db.Patients.Add(indexViewModel.Patient);

            await db.SaveChangesAsync();
            return RedirectToAction("ViewPatients", "Home");

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateAppointment()
        {

            ViewBag.Patients = new SelectList(AllPatients(), "PatientId", "PatientName");
            ViewBag.Doctors = new SelectList(AllDoctors(), "DoctorId", "DoctorName");
            ViewBag.TimeAppointment = new SelectList(AllTimes(), "Value", "Text");

            return View();

        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            var findTime = await db.Appointments.FirstOrDefaultAsync(x => x.TimeAppointment == appointment.TimeAppointment && x.DateAppointment == appointment.DateAppointment);

            if (findTime != null)
            {


                TempData["msg"] = FindTime(findTime).ToString();
                return RedirectToAction("CreateAppointment", "Data");
            }
            else if (appointment.TimeAppointment == null)
            {
                TempData["msg"] = "Не выбрано время записи";
                return RedirectToAction("CreateAppointment", "Data");
            }
            else
            {
                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewAppointments", "Home");
            }

        }

        [Authorize(Roles = "patient, doctor")]
        [HttpGet]
        public async Task<IActionResult> UserCreateAppointment(Guid id)
        {

            ViewBag.TimeAppointment = new SelectList(AllTimes(), "Value", "Text");
            if (id != null && AccountController.idUser != Guid.Empty)
            {
                Appointment? appointment = null;


                var patientUser = await db.Patients.FirstOrDefaultAsync(p => p.UserKey == AccountController.idUser);
                var doctorUser = await db.Doctors.FirstOrDefaultAsync(p => p.UserKey == AccountController.idUser);

                if (patientUser != null)
                {
                    var doctor = await db.Doctors.FirstOrDefaultAsync(p => p.Id == id);
                    appointment = new()
                    {
                        Id = Guid.NewGuid(),
                        DoctorId = id,
                        PatientId = patientUser.Id,
                        DateAppointment = DateTime.Now,
                        TimeAppointment = TimeAppointment.Time1000
                    };
                }
                else if (doctorUser != null)
                {
                    var patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                    appointment = new()
                    {
                        Id = Guid.NewGuid(),
                        DoctorId = doctorUser.Id,
                        PatientId = id,
                        DateAppointment = DateTime.Now,
                        TimeAppointment = TimeAppointment.Time1000
                    };
                }



                if (patientUser != null || doctorUser != null)
                {
                    return View(appointment);
                }

            }
            return NotFound();
        }

        [Authorize(Roles = "patient, doctor")]
        [HttpPost]
        public async Task<IActionResult> UserCreateAppointment(Appointment appointment)
        {

            var findTime = await db.Appointments.FirstOrDefaultAsync(x => x.TimeAppointment == appointment.TimeAppointment && x.DateAppointment == appointment.DateAppointment);

            if (findTime != null)
            {


                TempData["msg"] = FindTime(findTime).ToString();

                return RedirectToAction("UserCreateAppointment", "Data");
            }
            else if (appointment.TimeAppointment == null)
            {
                TempData["msg"] = "Не выбрано время записи";
                return RedirectToAction("UserCreateAppointment", "Data");
            }
            else
            {
                Patient? patientUser = await db.Patients.FirstOrDefaultAsync(p => p.UserKey == AccountController.idUser);
                Doctor? doctorUser = await db.Doctors.FirstOrDefaultAsync(p => p.UserKey == AccountController.idUser);
                if (patientUser != null)
                {
                    appointment.PatientId = patientUser!.Id;
                    appointment.DoctorId = appointment.Id;
                    appointment.Id = Guid.NewGuid();
                    db.Appointments.Add(appointment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ViewAppointments", "Home", new { id = patientUser.Id });
                }
                else if (doctorUser != null)
                {
                    appointment.DoctorId = doctorUser.Id;
                    appointment.PatientId = appointment.Id;
                    appointment.Id = Guid.NewGuid();
                    db.Appointments.Add(appointment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ViewAppointments", "Home", new { id = doctorUser.Id });

                }
                return RedirectToAction("Index", "Home");

            }


        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(Guid? id)
        {
            if (id != null)
            {
                Doctor doctor = new Doctor { Id = id.Value };
                db.Entry(doctor).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ViewDoctors", "Home");
            }
            return NotFound();
        }


        [Authorize(Roles = "doctor, admin")]
        [HttpGet]
        public async Task<IActionResult> EditDoctor(Guid? id)
        {
            SelectList profiles = new SelectList(db.Profiles, "Id", "Name");
            ViewBag.Profiles = profiles;
            if (id != null)
            {
                Doctor? doctor = await db.Doctors.FirstOrDefaultAsync(p => p.Id == id);
                if (doctor != null)
                {
                    return View(doctor);
                }

            }
            return NotFound();
        }


        [Authorize(Roles = "doctor, admin")]
        [HttpPost]
        public async Task<IActionResult> EditDoctor(Doctor doctor)
        {
            doctor.UserKey = AccountController.idUser;
            db.Doctors.Update(doctor);
            await db.SaveChangesAsync();
            if (doctor.UserKey != null)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("ViewDoctors", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePatient(Guid? id)
        {
            if (id != null)
            {
                Patient? patient = new Patient { Id = id.Value };
                db.Entry(patient).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ViewPatients", "Home");
            }
            return NotFound();
        }


        [Authorize(Roles = "patient, admin")]
        [HttpGet]
        public async Task<IActionResult> EditPatient(Guid? id)
        {
            if (id != null)
            {

                ViewBag.Genders = new SelectList(AllGenders(), "Value", "Text");

                Patient? patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (patient != null)
                {
                    return View(patient);
                }

            }
            return NotFound();
        }

        [Authorize(Roles = "patient, admin")]
        [HttpPost]
        public async Task<IActionResult> EditPatient(Patient patient)
        {

            patient.UserKey = AccountController.idUser;
            db.Patients.Update(patient);
            await db.SaveChangesAsync();
            if (patient.UserKey != null)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("ViewPatients", "Home");
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteProfile(int? id)
        {
            if (id != null)
            {
                Profile? profile = new Profile { Id = id.Value };
                db.Entry(profile).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ViewProfile", "Home");
            }
            return NotFound();
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id != null)
            {
                Profile? profile = await db.Profiles.FirstOrDefaultAsync(p => p.Id == id);
                if (profile != null)
                {
                    return View(profile);
                }

            }
            return NotFound();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(Profile profile)
        {


            db.Profiles.Update(profile);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewProfiles", "Home");
        }


        [Authorize(Roles = "admin, patient, doctor")]
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(Guid? id)
        {
            if (id != null)
            {
                Appointment? appointment = new Appointment { Id = id.Value };
                db.Entry(appointment).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                var patient = db.Patients.FirstOrDefault(p => p.UserKey == AccountController.idUser);
                var doctor = db.Doctors.FirstOrDefault(p => p.UserKey == AccountController.idUser);
                if (AccountController.idUser == patient?.UserKey)

                    return RedirectToAction("ViewAppointments", "Home", new { id = patient!.Id });

                else if (AccountController.idUser == doctor?.UserKey)

                    return RedirectToAction("ViewAppointments", "Home", new { id = doctor!.Id });

                else
                    return RedirectToAction("ViewAppointments", "Home");
            }
            return NotFound();
        }



        public IQueryable AllPatients()
        {
            var patients = from patient in db.Patients
                           select new
                           {
                               PatientId = patient.Id,
                               PatientName = string.Format("{0} {1} {2} - {3}", patient.LastName, patient.FirstName, patient.Patronymic,
                                patient.Polis
                               )
                           };
            return patients;
        }

        public IQueryable AllDoctors()
        {
            var doctors = from doctor in db.Doctors
                          select new
                          {
                              DoctorId = doctor.Id,
                              DoctorName = string.Format("{0} {1} {2} - {3}", doctor.LastName, doctor.FirstName, doctor.Patronymic,
                               doctor.Profile.Name
                              )
                          };
            return doctors;

        }

        public IEnumerable AllTimes()
        {
            var times = from TimeAppointment time in Enum.GetValues(typeof(TimeAppointment))
                        select new
                        {
                            Value = Convert.ToInt32(time).ToString(),
                            Text = time.GetAttribute<DisplayAttribute>().Name
                        };

            return times;
        }

        public IEnumerable AllGenders()
        {
            var genders = from Genders gender in Enum.GetValues(typeof(Genders))
                          select new
                          {
                              Value = Convert.ToInt32(gender).ToString(),
                              Text = gender.GetAttribute<DisplayAttribute>().Name
                          };
            return genders;
        }

        public StringBuilder FindTime(Appointment findTime)
        {
            var freeTime = from time in db.Appointments where time.TimeAppointment == findTime.TimeAppointment select time;

            StringBuilder sb = new StringBuilder();
            sb.Append($"{findTime.DateAppointment.ToShortDateString()} На время {findTime.TimeAppointment.GetAttribute<DisplayAttribute>().Name} уже существует запись \n " +
                $"Доступное время для записи: \n");

            foreach (TimeAppointment timeAppointment in (TimeAppointment[])Enum.GetValues(typeof(TimeAppointment)))
            {
                if (timeAppointment != findTime.TimeAppointment)
                    sb.Append($"{timeAppointment.GetAttribute<DisplayAttribute>().Name} \n");
            }
            return sb;


        }
    }
}
