using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_2019
{

    public enum Position { Goalkeeper, Defender, Midfielder, Forward}
    public class Player
    {
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public Position PreferedPosition { get; set; }

        public DateTime DateOfBirth { get; set; }

        private int age;

        public int Age
        {
            get 
            {
                age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth.DayOfYear >= DateTime.Now.DayOfYear)
                    age--;
                return age;
            }
        }

        public override string ToString()
        {
            return $"{FirstName}{SurName}({Age}){PreferedPosition.ToString().ToUpper()}";
        }
    }
}
