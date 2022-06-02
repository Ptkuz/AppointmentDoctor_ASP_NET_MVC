using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointments.Models.DataBase
{
    public class Doctor
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя доктора не задано")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Имя:")]
        public string? FirstName { get; set; }



        [Display(Name = "Отчество:")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректное отчество")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        public string? Patronymic { get; set; }


        [Required(ErrorMessage = "Фамилия доктора не задана")]
        [RegularExpression(@"^[A-ЯЁ][а-яё]+$", ErrorMessage = "Некорректная фамилия")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина должна быть от 3 до 20 символов")]
        [Display(Name = "Фамилия:")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Дата рождения доктора не задана")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения:")]
        public DateTime DateAge { get; set; }


        [Required(ErrorMessage = "Паспорт не задан")]
        [RegularExpression(@"^[0-9]{4}\s[0-9]{6}$", ErrorMessage = "Некорректная серия номер паспорта")]
        [Display(Name = "Серия и номер паспорта")]
        public string? Passport { get; set; }


        [Required(ErrorMessage = "Профиль не выбран")]
        [Display(Name = "Профиль:")]
        public int ProfileId { get; set; }


        public Profile? Profile { get; set; }


        [Required(ErrorMessage = "Опыт врача не указан")]
        [Range(0, 100, ErrorMessage = "Некорректное число")]
        [Display(Name = "Опыт работы")]
        public int Experience { get; set; }

        public Guid UserKey { get; set; }
        public User? User { get; set; }

        public List<Appointment> Appointments { get; set; } = new();
    }
}
