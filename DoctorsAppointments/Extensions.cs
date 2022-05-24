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

        public static bool Contains1(this string source, string toCheck)
        {
            return source.IndexOf(toCheck) >= 0;
        }
    }
}
