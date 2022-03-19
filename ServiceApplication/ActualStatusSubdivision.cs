using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceApplication
{
    public class ActualStatusSubdivision
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public int? OwnerId { get; set; }

        public ActualStatusSubdivision(string name, bool status, int? ownerId)
        {
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }

        public ActualStatusSubdivision(int id, string name, bool status, int? ownerId)
        {
            Id = id;
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }
    }
}
