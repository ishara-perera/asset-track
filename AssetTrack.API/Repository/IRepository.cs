namespace AssetTrack.API.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddEntity(T entity);
    
}