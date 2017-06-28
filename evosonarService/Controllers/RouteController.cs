using evosonarService.Models;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace evosonarService.Controllers
{
    [Route("api/route")]
    public class RouteController : ApiController
    {
       
        public RouteResponse PostRoute([FromBody] RouteRequest body)
        {
            body.DepartureTime = DateTime.Now;
            var response = new RouteResponse();
            response.Steps = new List<Models.Step>();

            var origin = body.OriginLat.ToString().Replace(',', '.') + "," + body.OriginLng.ToString().Replace(',', '.');
            var destination = body.DestinationLat.ToString().Replace(',', '.') + "," + body.DestinationLng.ToString().Replace(',', '.');
            DirectionsRequest directionsRequest = new DirectionsRequest()
            {
                Origin = origin,
                Destination = destination,
                Language = "pt-BR",
                TravelMode = TravelMode.Transit,
                DepartureTime = body.DepartureTime,
                ApiKey = "AIzaSyBicGuTCqAsXSLuWvBvVUO6TwkgWIXU7tI"
            };

            DirectionsResponse directions = GoogleMaps.Directions.Query(directionsRequest);
            if (directions.Routes.Count() > 0)
            {
                var route = directions.Routes.FirstOrDefault();
                if (route.Legs.Count() > 0)
                {
                    var legs = route.Legs.FirstOrDefault();
                    response.StartLatitude = legs.StartLocation.Latitude;
                    response.StartLongitude = legs.StartLocation.Longitude;
                    response.EndLatitude = legs.EndLocation.Latitude;
                    response.EndLongitude = legs.EndLocation.Longitude;
                    response.Distance = legs.Distance.Text;
                    response.Duration = legs.Duration.Text;

                    if (legs.Steps.Count() > 0)
                    {
                        foreach (var s in legs.Steps)
                        {
                            if (s.TravelMode == TravelMode.Walking)
                            {
                                response.Steps.Add(new Models.Step()
                                {
                                    Mode = 2,
                                    StartLatitude = s.StartLocation.Latitude,
                                    StartLongitude = s.StartLocation.Longitude,
                                    Distance = s.Distance.Value,
                                    Duration = s.Duration.Text,
                                    EndLatitude = s.EndLocation.Latitude,
                                    EndLongitude = s.EndLocation.Longitude,
                                    Instructions = s.HtmlInstructions,
                                    SubStep = false
                                });

                                if (s.SubSteps.Count() > 0)
                                {
                                    foreach (var ss in s.SubSteps)
                                    {
                                        response.Steps.Add(new Models.Step()
                                        {
                                            Mode = 2,
                                            Distance = ss.Distance.Value,
                                            Duration = ss.Duration.Text,
                                            StartLatitude = ss.StartLocation.Latitude,
                                            StartLongitude = ss.StartLocation.Longitude,
                                            EndLatitude = ss.EndLocation.Latitude,
                                            EndLongitude = ss.EndLocation.Longitude,
                                            Instructions = ss.HtmlInstructions,
                                            SubStep = true
                                        });
                                    }
                                }
                            }
                            else
                            {
                                response.Steps.Add(new Models.Step()
                                {
                                    Mode = 1,
                                    Distance = s.Distance.Value,
                                    Duration = s.Duration.Text,
                                    StartLatitude = s.StartLocation.Latitude,
                                    StartLongitude = s.StartLocation.Longitude,
                                    EndLatitude = s.EndLocation.Latitude,
                                    EndLongitude = s.EndLocation.Longitude,
                                    Instructions = s.HtmlInstructions,
                                    SubStep = false,
                                    TransportArrivalTime = s.TransitDetails.ArrivalTime.Text,
                                    TransportDepartureTime = s.TransitDetails.ArrivalTime.Text,
                                    TransportArrivalLocation = s.TransitDetails.ArrivalStop.Name,
                                    TransportArrivalLatitude = s.TransitDetails.ArrivalStop.Location.Latitude,
                                    TransportArrivalLongitude = s.TransitDetails.ArrivalStop.Location.Longitude,
                                    TransportLineName = s.TransitDetails.Lines.Name,
                                    TransportLineShortName = s.TransitDetails.Lines.ShortName,
                                    TransportType = s.TransitDetails.Lines.Vehicle.Name,
                                    TransportStopNumber = s.TransitDetails.NumberOfStops
                                });
                            }
                        }
                    }
                }
            }

            return response;
        }
    }
}