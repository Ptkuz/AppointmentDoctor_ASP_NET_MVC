using DoctorsAppointments.Models.DataBase;

namespace DoctorsAppointments.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Profile> Profiles { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

       public PageViewModel PageViewModel { get; }

        public IndexViewModel(IEnumerable<Doctor> doctors, PageViewModel viewModel) 
        { 
            Doctors = doctors;
            PageViewModel = viewModel;
        }

        public IndexViewModel(IEnumerable<Patient> patients, PageViewModel viewModel) 
        { 
            Patients = patients;
            PageViewModel= viewModel;
        }

        public IndexViewModel(IEnumerable<Appointment> appointments, PageViewModel viewModel) 
        { 
            Appointments = appointments;
            PageViewModel = viewModel;
        }
    }
}
