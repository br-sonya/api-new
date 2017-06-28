using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evosonarService.Models
{
    public class RouteRequest
    {
        public Double OriginLat { get; set; }
        public Double OriginLng { get; set; }
        public Double DestinationLat { get; set; }
        public Double DestinationLng { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}