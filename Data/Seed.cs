using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocilaMediaProject.Data.Enum;
using SocilaMediaProject.Models;
using Microsoft.AspNetCore.Identity;

namespace SocilaMediaProject.Data
{
    public class Seed
    {
        private readonly ModelBuilder modelBuilder;
        public Seed(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void SeedData()
        {
            const string ADMIN_USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
            const string ADMIN_ROLE_ID = "b421e928-0613-9ebd-a64c-f10b6a706e73";

            modelBuilder.Entity<IdentityRole>(role =>
            {

                role.HasData(new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = "1"
                },
                 new IdentityRole()
                 {
                     Id = "role2",
                     Name = "user",
                     NormalizedName = "User",
                     ConcurrencyStamp = "2"
                 },
                new IdentityRole()
                {
                    Id = "role3",
                    Name = "HR",
                    NormalizedName = "HR",
                    ConcurrencyStamp = "3"
                });

            });

            modelBuilder.Entity<AppUser>(user =>
    {
        user.HasData(new AppUser()
        {
            Id = ADMIN_USER_ID,
            UserName = "Karim_Abbas",
            Email = "Karim.abdelhameed@gmail.com",
            PasswordHash = "lol123*As",
            PhoneNumber = "+002114515",

        });
        modelBuilder.Entity<IdentityUserRole<string>>(i => i.HasData(new IdentityUserRole<string>()
        {
            UserId = ADMIN_USER_ID,
            RoleId = ADMIN_ROLE_ID
        }));
    });

            modelBuilder.Entity<Address>(address =>
            {
                address.HasData(new Address()
                {
                    ID = 1,
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC",
                },
                new Address()
                {
                    ID = 2,
                    Street = "456 Main St",
                    City = "Charlotte",
                    State = "NC",
                },
                     new Address()
                     {
                         ID = 3,
                         Street = "456 Main St",
                         City = "Michigan",
                         State = "NC",
                     },
                     new Address()
                     {
                         ID = 4,
                         Street = "456 Main St",
                         City = "Charlotte",
                         State = "NC",
                     });
            });
            modelBuilder.Entity<Club>(club =>
            {
                club.HasData(new
                {
                    ID = 1,
                    Title = "RunningClub1",
                    Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                    Description = "This is the description of the first cinema",
                    ClubCategory = ClubCategory.City,
                    AddressID = 1,
                }, new
                {
                    ID = 2,
                    Title = "RunningClub2",
                    Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                    Description = "This is the description of the first cinema",
                    ClubCategory = ClubCategory.City,
                    AddressID = 2,
                },
                 new
                 {
                     ID = 3,
                     Title = "RunningClub3",
                     Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                     Description = "This is the description of the first cinema",
                     ClubCategory = ClubCategory.City,
                     AddressID = 3,
                 },
                 new
                 {
                     ID = 4,
                     Title = "RunningClub4",
                     Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                     Description = "This is the description of the first cinema",
                     ClubCategory = ClubCategory.City,
                     AddressID = 4,
                 });
            });

        }

    }
}