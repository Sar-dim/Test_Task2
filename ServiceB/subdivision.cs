using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceB
{
    public class subdivision
    {
        public int id { get; set; }

        public string name { get; set; }

        public int status { get; set; }

        public int? ownerid { get; set; }

        [JsonConstructor]
        public subdivision(string name, int status, int? ownerid)
        {
            this.name = name;
            this.status = status;
            this.ownerid = ownerid;
        }

        public subdivision(int id, string name, int status, int? ownerid)
        {
            this.id = id;
            this.name = name;
            this.status = status;
            this.ownerid = ownerid;
        }
    }
}
