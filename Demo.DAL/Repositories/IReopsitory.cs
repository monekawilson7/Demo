namespace Demo.DAL.Repositories;
public interface IReopsitory <TEntity> where TEntity : BaseEntitiy
{
    IEnumerable<TEntity> GetAll(bool trackChanges = false);
    TEntity? GetById(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
