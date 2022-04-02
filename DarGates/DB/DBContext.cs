using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DarGates.DB
{
    public class DBContext : IdentityDbContext<GardUser,IdentityRole,string>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        //public DbSet<GardUser> GardUser { get; set; }
        public DbSet<Gate> Gate { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<GateLog> GateLog { get; set; }
        public DbSet<WeeklyHoliday> WeeklyHoliday { get; set; }
        public DbSet<OfficialHoliday> OfficialHoliday { get; set; }
        public DbSet<GardErrorLog> GardErrorLog { get; set; }
        public DbSet<SignInLog> SignInLog { get; set; }
        public DbSet<RePrintReason> RePrintReason { get; set; }
        public DbSet<PrinterLog> PrinterLog { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<InvitationType> InvitationType { get; set; }
        public DbSet<PrinterMacs> PrinterMac { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GardUser>()
                .Ignore(g => g.AccessFailedCount)
                .Ignore(g => g.Email)
                .Ignore(g => g.EmailConfirmed)
                .Ignore(g => g.LockoutEnabled)
                .Ignore(g => g.LockoutEnd)
                .Ignore(g => g.NormalizedEmail)
                .Ignore(g => g.NormalizedUserName)
                .Ignore(g => g.PhoneNumber)
                .Ignore(g => g.PhoneNumberConfirmed)
                .Ignore(g => g.TwoFactorEnabled);
            /*modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Guard", NormalizedName = "Guard".ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() }
                );*/
            modelBuilder.Entity<GateLog>()
                .HasOne(g => g.DeletedByUser)
                .WithMany(g => g.DeletedLogs)
                .HasForeignKey(f => f.DeletedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PrinterLog>()
                .HasOne(g => g.DeletedByUser)
                .WithMany(g => g.DeletedPrinterLogs)
                .HasForeignKey(f => f.DeletedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WeeklyHoliday>()
                .HasData(
                new WeeklyHoliday { Id = 1, Number = 1, Day = "Monday", IsHoliday = false },
                new WeeklyHoliday { Id = 2, Number = 2, Day = "Tuesday", IsHoliday = false },
                new WeeklyHoliday { Id = 3, Number = 3, Day = "Wednesday", IsHoliday = false },
                new WeeklyHoliday { Id = 4, Number = 4, Day = "Thursday", IsHoliday = false },
                new WeeklyHoliday { Id = 5, Number = 5, Day = "Friday", IsHoliday = true },
                new WeeklyHoliday { Id = 6, Number = 6, Day = "Saturday", IsHoliday = true },
                new WeeklyHoliday { Id = 7, Number = 0, Day = "Sunday", IsHoliday = false }
                );

            modelBuilder.Entity<Owner>()
                .HasData(
                new Owner { Id = 1, Type = "Militry", Price = 5, PriceInHoliday = 32F },
                new Owner { Id = 2, Type = "Civil", Price = 10.5F, PriceInHoliday = 15 },
                new Owner { Id = 3, Type = "عضو دار", Price = 30, PriceInHoliday = 20.5F },
                new Owner { Id = 4, Type = "ق.م", Price = 20, PriceInHoliday = 20.5F },
                new Owner { Id = 5, Type = "مدنى", Price = 10, PriceInHoliday = 20.5F },
                new Owner { Id = 6, Type = "انشطه", Price = 10, PriceInHoliday = 20.5F }
                
                );
            modelBuilder.Entity<RePrintReason>()
                .HasData(
                new RePrintReason { ID = 1, Reason = "فقدان التذكره", Price = 5 },
                new RePrintReason { ID = 2, Reason = "خطأ فى الطباعه", Price = 0 }
                );
            modelBuilder.Entity<Gate>()
                .HasData(new Gate { Id = 1, Name = "بوابه رقم 1", PrinterMac = "DC:0D:30:CC:27:08" });
            
            modelBuilder.Entity<InvitationType>()
                .HasData(
                new InvitationType { ID = 1, Type = "VIP Invitation" },
                new InvitationType { ID = 2, Type = "Normal" }
                );

            //modelBuilder.Entity<PrinterLog>()
            //    .HasOne(e => e.GateLog)
            //    .WithMany(m=>m.PrinterLogs)
            //    .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
