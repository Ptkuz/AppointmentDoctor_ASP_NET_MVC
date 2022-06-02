using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointments.Models.DataBase
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Неверный формат Email")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Пароль не соответствует шаблону")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
