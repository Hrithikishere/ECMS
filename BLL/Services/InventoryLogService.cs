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
    public class InventoryLogService
    {
        public static List<InventoryLogDTO> Read()
        {

            var data = DataAccessFactory.InventoryLogData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<InventoryLog, InventoryLogDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<InventoryLogDTO>>(data);
            return mapped;
        }

        public static InventoryLogDTO Read(int Id)
        {

            var data = DataAccessFactory.InventoryLogData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<InventoryLog, InventoryLogDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<InventoryLogDTO>(data);
            return mapped;
        }

        public static bool Create(InventoryLogDTO inventoryLogDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<InventoryLogDTO, InventoryLog>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<InventoryLog>(inventoryLogDTO);

            var response = DataAccessFactory.InventoryLogData().Create(mapped);
            return response;
        }
        public static bool Update(InventoryLogDTO inventoryLogDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<InventoryLogDTO, InventoryLog>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<InventoryLog>(inventoryLogDTO);

            var response = DataAccessFactory.InventoryLogData().Update(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.InventoryLogData().Delete(Id);
            return response;
        }

    }
}
