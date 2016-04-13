using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegularExpressions
{
    class Format
    {
        public static string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string formatZipCode(string number)
        {
            Regex zip = new Regex("[-]");
            number = zip.Replace(number, "");
            number = String.Format("{0:#####-####}", Convert.ToInt64(number));
            return number;
        }

        public static string formatPhone(string number)
        {
            Regex phone = new Regex("[-() ]");
            number = phone.Replace(number, "");
            number = String.Format("{0:(###) ###-####}", Convert.ToInt64(number));
            return number;
        }

        public static string formatSSN(string number)
        {
            Regex ssn = new Regex("[-]");
            number = ssn.Replace(number, "");
            number = String.Format("{0:###-##-####}", Convert.ToInt64(number));
            return number;
        }
    }
}
