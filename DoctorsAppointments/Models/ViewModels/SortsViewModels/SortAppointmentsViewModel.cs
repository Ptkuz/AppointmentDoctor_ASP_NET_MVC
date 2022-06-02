using DoctorsAppointments.Models.DataBase.Sorts;

namespace DoctorsAppointments.Models.ViewModels.SortsViewModels
{
    public class SortAppointmentsViewModel
    {
        public SortAppointments PatientFirstName { get; set; }
        public SortAppointments PatientPatronymic { get; set; }
        public SortAppointments PatientLastName { get; set; }
        public SortAppointments PatientPolis { get; set; }
        public SortAppointments DoctorFirstName { get; set; }
        public SortAppointments DoctorPatronymic { get; set; }
        public SortAppointments DoctorLastName { get; set; }
        public SortAppointments DoctorProfileName { get; set; }
        public SortAppointments DateAppointment { get; set; }
        public SortAppointments Current { get; set; }
        public bool Up { get; set; }

        public SortAppointmentsViewModel(SortAppointments sortOrder)
        {
            PatientFirstName = SortAppointments.PatientFirstNameAsc;
            PatientPatronymic = SortAppointments.PatientPatronymicAsc;
            PatientLastName = SortAppointments.PatientLastNameAsc;
            PatientPolis = SortAppointments.PatientPolisAsc;
            DoctorFirstName = SortAppointments.DoctorFirstNameAsc;
            DoctorPatronymic = SortAppointments.DoctorPatronymicAsc;
            DoctorLastName = SortAppointments.DoctorLastNameAsc;
            DoctorProfileName = SortAppointments.DoctorProfileNameAsc;
            DateAppointment = SortAppointments.DateAppointmentAsc;
            Up = true;

            if (sortOrder == SortAppointments.PatientFirstNameDesc || sortOrder == SortAppointments.PatientPatronymicDesc ||
                sortOrder == SortAppointments.PatientLastNameDesc || sortOrder == SortAppointments.PatientPolisDesc || sortOrder == SortAppointments.DoctorFirstNameDesc ||
                sortOrder == SortAppointments.DoctorPatronymicDesc || sortOrder == SortAppointments.DoctorLastNameDesc ||
                sortOrder == SortAppointments.DoctorProfileNameDesc || sortOrder == SortAppointments.DateAppointmentDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortAppointments.PatientFirstNameAsc:
                    Current = PatientFirstName = SortAppointments.PatientFirstNameDesc;
                    break;
                case SortAppointments.PatientFirstNameDesc:
                    Current = PatientFirstName = SortAppointments.PatientFirstNameAsc;
                    break;
                case SortAppointments.PatientPatronymicAsc:
                    Current = PatientPatronymic = SortAppointments.PatientPatronymicDesc;
                    break;
                case SortAppointments.PatientPatronymicDesc:
                    Current = PatientPatronymic = SortAppointments.PatientPatronymicAsc;
                    break;
                case SortAppointments.PatientLastNameAsc:
                    Current = PatientLastName = SortAppointments.PatientLastNameDesc;
                    break;
                case SortAppointments.PatientLastNameDesc:
                    Current = PatientLastName = SortAppointments.PatientLastNameAsc;
                    break;
                case SortAppointments.PatientPolisAsc:
                    Current = PatientPolis = SortAppointments.PatientPolisDesc;
                    break;
                case SortAppointments.PatientPolisDesc:
                    Current = PatientPolis = SortAppointments.PatientPolisAsc;
                    break;

                case SortAppointments.DoctorFirstNameAsc:
                    Current = DoctorFirstName = SortAppointments.DoctorFirstNameDesc;
                    break;
                case SortAppointments.DoctorFirstNameDesc:
                    Current = DoctorFirstName = SortAppointments.DoctorFirstNameAsc;
                    break;
                case SortAppointments.DoctorPatronymicAsc:
                    Current = DoctorPatronymic = SortAppointments.DoctorFirstNameDesc;
                    break;
                case SortAppointments.DoctorPatronymicDesc:
                    Current = DoctorPatronymic = SortAppointments.DoctorPatronymicAsc;
                    break;
                case SortAppointments.DoctorLastNameAsc:
                    Current = DoctorLastName = SortAppointments.DoctorLastNameDesc;
                    break;
                case SortAppointments.DoctorLastNameDesc:
                    Current = DoctorLastName = SortAppointments.DoctorLastNameAsc;
                    break;
                case SortAppointments.DoctorProfileNameAsc:
                    Current = DoctorProfileName = SortAppointments.DoctorProfileNameDesc;
                    break;
                case SortAppointments.DoctorProfileNameDesc:
                    Current = DoctorProfileName = SortAppointments.DoctorProfileNameAsc;
                    break;
                case SortAppointments.DateAppointmentDesc:
                    Current = DateAppointment = SortAppointments.DateAppointmentAsc;
                    break;
                default:
                    Current = DateAppointment = SortAppointments.DateAppointmentDesc;
                    break;


            }

        }
    }
}
