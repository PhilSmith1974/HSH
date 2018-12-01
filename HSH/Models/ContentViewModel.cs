using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HSH.Models;

namespace HSH.Models
{
    public class ContentViewModel
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HTML { get; set; }
        public string VideoURL { get; set; }
    }
}