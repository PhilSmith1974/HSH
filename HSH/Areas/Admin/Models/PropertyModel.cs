using HSH.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HSH.Areas.Admin.Models
{
    public class PropertyModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        public int PropertyLinkTextId { get; set; }
        public int PropertyTypeId { get; set; }

        [DisplayName("Property Link Text")]
        public ICollection<PropertyLinkText> PropertyLinkTexts { get; set; }

        [DisplayName("Property Type")]
        public ICollection<PropertyType> PropertyTypes { get; set; }

        public string PropertyType
        {
            get
            {
                return PropertyTypes == null ||
                    PropertyTypes.Count.Equals(0) ?
                    String.Empty : PropertyTypes.First(
                        pt => pt.Id.Equals(PropertyTypeId)).Title;
            }
        }

        public string PropertyLinkText
        {
            get
            {
                return PropertyLinkTexts == null ||
                    PropertyLinkTexts.Count.Equals(0) ?
                    String.Empty : PropertyLinkTexts.First(
                        pt => pt.Id.Equals(PropertyLinkTextId)).Title;
            }
        }

    }
}
