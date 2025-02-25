using System.Globalization;
using System.Text.RegularExpressions;
using Utilities.Framework.Exceptions;

namespace Utilities.Framework
{
    public static class ValidationExtensions
    {
        //public static string ConvertPersianNumberstoEnglish(String input)
        //{
        //    var text = input;
        //    text = text.Replace("٠", "0").Replace("١", "1").Replace("٢", "2")
        //           .Replace("٣", "3").Replace("٤", "4").Replace("٥", "5").Replace("٦", "6").Replace("٧", "7").Replace("٨", "8").Replace("٩", "9");
        //    return text;
        //}
        public static long ToShamsiLong(this DateTime input)
        {
            var persianCalendar = new PersianCalendar();
            var bithDate = Convert.ToInt64(persianCalendar.GetYear(input) + "" + (persianCalendar.GetMonth(input).ToString().Length == 1 ? "0" + persianCalendar.GetMonth(input).ToString() : persianCalendar.GetMonth(input).ToString()) + "" + (persianCalendar.GetDayOfMonth(input).ToString().Length == 1 ? "0" + persianCalendar.GetDayOfMonth(input).ToString() : persianCalendar.GetDayOfMonth(input).ToString()));
            return bithDate;
        }
        public static string ToShamsiString(this DateTime input)
        {
            var persianCalendar = new PersianCalendar();
            //var birthDate = Convert.ToDateTime(input);
            var birthDate = persianCalendar.GetYear(input) + "-" + (persianCalendar.GetMonth(input).ToString().Length == 1 ? "0" + persianCalendar.GetMonth(input).ToString() : persianCalendar.GetMonth(input).ToString()) + "-" + (persianCalendar.GetDayOfMonth(input).ToString().Length == 1 ? "0" + persianCalendar.GetDayOfMonth(input).ToString() : persianCalendar.GetDayOfMonth(input).ToString());

            return birthDate;
        }
        public static long ToMilisecond(this DateTime dateTime)
        {
            var dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = Convert.ToInt64(Math.Round((dateTime2.ToUniversalTime() - epoch).TotalMilliseconds));
            return unixDateTime;
        }
        public static long TimeToUnix(this DateTime MyDate)
        {
            DateTime sTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(MyDate - sTime).TotalSeconds;
        }
        public static void ValidateMobile(this string input)
        {
            if (input.StartsWith("0"))
                if (Regex.IsMatch(input, "^09([01239])([0-9]{8})$"))
                    return;
            throw new ArgumentException("شماره صحیح نیست");
        }
        public static string ToMobile(this string input)
        {
            if (input != null)
            {
                if (!input.StartsWith("0"))
                    input = "0" + input;
                if (input.StartsWith("0098"))
                    input = "0" + input.Remove(0, 4);
                if (input.StartsWith("098"))
                    input = "0" + input.Remove(0, 3);
                if (input.StartsWith("+98"))
                    input = "0" + input.Remove(0, 3);
                if (input.StartsWith("98"))
                    input = "0" + input.Remove(0, 2);
            }
            input.ValidateMobile();
            return input;
        }
        public static bool IsValid(this DateTime input)
        {
            var a = ((input > DateTime.Now.AddYears(100)) || (input < DateTime.Now.AddYears(-100)));
            return a;
        }
        public static void ValidateBirthDate(this DateTime? input)
        {
            if (input > DateTime.Now)
                throw new ArgumentException("تاریخ صحیح نیست");
            //if (input > DateTime.Now.AddYears(-18))
            //    throw new ArgumentException("زیر سن قانونی");
            if (input < DateTime.Now.AddYears(-150))
                throw new ArgumentException("تاریخ صحیح نیست");
        }
        public static void ValidateBirthDate(this DateTime input)
        {
            if (input > DateTime.Now)
                throw new ArgumentException("تاریخ صحیح نیست");
            //if (input > DateTime.Now.AddYears(-18))
            //    throw new ArgumentException("زیر سن قانونی");
            if (input < DateTime.Now.AddYears(-150))
                throw new ArgumentException("تاریخ صحیح نیست");
        }

        public static bool ValidatePasword(string password)
        {
            if (password.Length < 8)
            {
                throw new AppException("پسورد باید حداقل دارای 8 کاراکتر باشد");
            }
            if (!password.Any(x => char.IsDigit(x)))
            {
                throw new AppException("پسورد باید حداقل دارای یک یا چند عدد باشد");
            }
            if (!password.Any(x => char.IsLetter(x)))
            {
                throw new AppException("پسورد باید حداقل دارای یک یا چند حرف باشد");
            }
            return true;
        }



    }
}
