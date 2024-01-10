namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        public int ID { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int? Requester { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Coder { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual User User { get; set; }
    }
}
