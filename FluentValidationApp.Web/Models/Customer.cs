using System.Collections.Generic;

namespace FluentValidationApp.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public IList<Address> Address { get; set; }
        public Gender Gender { get; set; }


        public string GetFullName()//Burda basına Get yazarsak CustomerDto daki FullName ile direk mapler
         {
            return $"{Name}-{Email}-{Age}";
        }
    }
}
