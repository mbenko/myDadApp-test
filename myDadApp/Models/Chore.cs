using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myDadApp.Models
{
    public class Chore
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompleteAt { get; set; }
        public bool Completed { get { return CompleteAt != null; } }

        public Chore()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            //Owner = "demo";
        }
    }

    // View vChores - Id, Owner, Title, Description, createdAt, CompleteAt, Depth, Sort
    [Table("v_Chores")]
    public class vChores
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompleteAt { get; set; }
        public int Depth { get; set; }
        public string Sort { get; set; }
    }
}
