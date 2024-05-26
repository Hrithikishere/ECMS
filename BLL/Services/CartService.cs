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

        public static List<CartCartItemDTO> CartWithCartItems()
        {
            var data = DataAccessFactory.CartData().Read();

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Cart, CartCartItemDTO>();
                c.CreateMap<CartItem, CartItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<CartCartItemDTO>>(data);
            return mapped;
        }

        public static CartCartItemDTO CartWithCartItems(int Id)
        {
            var data = DataAccessFactory.CartData().Read(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Cart, CartCartItemDTO>();
                c.CreateMap<CartItem, CartItemDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CartCartItemDTO>(data);
            return mapped;
        }

        public static bool AddToCart(CartCartItemDTO cartCartItemDTO)
        {

            Cart cart = new Cart
            {
                CustomerId = cartCartItemDTO.CustomerId
            };

            foreach (var itemDTO in cartCartItemDTO.CartItems)
            {
                CartItem cartItem = new CartItem
                {
                    CartId = 0,
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity

                };
                cart.CartItems.Add(cartItem);
            }

            var response = DataAccessFactory.CartData().Create(cart);
            return response;
        }


        public static bool UpdateCart(CartCartItemDTO cartCartItemDTO)
        {
            var existingCart = DataAccessFactory.CartData().Read(cartCartItemDTO.Id);
            if (existingCart == null)
            {
                throw new Exception($"Cart with Id {cartCartItemDTO.Id} does not exist.");
            }

            existingCart.CustomerId = cartCartItemDTO.CustomerId;

            var existingCartItems = existingCart.CartItems.ToList();
            var updatedCartItems = cartCartItemDTO.CartItems;

            foreach (var itemDTO in updatedCartItems)
            {
                var existingItem = existingCartItems.FirstOrDefault(i => i.Id == itemDTO.Id);
                if (existingItem != null)
                {
                    existingItem.ProductId = itemDTO.ProductId;
                    existingItem.Quantity = itemDTO.Quantity;
                }
                else
                {
                    CartItem newItem = new CartItem
                    {
                        CartId = existingCart.Id,
                        ProductId = itemDTO.ProductId,
                        Quantity = itemDTO.Quantity
                    };
                    existingCart.CartItems.Add(newItem);
                }
            }

            foreach (var existingItem in existingCartItems)
            {
                if (!updatedCartItems.Any(i => i.Id == existingItem.Id))
                {
                    existingCart.CartItems.Remove(existingItem);
                }
            }

            var response = DataAccessFactory.CartData().Update(existingCart);
            return response;
        }
    }
}
