using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace HSH.Areas.Admin.Models
{
    // root element of service output
    // essentially an array of addresses
    [XmlRoot("Addresses")]
    public class Addresses
    {

        [XmlElement("Address")]
        public List<PropertyModel> AddressList { get; set; }
    }
}