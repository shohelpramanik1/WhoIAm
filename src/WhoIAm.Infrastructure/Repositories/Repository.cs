using Microsoft.EntityFrameworkCore;
using WhoIAm.Domain.Entities;
using WhoIAm.Infrastructure.Data;

namespace WhoIAm.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly WhoIAmDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(WhoIAmDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

public class UserRepository : Repository<User>
{
    public UserRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .Include(u => u.VirtualIdentities)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .Include(u => u.VirtualIdentities)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}

public class VirtualIdentityRepository : Repository<VirtualIdentity>
{
    public VirtualIdentityRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<VirtualIdentity?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .Include(v => v.Interests)
            .FirstOrDefaultAsync(v => v.Username == username);
    }

    public async Task<List<VirtualIdentity>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(v => v.UserId == userId && !v.IsDeleted)
            .Include(v => v.Interests)
            .ToListAsync();
    }
}

public class PostRepository : Repository<Post>
{
    public PostRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<List<Post>> GetFeedAsync(Guid userId, int skip, int take)
    {
        return await _dbSet
            .Where(p => !p.IsDeleted && p.Visibility.ToString() == "Public")
            .OrderByDescending(p => p.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Include(p => p.Media)
            .Include(p => p.VirtualIdentity)
            .ToListAsync();
    }

    public async Task<int> GetFeedCountAsync()
    {
        return await _dbSet
            .Where(p => !p.IsDeleted && p.Visibility.ToString() == "Public")
            .CountAsync();
    }
}

public class CommunityRepository : Repository<Community>
{
    public CommunityRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<Community?> GetBySlugAsync(string slug)
    {
        return await _dbSet
            .Include(c => c.Members)
            .Include(c => c.Moderators)
            .FirstOrDefaultAsync(c => c.Slug == slug && !c.IsDeleted);
    }

    public async Task<List<Community>> GetActiveCommunitiesAsync(int skip, int take)
    {
        return await _dbSet
            .Where(c => !c.IsDeleted && c.IsActive)
            .OrderByDescending(c => c.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}

public class MoodRepository : Repository<MoodEntry>
{
    public MoodRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<List<MoodEntry>> GetRecentMoodsAsync(Guid userId, Guid identityId, int days)
    {
        var since = DateTime.UtcNow.AddDays(-days);
        return await _dbSet
            .Where(m => m.UserId == userId && m.VirtualIdentityId == identityId && m.CreatedAt >= since)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }
}

public class JournalRepository : Repository<JournalEntry>
{
    public JournalRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<List<JournalEntry>> GetUserEntriesAsync(Guid userId, Guid identityId, int skip, int take)
    {
        return await _dbSet
            .Where(j => j.UserId == userId && j.VirtualIdentityId == identityId && !j.IsDeleted)
            .OrderByDescending(j => j.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Include(j => j.Tags)
            .ToListAsync();
    }
}

public class EnemyRepository : Repository<Enemy>
{
    public EnemyRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<List<Enemy>> GetUserEnemiesAsync(Guid userId, Guid identityId, int skip, int take)
    {
        return await _dbSet
            .Where(e => e.UserId == userId && e.VirtualIdentityId == identityId && !e.IsDeleted)
            .OrderByDescending(e => e.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}

public class MissionRepository : Repository<RealLifeMission>
{
    public MissionRepository(WhoIAmDbContext context) : base(context) { }

    public async Task<List<RealLifeMission>> GetUserMissionsAsync(Guid userId, Guid identityId, int skip, int take)
    {
        return await _dbSet
            .Where(m => m.UserId == userId && m.VirtualIdentityId == identityId && !m.IsDeleted)
            .OrderByDescending(m => m.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetCompletedMissionCountAsync(Guid userId)
    {
        return await _dbSet
            .Where(m => m.UserId == userId && m.IsCompleted)
            .CountAsync();
    }
}
