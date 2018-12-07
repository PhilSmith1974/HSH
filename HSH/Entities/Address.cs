using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace HSH.Entities
{
    // address class
    [Table("Address")]
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string SummaryLine { get; set; }
        public string Premise { get; set; }
        public string Street { get; set; }
        public string DependentLocality { get; set; }
        public string DoubleDependentLocality { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Number { get; set; }

        public override string ToString() { return SummaryLine; }
    }
}