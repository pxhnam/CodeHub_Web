namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? CodeID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? StarRating { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual SourceCode SourceCode { get; set; }

        public virtual User User { get; set; }
    }
}
