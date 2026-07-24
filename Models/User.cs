using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace INFASS.Models
{
    public class User
    {
        public string sql(string[] values, string[] numbers)
        {
            string val = "";
            string num = "";
            string table = "Users";

            for (int i = 0; i < values.Length; i++)
            {
                val += values[i];

                if (i < values.Length - 1)
                {
                    val += ", ";
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                if (int.TryParse(numbers[i], out _))
                {
                    num += numbers[i];

                }

                else
                {
                    num += $"'{numbers[i]}'";
                }

                if (i < numbers.Length - 1)
                {
                    num += ", ";
                }
            }

            return $"INSERT INTO {table} ({val}) VALUES ({num});";

        }
    }
}
