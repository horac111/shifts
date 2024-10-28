using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace Shifts.DAL.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public virtual DbSet<Occurence> Occurences { get; set; } = default!;
    public virtual DbSet<Shift> Shifts { get; set; } = default!;
    public virtual DbSet<ShiftDay> ShiftDays { get; set; } = default!;
    public virtual DbSet<WorkingAvailability> WorkingAvailabilities { get; set; } = default!;
    public virtual DbSet<ShiftAssignment> ShiftAssignments { get; set; } = default!;
    public virtual DbSet<ChangeRequest> ChangeRequests { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ChangeRequest>(e =>
        {
            e.HasOne(x => x.Requestor)
            .WithMany(x => x.MyRequests)
            .HasForeignKey(x => x.RequestorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

            e.HasOne(x => x.Replier)
            .WithMany(x => x.RequestsForMe)
            .HasForeignKey(x => x.ReplierId)
            .IsRequired();

            e.HasOne(x => x.ShiftDay)
            .WithMany(x => x.ChangeRequests)
            .HasForeignKey(x => x.ShiftDayId)
            .IsRequired();

            e.HasIndex(x => new { x.ShiftDayId, x.ReplierId, x.RequestorId })
            .IsUnique();

        });

        builder.Entity<ShiftAssignment>(e =>
        {
            e.HasOne(x => x.ShiftDay)
            .WithMany(x => x.ShiftAssignments)
            .HasForeignKey(x => x.ShiftDayId)
            .IsRequired();

            e.HasOne(x => x.User)
            .WithMany(x => x.ShiftAssignments)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

            e.HasIndex(x => new { x.ShiftDayId, x.UserId })
            .IsUnique();
        });

        builder.Entity<ApplicationRole>(e => e.HasData(Enum.GetValues<AplicationRoleEnum>().Select(x => new ApplicationRole() { Role = x, Id = ((int)x).ToString() })));

        var adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
        ApplicationUser admin = new() { Name = "Admin", Surname = "Admin", UserName = "admin", NormalizedUserName = "admin", Id = adminId };
        var password = new PasswordHasher<ApplicationUser>();
        var hashed = password.HashPassword(admin, "admin");
        admin.PasswordHash = hashed;
        builder.Entity<ApplicationUser>(e => e.HasData(admin));
        builder.Entity<IdentityUserRole<string>>(e => e.HasData(Enum.GetValues<AplicationRoleEnum>().Select(x => new IdentityUserRole<string>() { RoleId = ((int)x).ToString(), UserId = adminId })));

        builder.Entity<Occurence>(e =>
        {
            e.HasOne(x => x.Shift)
            .WithMany(x => x.Occurences)
            .HasForeignKey(x => x.ShiftId)
            .IsRequired();

            e.HasIndex(x => x.Date);
        });

        builder.Entity<ShiftDay>(e =>
        {
            e.HasOne(x => x.Shift)
            .WithMany(x => x.ShiftDays)
            .HasForeignKey(x => x.ShiftId)
            .IsRequired();

            e.HasIndex(x => x.Date);

            e.HasIndex(x => new { x.ShiftId, x.Date })
            .IsUnique();
        });

        builder.Entity<WorkingAvailability>(e =>
        {
            e.HasOne(x => x.ShiftDay)
            .WithMany(x => x.WorkingAvailability)
            .HasForeignKey(x => x.ShiftDayId)
            .IsRequired();

            e.HasIndex(x => new { x.ShiftDayId, x.UserId })
            .IsUnique();
        });
    }
}

