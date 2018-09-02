using HSH.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HSH.Areas.Admin.Models
{
    public class PropertyItemModel
    {
        [DisplayName("Property Id")]
        public int PropertyId { get; set; }
        [DisplayName("Item Id")]
        public int ItemId { get; set; }
        [DisplayName("Property Title")]
        public string ProductTitle { get; set; }
        [DisplayName("Item Title")]
        public string ItemTitle { get; set; }
        public ICollection<Property>Propertys { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}