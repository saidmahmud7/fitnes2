using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Trainer>()
            .HasMany(t => t.WorkoutSessions)
            .WithOne(ws => ws.Trainer)
            .HasForeignKey(w => w.TrainerId);
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.WorkoutSessions)
            .WithOne(sw => sw.Workout)
            .HasForeignKey(s => s.WorkoutId);
        modelBuilder.Entity<Client>()
            .HasMany(c => c.WorkoutSessions)
            .WithOne(ws => ws.Client)
            .HasForeignKey(w => w.ClientId);

    }
}

