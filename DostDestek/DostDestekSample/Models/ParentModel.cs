using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DostDestek.Entity;

namespace DostDestekSample.Models
{
    public class ParentModel
    {
        public Comment comment { get; set; }
        public Post post { get; set; }
    }
}