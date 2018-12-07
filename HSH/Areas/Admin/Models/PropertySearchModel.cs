using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HSH.Entities;
using HSH.Areas.Admin.Models;
using System.Web.Mvc;

namespace HSH.Models
{
    public class PropertySearchModel
    {
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }

        [Display(Name = "Price From")]
        public double? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public double? PriceTo { get; set; }

        [HiddenInput]
        public int? PropertyTypeId { get; set; }

        [DisplayName("Property Type")]
        public List<SelectListItem> PropertyTypesList { get; set; }

        [Display(Name = "No. Of Bedrooms From")]
        public int? NumberOfBedroomsFrom { get; set; }

        [Display(Name = "No. Of Bedrooms To")]
        public int? NumberOfBedroomsTo { get; set; }

        [Display(Name = "County")]
        public string County { get; set; }
    }
}