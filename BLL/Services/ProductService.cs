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
    public class ProductService
    {
        public static List<ProductDTO> Read()
        {

            var data = DataAccessFactory.ProductData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<ProductDTO>>(data);
            return mapped;
        }

        public static ProductDTO Read(int Id)
        {

            var data = DataAccessFactory.ProductData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<ProductDTO>(data);
            return mapped;
        }

        public static bool Create(ProductDTO productDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ProductDTO, Product>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Product>(productDTO);

            var response = DataAccessFactory.ProductData().Create(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.ProductData().Delete(Id);
            return response;
        }

        public static ProductInventoryLogDTO ProductsWithInventoryLogs(int Id)
        {

            var data = DataAccessFactory.ProductData().Read(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductInventoryLogDTO>();
                c.CreateMap<InventoryLog, InventoryLogDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<ProductInventoryLogDTO>(data);
            return mapped;

        }
        public static ProductOrderItemDTO ProductsWithOrderItems(int Id)
        {

            var data = DataAccessFactory.ProductData().Read(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductOrderItemDTO>();
                c.CreateMap<OrderItem, OrderItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<ProductOrderItemDTO>(data);
            return mapped;

        }
    }
}
