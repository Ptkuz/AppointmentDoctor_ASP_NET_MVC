using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointments.Models.ViewModels.UserViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Неверный формат Email")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage ="Пароль не соответствует шаблону")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Пароль")]
        public string ConfirmPassword { get; set; }
    }
}
