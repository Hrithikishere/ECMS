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
    public class CartItemService
    {
        public static List<CartItemDTO> Read()
        {

            var data = DataAccessFactory.CartItemData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CartItem, CartItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<CartItemDTO>>(data);
            return mapped;
        }

        public static CartItemDTO Read(int Id)
        {

            var data = DataAccessFactory.CartItemData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CartItem, CartItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CartItemDTO>(data);
            return mapped;
        }

        public static bool Create(CartItemDTO cartItemDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CartItemDTO, CartItem>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CartItem>(cartItemDTO);

            var response = DataAccessFactory.CartItemData().Create(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.CartItemData().Delete(Id);
            return response;
        }
    }
}
