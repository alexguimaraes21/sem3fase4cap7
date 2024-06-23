using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Context
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<TruckModel> Trucks { get; set; }
        public virtual DbSet<ContainerModel> Containers { get; set; }
        public virtual DbSet<RouteModel> Routes { get; set; }
        public virtual DbSet<CollectionModel> Collections { get; set; }
        public virtual DbSet<NotificationModel> Notifications { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("NET_USERS");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("USER_ID");
                entity.Property(u => u.Email).HasColumnType("VARCHAR(255)").HasColumnName("EMAIL").IsRequired();
                entity.Property(u => u.Password).HasColumnType("VARCHAR(255)").HasColumnName("PASSWORD").IsRequired();
                entity.Property(u => u.IsActive).HasColumnType("NUMBER(1,0)").HasColumnName("IS_ACTIVE").IsRequired();
                entity.Property(u => u.CreatedAt).HasColumnType("DATE").HasColumnName("CREATED_AT").IsRequired();
                entity.Property(u => u.Role).HasColumnType("VARCHAR(50)").HasColumnName("ROLE").IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<TruckModel>(entity =>
            {
                entity.ToTable("NET_TRUCKS");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).HasColumnName("TRUCK_ID");
                entity.Property(t => t.LicensePlate).HasColumnType("CHAR(7)").HasColumnName("LICENSE_PLATE").IsRequired(true);
                entity.Property(t => t.Capacity).HasColumnType("FLOAT").HasColumnName("CAPACITY").IsRequired(true);
                entity.Property(t => t.Available).HasColumnType("NUMBER(1,0)").HasColumnName("AVAILABLE").IsRequired(true);
            });

            modelBuilder.Entity<ContainerModel>(entity =>
            {
                entity.ToTable("NET_CONTAINERS");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnName("CONTAINER_ID");
                entity.Property(c => c.Location).HasColumnName("LOCATION").IsRequired();
                entity.Property(c => c.Capacity).HasColumnType("FLOAT").HasColumnName("CAPACITY").IsRequired(true);
                entity.Property(c => c.CurrentLevel).HasColumnName("CURRENT_LEVEL").IsRequired(true);
            });

            modelBuilder.Entity<RouteModel>(entity =>
            {
                entity.ToTable("NET_ROUTES");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).HasColumnName("ROUTE_ID");
                entity.Property(r => r.Description).HasColumnName("DESCRIPTION").IsRequired(true);
                entity.Property(r => r.StartTime).HasColumnName("START_TIME").IsRequired(true);
                entity.Property(r => r.EndTime).HasColumnName("END_TIME").IsRequired(false);
                entity.Property(r => r.TruckId).HasColumnName("TRUCK_ID");

                // Truck Foreign Key
                entity.HasOne(r => r.Truck)
                    .WithMany()
                    .HasForeignKey(r => r.TruckId)
                    .IsRequired(true);
            });

            modelBuilder.Entity<CollectionModel>(entity =>
            {
                entity.ToTable("NET_COLLECTIONS");
                entity.HasKey(c =>  c.Id);
                entity.Property(c => c.Id).HasColumnName("ID");
                entity.Property(c => c.DateTime).HasColumnName("DATE_TIME").IsRequired(true);
                entity.Property(c => c.ContainerId).HasColumnName("CONTAINER_ID");
                entity.Property(c => c.RouteId).HasColumnName("ROUTE_ID");
                
                // Container Foreign Key
                entity.HasOne(c => c.Container)
                    .WithMany()
                    .HasForeignKey(c => c.ContainerId)
                    .IsRequired(true);

                // Route Foreign Key
                entity.HasOne(c => c.Route)
                    .WithMany()
                    .HasForeignKey(c => c.RouteId)
                    .IsRequired(true);
            });

            modelBuilder.Entity<NotificationModel>(entity =>
            {
                entity.ToTable("NET_NOTIFICATIONS");
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Id).HasColumnName("ID");
                entity.Property(n => n.NotificationType).HasColumnName("NOTIFICATION_TYPE").IsRequired(true);
                entity.Property(n => n.Message).HasColumnName("MESSAGE").IsRequired(true);
                entity.Property(n => n.ValidUntil).HasColumnName("VALID_UNTIL").IsRequired(true);
                entity.Property(n => n.IsActive).HasColumnType("NUMBER(1,0)").HasColumnName("IS_ACTIVE").IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
