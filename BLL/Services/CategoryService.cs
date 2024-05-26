using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService
    {
        public static List<CategoryDTO> Read()
        {

            var data = DataAccessFactory.CategoryData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<CategoryDTO>>(data);
            return mapped;
        }

        public static CategoryDTO Read(int Id)
        {

            var data = DataAccessFactory.CategoryData().Read(Id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CategoryDTO>(data);
            return mapped;
        }

        public static bool Create(CategoryDTO categoryDTO)
        {
            
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CategoryDTO, Category>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Category>(categoryDTO);

            var response = DataAccessFactory.CategoryData().Create(mapped);
            return response;
        }
        public static bool Update(CategoryDTO categoryDTO)
        {
            
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CategoryDTO, Category>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Category>(categoryDTO);

            var response = DataAccessFactory.CategoryData().Update(mapped);
            return response;
        }
        public static bool Delete(int Id)
        {
            var response = DataAccessFactory.CategoryData().Delete(Id);
            return response;
        }
    
        public static CategoryProductDTO CategoryWithProducts(int Id)
        {

            var data = DataAccessFactory.CategoryData().Read(Id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryProductDTO>();
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CategoryProductDTO>(data);
            return mapped;

        }
    }
}
