namespace DoctorsAppointments.Models.DataBase
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Doctor> Doctors { get; set; } = new();

    }
}
