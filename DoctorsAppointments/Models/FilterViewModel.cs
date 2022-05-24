using System.Collections.Generic;
using DoctorsAppointments.Models.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorsAppointments.Models
{
    public class FilterViewModel
    {
        public SelectList Profiles { get; set; } 
        public Guid SelectedProfile { get; set; }
        public string SelectedName { get; set; }

        public FilterViewModel(List<Profile> profiles, Guid profile, string name) 
        {
            profiles.Insert(0, new Profile { Id=Guid.Empty,Name="Все"});
            Profiles = new SelectList(profiles, "Id", "Name", profile);
            SelectedProfile = profile;
            SelectedName = name;
        }
    }
}
