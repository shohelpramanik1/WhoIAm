using Microsoft.EntityFrameworkCore;
using WhoIAm.Domain.Entities;

namespace WhoIAm.Infrastructure.Data;

public class WhoIAmDbContext : DbContext
{
    public WhoIAmDbContext(DbContextOptions<WhoIAmDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<VirtualIdentity> VirtualIdentities { get; set; } = null!;
    public DbSet<IdentityInterest> IdentityInterests { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostMedia> PostMedia { get; set; } = null!;
    public DbSet<PostReaction> PostReactions { get; set; } = null!;
    public DbSet<PostComment> PostComments { get; set; } = null!;
    public DbSet<PostCommentReaction> PostCommentReactions { get; set; } = null!;
    public DbSet<Community> Communities { get; set; } = null!;
    public DbSet<CommunityMember> CommunityMembers { get; set; } = null!;
    public DbSet<CommunityModerator> CommunityModerators { get; set; } = null!;
    public DbSet<UserFollower> UserFollowers { get; set; } = null!;
    public DbSet<VirtualIdentityFollower> VirtualIdentityFollowers { get; set; } = null!;
    public DbSet<MoodEntry> MoodEntries { get; set; } = null!;
    public DbSet<JournalEntry> JournalEntries { get; set; } = null!;
    public DbSet<JournalTag> JournalTags { get; set; } = null!;
    public DbSet<Enemy> Enemies { get; set; } = null!;
    public DbSet<EnemySession> EnemySessions { get; set; } = null!;
    public DbSet<EnemyMessage> EnemyMessages { get; set; } = null!;
    public DbSet<ReflectionEntry> ReflectionEntries { get; set; } = null!;
    public DbSet<RealLifeMission> RealLifeMissions { get; set; } = null!;
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<MessageAttachment> MessageAttachments { get; set; } = null!;
    public DbSet<ContentReport> ContentReports { get; set; } = null!;
    public DbSet<ModerationAction> ModerationActions { get; set; } = null!;
    public DbSet<ModerationAppeal> ModerationAppeals { get; set; } = null!;
    public DbSet<UserBlock> UserBlocks { get; set; } = null!;
    public DbSet<UserMute> UserMutes { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(32);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.Country).HasMaxLength(100);
        });

        // VirtualIdentity configuration
        modelBuilder.Entity<VirtualIdentity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(32);
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.HasOne(e => e.User)
                .WithMany(u => u.VirtualIdentities)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Post configuration
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).HasMaxLength(5000);
            entity.HasOne(e => e.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.VirtualIdentity)
                .WithMany(v => v.Posts)
                .HasForeignKey(e => e.VirtualIdentityId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Community configuration
        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Follower relationships
        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FollowerId, e.FollowingId }).IsUnique();
            entity.HasOne(e => e.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(e => e.FollowingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Block and Mute relationships
        modelBuilder.Entity<UserBlock>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.BlockerId, e.BlockedId }).IsUnique();
            entity.HasOne(e => e.Blocker)
                .WithMany()
                .HasForeignKey(e => e.BlockerId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Blocked)
                .WithMany()
                .HasForeignKey(e => e.BlockedId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
