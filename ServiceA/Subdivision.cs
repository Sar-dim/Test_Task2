using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceA
{
    public class Subdivision
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public int? OwnerId { get; set; }

        public List<Subdivision> Subdivisions { get; set; }

        public Subdivision OwnerSubdivision { get; set; }

        public Subdivision(string name, bool status, int? ownerId)
        {
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }

        public Subdivision(int id, string name, bool status, int? ownerId)
        {
            Id = id;
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }
    }
}
