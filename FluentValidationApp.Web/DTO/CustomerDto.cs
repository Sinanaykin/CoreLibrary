using System;

namespace FluentValidationApp.Web.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Isım { get; set; }
        public string Eposta { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
        public string CreditCardNumber { get; set; }//Burda classın adı yani CreditCard ve devamında property adı verirsek Number gibi automapper otomatik maplar

        public string Number { get; set; }//yada CreditCard isimiyle burayı aynı vericez yani Number ve mappin tarafına IncludeMember ile CreditCard classını dahil edicez
          // CreateMap<Customer, CustomerDto>().IncludeMembers(x=>x.CreditCard)

    }
}
