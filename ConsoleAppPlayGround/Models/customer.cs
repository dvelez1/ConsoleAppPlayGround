using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPlayGround.Models
{
    public class customer
    {
        //public int Id { get; set; }
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty("city", Required = Required.Always)]
        public string City { get; set; }
        [JsonProperty("state", Required = Required.AllowNull)]
        public string State { get; set; }
    }
}


