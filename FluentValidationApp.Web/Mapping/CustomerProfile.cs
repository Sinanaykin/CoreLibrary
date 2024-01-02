using AutoMapper;
using FluentValidationApp.Web.DTO;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();//Buraya revermap metodunu eklersek tersi içinde mapleme yapar aşağıdakini yazmaya gerek kalmaz
                                                            // CreateMap<CustomerDto, Customer>();

            //  eğer property isimleri farklı ise customer ve customerdto da farklı olan propertler aşağıdaki gibi mapleme  yapılır 

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Isım, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Eposta, opt => opt.MapFrom(x => x.Email));
        }
    }
}
