﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Models
{
    public class ThumbnailAreaModel
    {
        public string Title { get; set; }
        public IEnumerable<ThumbnailModel> Thumbnails{ get; set; }      
    }
}