using DoctorsAppointments.Models.DataBase;

namespace DoctorsAppointments.Models.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasPreviousPage2 => PageNumber > 2;
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasNextPage2 => PageNumber > TotalPages-1;

        public PageViewModel(int count, int pageNumber, int pageSize) 
        { 
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
