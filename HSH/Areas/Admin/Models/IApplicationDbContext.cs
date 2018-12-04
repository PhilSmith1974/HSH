using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HSH.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HSH.Areas.Admin.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Property> Propertys { get; }
        DbSet<PropertyType> PropertyTypes { get; }
        DbSet<PropertyLinkText> PropertyLinkTexts { get; }

        //added Task<int> below
        Task<int> SaveChangesAsync();
        void MarkAsModified(Object item);


    }
}

