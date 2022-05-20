namespace DoctorsAppointments.Models.DataBase
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string? LastName { get; set; }
        public DateTime DateAge { get; set; }
        public string? Gender { get; set; }
        public string? Passport { get; set; }
        public string? Polis { get; set; }
        public string? Snils { get; set; }
        public List<Appointment> Appointments { get; set; } = new();

    }
}
