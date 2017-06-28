using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evosonarService.Models
{
    public class RouteResponse
    {
        public Double StartLatitude { get; set; }
        public Double StartLongitude { get; set; }
        public Double EndLatitude { get; set; }
        public Double EndLongitude { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public List<Step> Steps { get; set; }

    }
}