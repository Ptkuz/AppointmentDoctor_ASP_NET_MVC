using DoctorsAppointments.Models.DataBase.Sorts;

namespace DoctorsAppointments.Models
{
    public class SortDoctorsViewModel 
    {
        public SortDoctors FirstNameSort { get; set; }
        public SortDoctors PatronymicSort { get; set; }
        public SortDoctors LastNameSort { get; set; }
        public SortDoctors DateAgeSort { get; set; }
        public SortDoctors ProfileNameSort { get; set; }
        public SortDoctors ExperienceSort { get; set; }
        public SortDoctors Current { get; set; }
        public bool Up { get; set; }

        public SortDoctorsViewModel(SortDoctors sortOrder)
        {
            FirstNameSort = SortDoctors.FirstNameAsc;
            PatronymicSort = SortDoctors.PatronymicAsc;
            LastNameSort = SortDoctors.LastNameAsc;
            DateAgeSort = SortDoctors.DateAgeAsc;
            ProfileNameSort = SortDoctors.ProfileNameAsc;
            ExperienceSort = SortDoctors.ExperienceAsc;
            Up = true;

            if (sortOrder == SortDoctors.FirstNameDesc || sortOrder == SortDoctors.PatronymicDesc ||
                sortOrder == SortDoctors.LastNameDesc || sortOrder == SortDoctors.DateAgeDesc ||
                sortOrder == SortDoctors.ProfileNameDesc || sortOrder == SortDoctors.ExperienceDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortDoctors.FirstNameAsc:
                    Current = FirstNameSort = SortDoctors.FirstNameDesc;
                    break;
                case SortDoctors.FirstNameDesc:
                    Current = FirstNameSort = SortDoctors.FirstNameAsc;
                    break;
                case SortDoctors.PatronymicAsc:
                    Current = PatronymicSort = SortDoctors.PatronymicDesc;
                    break;
                case SortDoctors.PatronymicDesc:
                    Current = PatronymicSort = SortDoctors.PatronymicAsc;
                    break;
                case SortDoctors.LastNameAsc:
                    Current = LastNameSort = SortDoctors.LastNameDesc;
                    break;
                case SortDoctors.LastNameDesc:
                    Current = LastNameSort = SortDoctors.LastNameAsc;
                    break;
                case SortDoctors.DateAgeAsc:
                    Current = DateAgeSort = SortDoctors.DateAgeDesc;
                    break;
                case SortDoctors.DateAgeDesc:
                    Current = DateAgeSort= SortDoctors.DateAgeAsc;
                    break;
                case SortDoctors.ProfileNameDesc:
                    Current = ProfileNameSort = SortDoctors.ProfileNameAsc;
                    break;
                case SortDoctors.ExperienceAsc:
                    Current = ExperienceSort = SortDoctors.ExperienceDesc;
                    break;
                case SortDoctors.ExperienceDesc:
                    Current = ExperienceSort= SortDoctors.ExperienceAsc;
                    break;
                default:
                    Current = ProfileNameSort = SortDoctors.ProfileNameDesc;
                    break;


            }

        }
    }
}
