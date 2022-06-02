namespace DoctorsAppointments.Models.DataBase
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role() 
        {
            Users = new List<User>();
        }
    }
}
