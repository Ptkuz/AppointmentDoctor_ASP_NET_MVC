using DoctorsAppointments.Models.DataBase;
using System.Reflection;

namespace DoctorsAppointments
{
    public static class Extensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
         where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }


        public static void Filter(string name, ref IQueryable<Doctor> doctors, ref IQueryable<Patient> patients, ref IQueryable<Appointment> appointments)
        {
            string[] words = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (doctors != null)
            {
                if (words.Length == 3)
                    doctors = doctors.Where(p => p.FirstName!.Contains(words[0]!) && p.Patronymic!.Contains(words[1]!) && p.LastName!.Contains(words[2]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[2]!) && p.LastName!.Contains(words[0]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[0]!) && p.LastName!.Contains(words[2]!));
                else if (words.Length == 2)
                    doctors = doctors.Where(p => p.FirstName!.Contains(words[0]!) && p.LastName!.Contains(words[1]!) ||
                    p.FirstName!.Contains(words[1]!) && p.LastName!.Contains(words[0]!) ||
                    p.FirstName!.Contains(words[0]!) && p.Patronymic!.Contains(words[1]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[0]!) ||
                    p.Patronymic!.Contains(words[0]!) && p.LastName!.Contains(words[1]!) ||
                    p.Patronymic!.Contains(words[1]!) && p.LastName!.Contains(words[0]!));
                else
                {
                    doctors = doctors.Where(p => p.FirstName!.Contains(name) || p.LastName!.Contains(name) || p.Patronymic!.Contains(name));

                }
            }
            else if (patients != null)
            {
                if (words.Length == 3)
                    patients = patients.Where(p => p.FirstName!.Contains(words[0]!) && p.Patronymic!.Contains(words[1]!) && p.LastName!.Contains(words[2]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[2]!) && p.LastName!.Contains(words[0]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[0]!) && p.LastName!.Contains(words[2]!));
                else if (words.Length == 2)
                    patients = patients.Where(p => p.FirstName!.Contains(words[0]!) && p.LastName!.Contains(words[1]!) ||
                    p.FirstName!.Contains(words[1]!) && p.LastName!.Contains(words[0]!) ||
                    p.FirstName!.Contains(words[0]!) && p.Patronymic!.Contains(words[1]!) ||
                    p.FirstName!.Contains(words[1]!) && p.Patronymic!.Contains(words[0]!) ||
                    p.Patronymic!.Contains(words[0]!) && p.LastName!.Contains(words[1]!) ||
                    p.Patronymic!.Contains(words[1]!) && p.LastName!.Contains(words[0]!) ||
                    p.Passport!.Contains(words[0]!) && p.Passport!.Contains(words[1]));
                else
                {
                    patients = patients.Where(p => p.FirstName!.Contains(name) || p.LastName!.Contains(name) || p.Patronymic!.Contains(name)
                    || p.Passport!.Contains(name) || p.Snils!.Contains(name) || p.Polis!.Contains(name));

                }

            }
            else if (appointments != null)
            {
                if (words.Length == 3)
                    appointments = appointments.Where(p => p.Patient!.FirstName!.Contains(words[0]!) && p.Patient!.Patronymic!.Contains(words[1]!) && p.Patient!.LastName!.Contains(words[2]!) ||
                    p.Patient!.FirstName!.Contains(words[1]!) && p.Patient!.Patronymic!.Contains(words[2]!) && p.Patient!.LastName!.Contains(words[0]!) ||
                    p.Patient!.FirstName!.Contains(words[1]!) && p.Patient!.Patronymic!.Contains(words[0]!) && p.Patient!.LastName!.Contains(words[2]!) ||
                    p.Doctor!.FirstName!.Contains(words[0]!) && p.Doctor!.Patronymic!.Contains(words[1]!) && p.Doctor!.LastName!.Contains(words[2]!) ||
                   p.Doctor!.FirstName!.Contains(words[1]!) && p.Doctor!.Patronymic!.Contains(words[2]!) && p.Doctor!.LastName!.Contains(words[0]!) ||
                    p.Doctor!.FirstName!.Contains(words[1]!) && p.Doctor!.Patronymic!.Contains(words[0]!) && p.Doctor!.LastName!.Contains(words[2]!));
                else if (words.Length == 2)
                {
                    appointments = appointments.Where(p => p.Patient!.FirstName!.Contains(words[0]!) && p.Patient!.LastName!.Contains(words[1]!) ||
                        p.Patient!.FirstName!.Contains(words[1]!) && p.Patient!.LastName!.Contains(words[0]!) ||
                        p.Patient!.FirstName!.Contains(words[0]!) && p.Patient!.Patronymic!.Contains(words[1]!) ||
                        p.Patient!.FirstName!.Contains(words[1]!) && p.Patient!.Patronymic!.Contains(words[0]!) ||
                        p.Patient!.Patronymic!.Contains(words[0]!) && p.Patient!.LastName!.Contains(words[1]!) ||
                        p.Patient!.Patronymic!.Contains(words[1]!) && p.Patient!.LastName!.Contains(words[0]!) ||
                        p.Patient!.Passport!.Contains(words[0]!) && p.Patient!.Passport!.Contains(words[1]) ||
                        p.Doctor!.FirstName!.Contains(words[0]!) && p.Doctor!.LastName!.Contains(words[1]!) ||
                    p.Doctor!.FirstName!.Contains(words[1]!) && p.Doctor!.LastName!.Contains(words[0]!) ||
                    p.Doctor!.FirstName!.Contains(words[0]!) && p.Doctor!.Patronymic!.Contains(words[1]!) ||
                    p.Doctor!.FirstName!.Contains(words[1]!) && p.Doctor!.Patronymic!.Contains(words[0]!) ||
                    p.Doctor!.Patronymic!.Contains(words[0]!) && p.Doctor!.LastName!.Contains(words[1]!) ||
                    p.Doctor!.Patronymic!.Contains(words[1]!) && p.Doctor!.LastName!.Contains(words[0]!));
                }
                else 
                {
                    appointments = appointments.Where(p => p.Patient!.FirstName!.Contains(name) || p.Patient!.Polis!.Contains(name) || p.Patient!.LastName!.Contains(name) || p.Patient!.Patronymic!.Contains(name)
                   || p.Doctor!.FirstName!.Contains(name) || p.Doctor!.LastName!.Contains(name) || p.Doctor!.Patronymic!.Contains(name) || p.Doctor.Profile!.Name!.Contains(name) || p.Id.ToString().Contains(name)
                   );

                }
            }
        }

    }
}
