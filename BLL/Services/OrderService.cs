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
    public class OrderService
    {
        public static List<OrderDTO> Read()
        {

            var data = DataAccessFactory.OrderData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<OrderDTO>>(data);
            return mapped;
        }

        public static OrderDTO Read(int Id)
        {

            var data = DataAccessFactory.OrderData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<OrderDTO>(data);
            return mapped;
        }

        public static bool Create(OrderDTO orderDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderDTO, Order>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Order>(orderDTO);

            var response = DataAccessFactory.OrderData().Create(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.OrderData().Delete(Id);
            return response;
        }

        public static bool PlaceOrder(OrderOrderItemsDTO orderOrderItemsDTO)
        {
            Order order = new Order();
            order.CustomerId = orderOrderItemsDTO.CustomerId;
            order.OrderDate = orderOrderItemsDTO.OrderDate;
            order.TotalAmount  = orderOrderItemsDTO.TotalAmount;
            order.Status = orderOrderItemsDTO.Status;
            order.ShippingAddress = orderOrderItemsDTO.ShippingAddress;
            order.BillingAddress = orderOrderItemsDTO.BillingAddress;
            foreach(var item in orderOrderItemsDTO.OrderItems)
            {
                OrderItem orderItem = new OrderItem();

                orderItem.OrderId = 0;
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                orderItem.UnitPrice = item.UnitPrice;
                orderItem.TotalPrice = item.TotalPrice;
                
                order.OrderItems.Add(orderItem);
            }

            /*var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderOrderItemsDTO, Order>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Order>(orderOrderItemsDTO);*/
            var response = DataAccessFactory.OrderData().Create(order);
            return response;

        }
    }
}
