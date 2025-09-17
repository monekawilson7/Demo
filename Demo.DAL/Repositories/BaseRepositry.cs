namespace Demo.DAL.Repositories;
public class BaseRepositry<TEntity>(CompanyDBContext context) : IReopsitory<TEntity> where TEntity : BaseEntitiy
{
    protected DbSet<TEntity> _dbSet = context.Set<TEntity>();

    //public TEntityRepository(CompanyDBContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    public virtual int Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return context.SaveChanges();
    }

    public virtual int Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        _dbSet.Remove(entity);
        return context.SaveChanges();
    }

    public virtual int Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return context.SaveChanges();
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
