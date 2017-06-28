using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;


namespace evosonarService.DataObjects
{
    public class User : EntityData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}