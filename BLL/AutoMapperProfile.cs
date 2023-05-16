using AutoMapper;
using DAL.Models;
using BLL.DTO;

namespace BLL
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Good, GoodModel>()

                .ForMember(dest => dest.TypeOfGood, opt => opt.MapFrom(src => src.TypeOfGood.Name))
                .ReverseMap();

            CreateMap<GoodsRequest, Good>();
            CreateMap<GoodsRequestToCreate, Good>();


            CreateMap<TypeOfGood, TypeOfGoodModel>()
            .ReverseMap();

            CreateMap<TypeOfGoodRequest, TypeOfGood>();


            CreateMap<CustomerRequest, Customer>();

            CreateMap<Customer, CustomerModel>()
             .ReverseMap();

            CreateMap<OrderRequest, Order>();

            CreateMap<OrderGoodRequest, Order>();

            CreateMap<Order, OrderModel>()
           .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname))
           .ForMember(dest => dest.StatusOfOrder, opt => opt.MapFrom(src => src.StatusOfOrder.ToString()))
           .ForMember(dest => dest.Goods, opt => opt.MapFrom(src => src.Goods.Name))
           .ReverseMap();

            CreateMap<QueueRequest, Queue>();

            CreateMap<Queue, QueueOrderRequest>()
         .ReverseMap();

            CreateMap<Queue, QueueModel>()
           .ForMember(dest => dest.Good, opt => opt.MapFrom(src => src.Orders.Goods.Name))
           .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Orders.Customer.Name + " " + src.Orders.Customer.Surname))
         .ReverseMap();
        }


    }
}