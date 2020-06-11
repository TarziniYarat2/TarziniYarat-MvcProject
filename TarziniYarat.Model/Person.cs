using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarziniYarat.Core.Model;

namespace TarziniYarat.Model
{
    public class Person:BaseEntity
    {
        public Person()
        {
            Likes = new HashSet<Like>();
            Orders = new HashSet<Order>();
            PersonDetails = new HashSet<PersonDetails>();
        }
        public int PersonID { get; set; }
        public int RoleID { get; set; }       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCKN { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //Navigation prop
        public Role Role { get; set; }
        public ICollection<PersonDetails> PersonDetails { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
