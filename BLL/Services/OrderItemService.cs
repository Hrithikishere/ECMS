using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderItemService
    {
        public static List<OrderItemDTO> Read()
        {

            var data = DataAccessFactory.OrderItemData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<OrderItemDTO>>(data);
            return mapped;
        }

        public static OrderItemDTO Read(int Id)
        {

            var data = DataAccessFactory.OrderItemData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<OrderItemDTO>(data);
            return mapped;
        }

        public static bool Create(OrderItemDTO orderItemDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderItemDTO, OrderItem>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<OrderItem>(orderItemDTO);

            var response = DataAccessFactory.OrderItemData().Create(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.OrderItemData().Delete(Id);
            return response;
        }


        //---------Place Order
        /*

        public static bool PlaceOrder(OrderOrderItemsDTO orderOrderItemsDTO)
        {

            var data = DataAccessFactory.OrderData().Create(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductOrderItemDTO>();
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<ProductOrderItemDTO>(data);
            return mapped;

        }
        */
    }
}
