using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace HSH.Areas.Admin.Models
{
    // address class
    [Serializable]
    public class AddressModel
    {
        public long Id { get; set; }
        [DisplayName("Summary Line")]
        [XmlElement("summaryline")]
        public string SummaryLine { get; set; }
        [XmlElement("organisation")]
        public string Organisation { get; set; }
        [XmlElement("summapremiseryline")]
        public string Premise { get; set; }
        [DisplayName("Dependent Street")]
        [XmlElement("dependentstreet")]
        public string DependentStreet { get; set; }
        [XmlElement("street")]
        public string Street { get; set; }
        [DisplayName("Dependent Locality")]
        [XmlElement("dependentlocality")]
        public string DependentLocality { get; set; }
        [DisplayName("Double Dependent Locality")]
        [XmlElement("doubledependentlocality")]
        public string DoubleDependentLocality { get; set; }
        [DisplayName("Post Town")]
        [XmlElement("posttown")]
        public string PostTown { get; set; }
        [XmlElement("county")]
        public string County { get; set; }
        [DisplayName("Post Code")]
        [XmlElement("postcode")]
        public string PostCode { get; set; }
        public string Number { get; set; }

        public override string ToString() { return SummaryLine; }
    }
}