using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evosonarService.DataObjects
{
    public class Address : EntityData
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public string Cep { get; set; }
        public string Location { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
    }
}