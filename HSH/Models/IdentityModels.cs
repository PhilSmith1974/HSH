using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using HSH.Areas.Admin.Models;
using HSH.Entities;
//using HSH.Tests.TestContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HSH.Models
{
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser

    {
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Registered { get; set; } // errored to Public Database

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public static implicit operator ApplicationUser(ApplicationUser v)
        //{
        //    //return new ApplicationUser();
        //    throw new NotImplementedException();
        //}
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void MarkAsModified(object item)
        {
            throw new NotImplementedException();
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Property> Propertys { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyLinkText> PropertyLinkTexts { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<PropertyItem> PropertyItems { get; set; }
        public DbSet<FavouriteProperty> FavouritePropertys { get; set; }
        public DbSet<UserFavourite> UserFavourites { get; set; }
        public DbSet<UserPropertyFavourite> UserPropertyFavourites { get; set; }

        object IApplicationDbContext.UserPropertyFavourites => throw new NotImplementedException();
    }
}