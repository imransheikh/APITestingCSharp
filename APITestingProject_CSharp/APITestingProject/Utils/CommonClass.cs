using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestingProject.Utils
{
    class CommonClass
    {
        public int getRandomNumber(int digit)
        {
            Random random = new Random();
            string numbers = "0123456789";
            StringBuilder builder = new StringBuilder(digit);
            string numberAsString = "";
            int numberAsNumber = 0;

            for (var i = 0; i < digit; i++)
            {
                builder.Append(numbers[random.Next(0, numbers.Length)]);
            }
            numberAsString = builder.ToString();
            numberAsNumber = int.Parse(numberAsString);
            Console.WriteLine("Random is 1:" + numberAsNumber);
            return numberAsNumber;
        }
    }
}
