using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Models
{
    public class PropertySectionModel
    {
        public string Title { get; set; }
        public List<PropertySection> Sections { get; set; }
    }
}