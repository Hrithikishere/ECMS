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
    public class CartService
    {
        public static List<CartDTO> Read()
        {

            var data = DataAccessFactory.CartData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Cart, CartDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<CartDTO>>(data);
            return mapped;
        }

        public static CartDTO Read(int Id)
        {

            var data = DataAccessFactory.CartData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Cart, CartDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CartDTO>(data);
            return mapped;
        }

        public static bool Create(CartDTO cartDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CartDTO, Cart>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Cart>(cartDTO);

            var response = DataAccessFactory.CartData().Create(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.CartData().Delete(Id);
            return response;
        }
    }
}
