namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageUrl
    {
        public int ID { get; set; }

        public int Source { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public virtual SourceCode SourceCode { get; set; }
    }
}
