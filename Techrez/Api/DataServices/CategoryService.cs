using Api.Dto.Post;
using Api.Dto.Put;
using Dal;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataServices
{
    public interface ICategoryService
    {
        Task<object> GetAllCategoriesAsync();
    }

    public class CategoryService : ICategoryService
    {
        private readonly IDalService _dal;

        public CategoryService(IDalService dal)
        {
            _dal = dal;
        }

        public async Task<object> GetAllCategoriesAsync()
        {
            var om = await _dal.GetCategoriesAsync();
            return om.Select(x => new { x.Id, x.Description });
        }
    }
}