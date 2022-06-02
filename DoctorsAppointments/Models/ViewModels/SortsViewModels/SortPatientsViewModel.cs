using DoctorsAppointments.Models.DataBase.Sorts;
using DoctorsAppointments.Models.DataBase;

namespace DoctorsAppointments.Models.ViewModels.SortsViewModels
{
    public class SortPatientsViewModel
    {
        public SortPatients FirstNameSort { get; set; }
        public SortPatients PatronymicSort { get; set; }
        public SortPatients LastNameSort { get; set; }
        public SortPatients DateAgeSort { get; set; }
        public SortPatients GenderSort { get; set; }
        public SortPatients PassportSort { get; set; }
        public SortPatients PolisSort { get; set; }
        public SortPatients SnilsSort { get; set; }
        public SortPatients Current { get; set; }
        public bool Up { get; set; }

        public SortPatientsViewModel(SortPatients sortOrder)
        {
            FirstNameSort = SortPatients.FirstNameAsc;
            PatronymicSort = SortPatients.PatronymicAsc;
            LastNameSort = SortPatients.LastNameAsc;
            DateAgeSort = SortPatients.DateAgeAsc;
            GenderSort = SortPatients.GenderAsc;
            PassportSort = SortPatients.PassportAsc;
            PolisSort = SortPatients.PolisAsc;
            SnilsSort = SortPatients.SnilsAsc;
            Up = true;

            if (sortOrder == SortPatients.FirstNameDesc || sortOrder == SortPatients.PatronymicDesc ||
                sortOrder == SortPatients.LastNameDesc || sortOrder == SortPatients.DateAgeDesc ||
                sortOrder == SortPatients.GenderDesc || sortOrder == SortPatients.PassportDesc ||
                sortOrder == SortPatients.PolisDesc || sortOrder == SortPatients.SnilsDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortPatients.FirstNameAsc:
                    Current = FirstNameSort = SortPatients.FirstNameDesc;
                    break;
                case SortPatients.FirstNameDesc:
                    Current = FirstNameSort = SortPatients.FirstNameAsc;
                    break;
                case SortPatients.PatronymicAsc:
                    Current = PatronymicSort = SortPatients.PatronymicDesc;
                    break;
                case SortPatients.PatronymicDesc:
                    Current = PatronymicSort = SortPatients.PatronymicAsc;
                    break;
                case SortPatients.LastNameAsc:
                    Current = LastNameSort = SortPatients.LastNameDesc;
                    break;
                case SortPatients.LastNameDesc:
                    Current = LastNameSort = SortPatients.LastNameAsc;
                    break;
                case SortPatients.DateAgeAsc:
                    Current = DateAgeSort = SortPatients.DateAgeDesc;
                    break;
                case SortPatients.DateAgeDesc:
                    Current = DateAgeSort = SortPatients.DateAgeAsc;
                    break;
                case SortPatients.GenderDesc:
                    Current = GenderSort = SortPatients.GenderAsc;
                    break;
                case SortPatients.PassportAsc:
                    Current = PassportSort = SortPatients.PassportDesc;
                    break;
                case SortPatients.PassportDesc:
                    Current = PassportSort = SortPatients.PassportAsc;
                    break;
                case SortPatients.PolisAsc:
                    Current = PolisSort = SortPatients.PolisDesc;
                    break;
                case SortPatients.PolisDesc:
                    Current = PolisSort = SortPatients.PolisAsc;
                    break;
                case SortPatients.SnilsAsc:
                    Current = SnilsSort = SortPatients.SnilsDesc;
                    break;
                case SortPatients.SnilsDesc:
                    Current = SnilsSort = SortPatients.SnilsAsc;
                    break;

                default:
                    Current = GenderSort = SortPatients.GenderDesc;
                    break;


            }

        }
    }
}
