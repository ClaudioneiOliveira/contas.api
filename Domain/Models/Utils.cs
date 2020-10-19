using System;

namespace contas.api.Domain.Models
{
    public class Utils
    {
        public static bool IsValidDate(DateTime date)
        {
            try
            {
                var strDate = date.ToShortDateString();
                DateTime test = new DateTime();
                DateTime dt = DateTime.Parse(strDate);
                if (date == test)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}