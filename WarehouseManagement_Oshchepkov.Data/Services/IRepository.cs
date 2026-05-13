using System.Collections.Generic;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// ������� ��������� ��� CRUD ��������
    public interface IRepository<T>
    {
        ///�������� ��� ������
        List<T> GetAll();

        ///�������� ������ �� ID
        T? GetById(object id);

        ///�������� ����� ������
        void Add(T entity);

        ///�������� ������������ ������
        void Update(T entity);

        ///������� ������ �� ID 
        void Delete(object id);
    }
}