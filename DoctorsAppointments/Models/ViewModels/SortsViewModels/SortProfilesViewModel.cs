using DoctorsAppointments.Models.DataBase.Sorts;

namespace DoctorsAppointments.Models.ViewModels.SortsViewModels
{
    public class SortProfilesViewModel
    {
        public SortProfiles NameSort { get; set; }
        public SortProfiles Current { get; set; }
        public bool Up { get; set; }

        public SortProfilesViewModel(SortProfiles sortOrder)
        {
            NameSort = SortProfiles.NameAsc;
            Up = true;

            if (sortOrder == SortProfiles.NameDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortProfiles.NameDesc:
                    Current = NameSort = SortProfiles.NameAsc;
                    break;
                default:
                    Current = NameSort = SortProfiles.NameDesc;
                    break;


            }

        }
    }
}
