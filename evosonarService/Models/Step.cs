using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evosonarService.Models
{
    public class Step
    {
        public int Mode { get; set; }
        public bool SubStep { get; set; }
        public double Distance { get; set; }
        public string Duration { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
        public string Instructions { get; set; }
        public string TransportArrivalTime { get; set; }
        public string TransportDepartureTime { get; set; }
        public string TransportArrivalLocation { get; set; }
        public double? TransportArrivalLatitude { get; set; }
        public double? TransportArrivalLongitude { get; set; }
        public string TransportLineName { get; set; }
        public string TransportLineShortName { get; set; }
        public string TransportType { get; set; }
        public int TransportStopNumber { get; set; }

    }
}