using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorsAppointments.Models.DataBase
{
    public enum TimeAppointment
    {
        [Display(Name = "10:00")]
        Time1000,
        [Display(Name = "10:30")]
        Time1030,
        [Display(Name = "11:00")]
        Time1100,
        [Display(Name = "11:30")]
        Time1130,
        [Display(Name = "12:00")]
        Time1200,
        [Display(Name = "12:30")]
        Time1230,
        [Display(Name = "13:00")]
        Time1300,
        [Display(Name = "13:30")]
        Time1330,
        [Display(Name = "14:00")]
        Time1400,
        [Display(Name = "14:30")]
        Time1430,
        [Display(Name = "15:00")]
        Time1500,
        [Display(Name = "15:30")]
        Time1530,
        [Display(Name = "16:00")]
        Time1600,
        [Display(Name = "16:30")]
        Time1630


    }
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid IdPatient { get; set; }
        public Patient? Patient { get; set; }
        public Guid IdDoctor { get; set; }
        public Doctor? Doctor { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAppointment { get; set; }
        public TimeAppointment? TimeAppointment { get; set; }
    }
}
