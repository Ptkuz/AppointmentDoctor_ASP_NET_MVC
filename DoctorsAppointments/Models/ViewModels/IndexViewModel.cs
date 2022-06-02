using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.DataBase.Sorts;
using DoctorsAppointments.Models.ViewModels.SortsViewModels;

namespace DoctorsAppointments.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Profile> Profiles { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

        public SortDoctorsViewModel SortDoctorsViewModel { get; } = new SortDoctorsViewModel(SortDoctors.ProfileNameAsc);
        public SortPatientsViewModel SortPatientsViewModel { get; } = new SortPatientsViewModel(SortPatients.GenderAsc);
        public SortAppointmentsViewModel SortAppointmentsViewModel { get; } = new SortAppointmentsViewModel(SortAppointments.DateAppointmentAsc);
        public SortProfilesViewModel SortProfilesViewModel { get; } = new SortProfilesViewModel(SortProfiles.NameAsc);

        public PageViewModel PageViewModel { get; } 
        public FilterViewModel FilterViewModel { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public User User { get; set; }
        public Appointment Appointment { get; set; }


        public IndexViewModel() { }

        public IndexViewModel(Patient patient, Doctor doctor, User user, IEnumerable<Doctor> doctors) 
        { 
            Patient = patient;
            Doctor = doctor;
            User = user;
            Doctors = doctors;

        }



        public IndexViewModel(Doctor doctor, Appointment appointment) 
        {
            Doctor = doctor;
            Appointment = appointment;
        }

        public IndexViewModel(IEnumerable<Doctor> doctors, PageViewModel viewModel, SortDoctorsViewModel sortDoctorsViewModel, FilterViewModel filterViewModel, User user)
        {
            Doctors = doctors;
            PageViewModel = viewModel;
            SortDoctorsViewModel = sortDoctorsViewModel;
            FilterViewModel = filterViewModel;
            User = user;

        }

        public IndexViewModel(IEnumerable<Patient> patients, PageViewModel viewModel, SortPatientsViewModel sortPatientsViewModel, FilterViewModel filterViewModel, User user) 
        { 
            Patients = patients;
            PageViewModel= viewModel;
            SortPatientsViewModel = sortPatientsViewModel;
            FilterViewModel = filterViewModel;
            User=user;
        }

        public IndexViewModel(IEnumerable<Appointment> appointments, PageViewModel viewModel, SortAppointmentsViewModel sortAppointmentsViewModel, FilterViewModel filterViewModel, User user)
        { 
            Appointments = appointments;
            PageViewModel = viewModel;
            SortAppointmentsViewModel = sortAppointmentsViewModel;
            FilterViewModel= filterViewModel;
            User =user;

        }

        public IndexViewModel(IEnumerable<Profile> profiles, PageViewModel viewModel, SortProfilesViewModel sortProfilesViewModel, User user)
        {
            Profiles = profiles;
            PageViewModel = viewModel;
            SortProfilesViewModel = sortProfilesViewModel;
            User = User;
        }
    }
}
