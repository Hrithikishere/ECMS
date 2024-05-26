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

        public static List<OrderOrderItemsDTO> OrderWithOrderItems()
        {

            var data = DataAccessFactory.OrderData().Read();

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderOrderItemsDTO>();
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<OrderOrderItemsDTO>>(data);
            return mapped;

        }
        public static OrderOrderItemsDTO OrderWithOrderItems(int Id)
        {

            var data = DataAccessFactory.OrderData().Read(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderOrderItemsDTO>();
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<OrderOrderItemsDTO>(data);
            return mapped;

        }

        public static bool UpdateOrder(OrderOrderItemsDTO orderOrderItemsDTO)
        {
            // Retrieve the existing order
            var existingOrder = DataAccessFactory.OrderData().Read(orderOrderItemsDTO.Id);
            if (existingOrder == null)
            {
                throw new Exception($"Order with Id {orderOrderItemsDTO.Id} does not exist.");
            }

            existingOrder.CustomerId = orderOrderItemsDTO.CustomerId;
            existingOrder.OrderDate = orderOrderItemsDTO.OrderDate;
            existingOrder.TotalAmount = orderOrderItemsDTO.TotalAmount;
            existingOrder.Status = orderOrderItemsDTO.Status;
            existingOrder.ShippingAddress = orderOrderItemsDTO.ShippingAddress;
            existingOrder.BillingAddress = orderOrderItemsDTO.BillingAddress;

            var existingOrderItems = existingOrder.OrderItems.ToList();
            var updatedOrderItems = orderOrderItemsDTO.OrderItems;

            foreach (var itemDTO in updatedOrderItems)
            {
                var existingItem = existingOrderItems.FirstOrDefault(i => i.Id == itemDTO.Id);
                if (existingItem != null)
                {
                    existingItem.ProductId = itemDTO.ProductId;
                    existingItem.Quantity = itemDTO.Quantity;
                    existingItem.UnitPrice = itemDTO.UnitPrice;
                    existingItem.TotalPrice = itemDTO.TotalPrice;
                }
                else
                {
                    OrderItem newItem = new OrderItem
                    {
                        OrderId = existingOrder.Id,
                        ProductId = itemDTO.ProductId,
                        Quantity = itemDTO.Quantity,
                        UnitPrice = itemDTO.UnitPrice,
                        TotalPrice = itemDTO.TotalPrice
                    };
                    existingOrder.OrderItems.Add(newItem);
                }
            }

            foreach (var existingItem in existingOrderItems)
            {
                if (!updatedOrderItems.Any(i => i.Id == existingItem.Id))
                {
                    existingOrder.OrderItems.Remove(existingItem);
                }
            }
            var response = DataAccessFactory.OrderData().Update(existingOrder);
            return response;
        }


    }
}
