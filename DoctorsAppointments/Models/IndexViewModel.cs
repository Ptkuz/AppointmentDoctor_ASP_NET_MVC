using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments.Models.DataBase.Sorts;

namespace DoctorsAppointments.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Profile> Profiles { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

        public SortDoctorsViewModel SortDoctorsViewModel { get; } = new SortDoctorsViewModel(SortDoctors.ProfileNameAsc);
       public PageViewModel PageViewModel { get; } 
        public FilterViewModel FilterViewModel { get; set; }

        public IndexViewModel(IEnumerable<Doctor> doctors, PageViewModel viewModel, SortDoctorsViewModel sortDoctorsViewModel, FilterViewModel filterViewModel)
        {
            Doctors = doctors;
            PageViewModel = viewModel;
            SortDoctorsViewModel = sortDoctorsViewModel;
            FilterViewModel = filterViewModel;
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

        public IndexViewModel(IEnumerable<Profile> profiles, PageViewModel viewModel)
        {
            Profiles = profiles;
            PageViewModel = viewModel;
        }
    }
}
