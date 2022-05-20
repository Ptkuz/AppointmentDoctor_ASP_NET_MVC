namespace DoctorsAppointments.Models.DataBase
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string? LastName { get; set; }
        public DateTime DateAge { get; set; }
        public string? Passport { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public int Experience { get; set; }
       public List<Appointment> Appointments { get; set; } = new();
    }
}
