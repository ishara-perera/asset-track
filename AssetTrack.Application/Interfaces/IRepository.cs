namespace AssetTrack.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddEntity(T entity);
    
}