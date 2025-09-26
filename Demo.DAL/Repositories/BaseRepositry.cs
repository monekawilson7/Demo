namespace Demo.DAL.Repositories;
public class BaseRepositry<TEntity>(CompanyDBContext context) : IReopsitory<TEntity> where TEntity : BaseEntitiy
{
    protected DbSet<TEntity> _dbSet = context.Set<TEntity>();

    //public TEntityRepository(CompanyDBContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        _dbSet.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    public virtual IEnumerable<TEntity> GetAll(bool trackChanges = false)
    {
        return trackChanges ?
            _dbSet.ToList() :
            _dbSet.AsNoTracking()
            .ToList();
    }

    public virtual TEntity? GetById(int id)
    {
        return _dbSet.Find(id);
    }
}
