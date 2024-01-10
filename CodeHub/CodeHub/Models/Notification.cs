namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? ManagerID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual User User { get; set; }
    }
}
