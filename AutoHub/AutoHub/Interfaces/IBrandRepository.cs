using AutoHub.Data.Entities;
using AutoHub.ViewModels.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Interfaces
{
    public interface IBrandRepository
    {
        List<BrandViewModel> GetBrandList(); // получение всех объектов
        Task<BrandViewModel> GetBrandAsync(int id); // получение одного объекта по id
        Task CreateAsync(Brand item); // создание объекта
        Task UpdateAsync(Brand item); // обновление объекта
        Task DeleteAsync(int id); // удаление объекта по id

        bool BrandExists(int id);

        //void Save();  // сохранение изменений
    }
}
