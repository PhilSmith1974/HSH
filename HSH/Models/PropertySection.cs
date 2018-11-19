using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Models
{
    public class PropertySection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ItemTypeId { get; set; }
        public IEnumerable<PropertyItemRow> Items { get; set; }
    }
}