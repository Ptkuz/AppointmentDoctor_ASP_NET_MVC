using System.Collections.Generic;
using DoctorsAppointments.Models.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorsAppointments.Models.ViewModels
{
    public class FilterViewModel
    {
        public SelectList? Profiles { get; set; } 
        public int SelectedProfile { get; set; }
        public string SelectedName { get; set; }

        public FilterViewModel(string name)
        {
            SelectedName = name;
        }
        public FilterViewModel(List<Profile> profiles, int profile, string name) 
        {
            profiles.Insert(0, new Profile { Id=0,Name="Все"});
            Profiles = new SelectList(profiles, "Id", "Name", profile);
            SelectedProfile = profile;
            SelectedName = name;
        }
        
    }
}
