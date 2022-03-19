using System.Collections.Generic;

namespace Test_Task2.Models
{
    public class SubdivisionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public int? OwnerId { get; set; }

        public SubdivisionModel(string name, bool status, int? ownerId)
        {
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }

        public SubdivisionModel(int id, string name, bool status, int? ownerId)
        {
            Id = id;
            Name = name;
            Status = status;
            OwnerId = ownerId;
        }
    }
}
