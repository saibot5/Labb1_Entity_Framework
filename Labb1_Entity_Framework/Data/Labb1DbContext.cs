using Labb1_Entity_Framework.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Labb1_Entity_Framework.Models;

namespace Labb1_Entity_Framework.Data;

public class Labb1DbContext : IdentityDbContext<Labb1User>
{
    public Labb1DbContext(DbContextOptions<Labb1DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //create Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Customer", NormalizedName = "CUSTOMER" },
            new IdentityRole { Id = "2", Name = "Employee", NormalizedName = "EMPLOYEE" },
            new IdentityRole { Id = "3", Name = "Admin", NormalizedName = "ADMIN" }
            );


        PasswordHasher<Labb1User> ph = new PasswordHasher<Labb1User>();

        //create admin
        var user = new Labb1User
        {
            Id = "1",
            UserName = "admin@hotmail.com",
            NormalizedUserName = "ADMIN@HOTMAIL.COM",
            NormalizedEmail = "ADMIN@HOTMAIL.COM",
            Email = "admin@hotmail.com",
            EmailConfirmed = true,
            Firstname = "Adam",
            Lastname = "Adminsson"
        };
        user.PasswordHash = ph.HashPassword(user, "Test123!");
        builder.Entity<Labb1User>().HasData(user);

        //create users
        user = new Labb1User
        {
            Id = "2",
            UserName = "user1@hotmail.com",
            NormalizedUserName = "USER1@HOTMAIL.COM",
            NormalizedEmail = "USER1@HOTMAIL.COM",
            Email = "user1@hotmail.com",
            EmailConfirmed = true,
            Firstname = "Tobias",
            Lastname = "Söderqvist"
        };
        user.PasswordHash = ph.HashPassword(user, "Test123!");
        builder.Entity<Labb1User>().HasData(user);

        //Create Vacation Requests for first user
        builder.Entity<VacationRequest>().HasData(
            new VacationRequest { Id = 1, StartDate = new DateTime(2024, 04, 08), EndDate = new DateTime(2024, 04, 22), VacationType = 0, EmployeeId = "2", accepted = false, Days = new DateTime(2024, 04, 22).Subtract(new DateTime(2024, 04, 08)).Days, CreatedDate = new DateTime(2024, 01, 01) },
            new VacationRequest { Id = 2, StartDate = new DateTime(2024, 01, 08), EndDate = new DateTime(2024, 01, 22), VacationType = 0, EmployeeId = "2", accepted = true, Days = new DateTime(2024, 01, 22).Subtract(new DateTime(2024, 01, 08)).Days, CreatedDate = new DateTime(2024, 01, 02) },
            new VacationRequest { Id = 3, StartDate = new DateTime(2024, 02, 9), EndDate = new DateTime(2024, 02, 10), VacationType = 0, EmployeeId = "2", accepted = false, Days = new DateTime(2024, 02, 10).Subtract(new DateTime(2024, 02, 9)).Days, CreatedDate = new DateTime(2024, 02, 01) }
            );


        user = new Labb1User
        {
            Id = "3",
            UserName = "user2@hotmail.com",
            NormalizedUserName = "USER2@HOTMAIL.COM",
            NormalizedEmail = "USER2@HOTMAIL.COM",
            Email = "user2@hotmail.com",
            EmailConfirmed = true,
            Firstname = "John",
            Lastname = "Doe"
        };
        user.PasswordHash = ph.HashPassword(user, "Test123!");
        builder.Entity<Labb1User>().HasData(user);

        builder.Entity<VacationRequest>().HasData(
            new VacationRequest { Id = 4, StartDate = new DateTime(2024, 04, 08), EndDate = new DateTime(2024, 04, 9), VacationType = 0, EmployeeId = "3", accepted = true, Days = new DateTime(2024, 04, 9).Subtract(new DateTime(2024, 04, 08)).Days ,CreatedDate = new DateTime(2024, 03, 01) },
            new VacationRequest { Id = 5, StartDate = new DateTime(2024, 01, 08), EndDate = new DateTime(2024, 01, 22), VacationType = 0, EmployeeId = "3", accepted = true, Days = new DateTime(2024, 01, 22).Subtract(new DateTime(2024, 01, 08)).Days,CreatedDate = new DateTime(2024, 01, 04) },
            new VacationRequest { Id = 6, StartDate = new DateTime(2024, 02, 9), EndDate = new DateTime(2024, 02, 10), VacationType = 0, EmployeeId = "3", accepted = false, Days = new DateTime(2024, 02, 10).Subtract(new DateTime(2024, 02, 9)).Days ,CreatedDate = new DateTime(2024, 02, 05) }
            );

        //assign roles
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "3", UserId = "1" },
            new IdentityUserRole<string> { RoleId = "2", UserId = "2" },
            new IdentityUserRole<string> { RoleId = "2", UserId = "3" }
            );





    }

    public DbSet<Labb1_Entity_Framework.Models.VacationRequest> VacationRequest { get; set; } = default!;






}


