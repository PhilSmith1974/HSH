//using HSH.Entities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Xml.Serialization;

//namespace HSH.Areas.Admin.Models
//{
//    [Serializable]
//    [XmlRoot("Address")]
//    public class PropertyModel
//    {
//        [XmlIgnore]
//        public int Id { get; set; }

//        [XmlIgnore]
//        [MaxLength(255)]
//        [Required]
//        public string Title { get; set; }

//        [XmlIgnore]
//        [MaxLength(2048)]
//        [Required]
//        public string Description { get; set; }

//        [XmlIgnore]
//        [Required]
//        public int NumberOfBedrooms { get; set; }

//        [XmlIgnore]
//        [Required]
//        [DisplayName("Price € ")]
//        public double Price { get; set; }

//        //[Required]
//        //[DisplayName("Address")]
//        //public AddressModel Address { get; set; }

//        [XmlIgnore]
//        [MaxLength(1024)]
//        [DisplayName("Image Url")]
//        public string ImageUrl { get; set; }

//        [XmlIgnore]
//        [DisplayName("Property Link Text")]
//        public int PropertyLinkTextId { get; set; }

//        [XmlIgnore]
//        [DisplayName("Property Type")]
//        public int PropertyTypeId { get; set; }

//        [XmlIgnore]
//        [DisplayName("Property Link Text")]
//        public ICollection<PropertyLinkText> PropertyLinkTexts { get; set; }

//        [XmlIgnore]
//        [DisplayName("Property Type")]
//        public ICollection<PropertyType> PropertyTypes { get; set; }

//        [XmlIgnore]
//        public string PropertyType
//        {
//            get
//            {
//                return PropertyTypes == null ||
//                    PropertyTypes.Count.Equals(0) ?
//                    String.Empty : PropertyTypes.First(
//                        pt => pt.Id.Equals(PropertyTypeId)).Title;
//            }
//        }

//        [XmlIgnore]
//        public string PropertyLinkText
//        {
//            get
//            {
//                return PropertyLinkTexts == null ||
//                    PropertyLinkTexts.Count.Equals(0) ?
//                    String.Empty : PropertyLinkTexts.First(
//                        pt => pt.Id.Equals(PropertyLinkTextId)).Title;
//            }
//        }

//        [XmlIgnore]
//        public bool IsFavourite { get; set; }

//        public long AddressId { get; set; }
//        [DisplayName("Summary Line")]
//        [XmlElement("summaryline")]
//        public string SummaryLine { get; set; }
//        [XmlElement("premise")]
//        public string Premise { get; set; }
//        [XmlElement("street")]
//        public string Street { get; set; }
//        [DisplayName("Dependent Locality")]
//        [XmlElement("dependentlocality")]
//        public string DependentLocality { get; set; }
//        [DisplayName("Double Dependent Locality")]
//        [XmlElement("doubledependentlocality")]
//        public string DoubleDependentLocality { get; set; }
//        [DisplayName("Post Town")]
//        [XmlElement("posttown")]
//        public string PostTown { get; set; }
//        [XmlElement("county")]
//        public string County { get; set; }
//        [DisplayName("Post Code")]
//        [XmlElement("postcode")]
//        public string PostCode { get; set; }
//        [XmlElement("number")]
//        public string Number { get; set; }
//    }
//}
