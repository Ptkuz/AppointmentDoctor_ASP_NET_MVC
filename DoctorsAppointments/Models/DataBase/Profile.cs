using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointments.Models.DataBase
{
    public class Profile
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название профиля не указано")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное название")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name="Название профиля")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Описание профиля не указано")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 200 символов")]
        [Display(Name = "Описание профиля")]
        public string? Description { get; set; }
        public List<Doctor> Doctors { get; set; } = new();

    }
}
