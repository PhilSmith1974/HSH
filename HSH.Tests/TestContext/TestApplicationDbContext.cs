using HSH.Areas.Admin.Models;
using HSH.Entities;
using HSH.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSH.Tests.TestContext
{
    class TestApplicationDbContext : IApplicationDbContext
    {
        public TestApplicationDbContext()
        {
            this.Propertys = new TestPropertyDbSet();
            this.PropertyTypes = new TestPropertyTypeDbSet();
            this.PropertyLinkTexts = new TestPropertyLinkTextDbSet();
        }

        public DbSet<Property> Propertys { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyLinkText> PropertyLinkTexts { get; set; }

        public object UserPropertyFavourites => throw new NotImplementedException();

        public Task<int> SaveChangesAsync()
        {
           throw new NotImplementedException();
            
        }

        public void MarkAsModified(object item)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModified(Property item) { }
        public void Dispose(){ }
     
    }
}
