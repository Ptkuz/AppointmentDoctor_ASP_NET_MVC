﻿using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DoctorsAppointments.Controllers
{




    public class DataController : Controller
    {
        ApplicationContext db;
        public DataController(ApplicationContext context)
        {
            db = context;
        }


        [HttpGet]
        public IActionResult CreateDoctor()
        {
            SelectList profiles = new SelectList(db.Profiles, "Id", "Name");
            ViewBag.Profiles = profiles;
            return View();

        }

        [HttpPost]
        public IActionResult CreateDoctor(Doctor doctor)
        {
            db.Doctors.Add(doctor);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult CreateProfile() => View();

        [HttpPost]
        public IActionResult CreateProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            string[] massGenders = new string[] { "Мужской", "Женский" };

            SelectList genders = new SelectList(massGenders);
            ViewBag.Genders = genders;


            return View();
        }

        [HttpPost]
        public IActionResult CreatePatient(Patient patient)
        {
            db.Patients.Add(patient);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult CreateAppointment()
        {

            var patients = from patient in db.Patients
                           select new
                           {
                               PatientId = patient.Id,
                               PatientName = string.Format("{0} {1} {2} - {3}", patient.LastName, patient.FirstName, patient.Patronymic,
                                patient.Polis
                               )
                           };
            ViewBag.Patients = new SelectList(patients, "PatientId", "PatientName");

            var doctors = from doctor in db.Doctors
                           select new
                           {
                               DoctorId = doctor.Id,
                               DoctorName = string.Format("{0} {1} {2} - {3}", doctor.LastName, doctor.FirstName, doctor.Patronymic,
                                doctor.Profile.Name
                               )
                           };

            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "DoctorName");
            return View();

        }

        [HttpPost]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            db.Appointments.Add(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

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

        [HttpGet]
        public async Task<IActionResult> EditDoctor(Guid? id) 
        {
            SelectList profiles = new SelectList(db.Profiles, "Id", "Name");
            ViewBag.Profiles = profiles;
            if (id!=null) 
            { 
                Doctor? doctor = await db.Doctors.FirstOrDefaultAsync(p=>p.Id==id);
                if (doctor!=null) 
                { 
                    return View(doctor);
                }
            
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditDoctor(Doctor doctor) 
        {
            

            db.Doctors.Update(doctor);
            await db.SaveChangesAsync();
            return RedirectToAction("ViewDoctors", "Home");
        }
    }
}
