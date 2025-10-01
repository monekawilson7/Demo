
using System.Threading.Tasks;

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
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
    {
        return trackChanges ?
           await _dbSet.ToListAsync() :
           await _dbSet.AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}
