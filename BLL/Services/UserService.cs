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
    public class UserService
    {
        public static List<UserDTO> Read()
        {

            var data = DataAccessFactory.UserData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<UserDTO>>(data);
            return mapped;
        }

        public static UserDTO Read(int Id)
        {

            var data = DataAccessFactory.UserData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<UserDTO>(data);
            return mapped;
        }

        public static bool Create(UserDTO userDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<UserDTO, User>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<User>(userDTO);

            var response = DataAccessFactory.UserData().Create(mapped);
            return response;
        }

        public static bool Update(UserDTO userDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<UserDTO, User>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<User>(userDTO);

            var response = DataAccessFactory.UserData().Update(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.UserData().Delete(Id);
            return response;
        }



        public static List<UserCartDTO> UsersWithCartItems()
        {

            var data = DataAccessFactory.UserData().Read();

            List<UserCartDTO> listOfUserCartDTOs = new List<UserCartDTO>();

            foreach(var item in data)
            {
                UserCartDTO userCartDTO = new UserCartDTO();
                userCartDTO.Id = item.Id;
                userCartDTO.FirstName = item.FirstName;
                userCartDTO.LastName = item.LastName;
                userCartDTO.Email = item.Email;
                userCartDTO.Address = item.Address;
                userCartDTO.JoinDate = item.JoinDate;
                userCartDTO.Phone = item.Phone;
                userCartDTO.Role = item.Role;
                userCartDTO.Password = item.Password;

                foreach(var item2 in item.Carts)
                {
                    CartDTO cartDTO = new CartDTO();
                    cartDTO.Id = item2.Id;
                    cartDTO.CustomerId = item2.CustomerId;

                    userCartDTO.Carts.Add(cartDTO);
                }
                listOfUserCartDTOs.Add(userCartDTO);
            }
            return listOfUserCartDTOs;

        }
        public static UserCartDTO UsersWithCartItems(int Id)
        {

            var data = DataAccessFactory.UserData().Read(Id);

            UserCartDTO userCartDTO = new UserCartDTO();
            userCartDTO.Id = data.Id;
            userCartDTO.FirstName = data.FirstName;
            userCartDTO.LastName = data.LastName;
            userCartDTO.Email = data.Email;
            userCartDTO.Address = data.Address;
            userCartDTO.JoinDate = data.JoinDate;
            userCartDTO.Phone = data.Phone;
            userCartDTO.Role = data.Role;
            userCartDTO.Password = data.Password;

            foreach (var item2 in data.Carts)
            {
                CartDTO cartDTO = new CartDTO();
                cartDTO.Id = item2.Id;
                cartDTO.CustomerId = item2.CustomerId;

                userCartDTO.Carts.Add(cartDTO);
            }

            return userCartDTO;
        }
        public static List<UserOrderDTO> UsersWithOrderItems()
        {

            var data = DataAccessFactory.UserData().Read();

            List<UserOrderDTO> listOfUserOrderDTOs = new List<UserOrderDTO>();

            foreach (var item in data)
            {
                UserOrderDTO userOrderDTO = new UserOrderDTO();
                userOrderDTO.Id = item.Id;
                userOrderDTO.FirstName = item.FirstName;
                userOrderDTO.LastName = item.LastName;
                userOrderDTO.Email = item.Email;
                userOrderDTO.Address = item.Address;
                userOrderDTO.JoinDate = item.JoinDate;
                userOrderDTO.Phone = item.Phone;
                userOrderDTO.Role = item.Role;
                userOrderDTO.Password = item.Password;

                foreach (var item2 in item.Orders)
                {
                    OrderDTO orderDTO = new OrderDTO();
                    orderDTO.Id = item2.Id;
                    orderDTO.CustomerId = item2.CustomerId;

                    userOrderDTO.Orders.Add(orderDTO);
                }
                listOfUserOrderDTOs.Add(userOrderDTO);
            }
            return listOfUserOrderDTOs;

        }
        public static UserOrderDTO UsersWithOrderItems(int Id)
        {

            var data = DataAccessFactory.UserData().Read(Id);

            UserOrderDTO userOrderDTO = new UserOrderDTO();
            userOrderDTO.Id = data.Id;
            userOrderDTO.FirstName = data.FirstName;
            userOrderDTO.LastName = data.LastName;
            userOrderDTO.Email = data.Email;
            userOrderDTO.Address = data.Address;
            userOrderDTO.JoinDate = data.JoinDate;
            userOrderDTO.Phone = data.Phone;
            userOrderDTO.Role = data.Role;
            userOrderDTO.Password = data.Password;

            foreach (var item2 in data.Orders)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO.Id = item2.Id;
                orderDTO.CustomerId = item2.CustomerId;

                userOrderDTO.Orders.Add(orderDTO);
            }

            return userOrderDTO;
        }
    }
}
