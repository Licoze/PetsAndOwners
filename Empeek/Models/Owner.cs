using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empeek.Models
{
    public class Owner
    {
        public Owner()
        {
            Pets = new List<Pet>();
        }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
    }
}