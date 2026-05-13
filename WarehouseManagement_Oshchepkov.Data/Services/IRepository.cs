using System.Collections.Generic;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// Базовый интерфейс для CRUD операций
    public interface IRepository<T>
    {
        ///Получить все записи
        List<T> GetAll();

        ///Получить запись по ID
        T? GetById(object id);

        ///Добавить новую запись
        void Add(T entity);

        ///Обновить существующую запись
        void Update(T entity);

        ///Удалить запись по ID 
        void Delete(object id);
    }
}