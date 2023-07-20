using AppCRUDSerfinsa.Models;

// Funciona como una normalización de métodos 

namespace AppCRUDSerfinsa.Repositorios.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Lista();
        Task<bool> Guardar(T model);
        Task<bool> Editar(T model);
        Task<bool> Eliminar(int id);
    }   
}