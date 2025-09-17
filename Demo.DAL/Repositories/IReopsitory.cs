namespace Demo.DAL.Repositories;
public interface IReopsitory <TEntity> where TEntity : BaseEntitiy
{
    IEnumerable<TEntity> GetAll(bool trackChanges = false);
    TEntity? GetById(int id);
    int Add(TEntity entity);
    int Update(TEntity entity);
    int Delete(TEntity entity);
}
