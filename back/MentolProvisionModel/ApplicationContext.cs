using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace MentolProvisionModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ActionList> ActionLists { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ObjectList> ObjectLists { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageToRole> PagesToRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerTask> Tasks { get; set; }
        public DbSet<TasksHistory> TasksHistories { get; set; }
        public DbSet<TasksList> TasksLists { get; set; }
        public DbSet<TasksPool> TasksPools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToRole> UsersToRoles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorModel> VendorModels { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Version> Versions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionList>().ToTable("ActionList");
            modelBuilder.Entity<Device>().ToTable("Devices");
            modelBuilder.Entity<ObjectList>().ToTable("ObjectList");
            modelBuilder.Entity<Page>().ToTable("Pages");
            modelBuilder.Entity<PageToRole>().ToTable("PageToRole").HasKey(c => new {c.PageId, c.RoleId });
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().HasMany(x => x.UserToRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).IsRequired();
            modelBuilder.Entity<RoleAction>().ToTable("RoleAction");
            modelBuilder.Entity<Server>().ToTable("Servers");
            modelBuilder.Entity<ServerTask>().ToTable("Tasks");
            modelBuilder.Entity<TasksHistory>().ToTable("TasksHistory");
            modelBuilder.Entity<TasksList>().ToTable("Taskslist");
            modelBuilder.Entity<TasksPool>().ToTable("TasksPool");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasMany(x => x.UserToRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();
            modelBuilder.Entity<UserToRole>().ToTable("UserToRole");
            modelBuilder.Entity<UserToRole>().HasKey(c => new { c.UserId, c.RoleId });
            modelBuilder.Entity<Vendor>().ToTable("Vendors");
            modelBuilder.Entity<VendorModel>().ToTable("VendorModels");
            modelBuilder.Entity<Line>().ToTable("Lines");
            modelBuilder.Entity<ProductInfo>().ToView("vProducts").HasNoKey();

            modelBuilder.Entity<Vendor>().HasMany(x => x.VendorModels)
	            .WithOne(x => x.Vendor)
	            .HasForeignKey(x => x.VendorId)
	            .IsRequired(false);

            var versionTypeBuilder = modelBuilder.Entity<Version>();

            versionTypeBuilder.ToTable("Version");
            versionTypeBuilder.Property(x => x.VersionValue).HasColumnName("Version");
            versionTypeBuilder.Property(x => x.IsLastRecord).HasColumnName("LastRecord");
            versionTypeBuilder.HasOne(x => x.Server)
	            .WithMany(x => x.Versions)
	            .HasForeignKey(x => x.ServerId)
	            .IsRequired(false);

            versionTypeBuilder.HasOne(x => x.Node)
	            .WithMany(x => x.Versions)
	            .HasForeignKey(x => x.NodeId)
	            .IsRequired(false);
        }
    }
}