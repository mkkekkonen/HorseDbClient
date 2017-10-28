using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseDbClient.Models
{
    public class Horse
    {
        public string Name { get; set; }
        public Date DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Breed Breed { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + " DOB: " + DateOfBirth + " Gender: " + Gender + " Breed: " + Breed;
        }
    }
}
