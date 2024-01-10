namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? CodeID { get; set; }

        public int Fee { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual SourceCode SourceCode { get; set; }

        public virtual User User { get; set; }
    }
}
