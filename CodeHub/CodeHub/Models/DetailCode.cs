namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetailCode
    {
        public int ID { get; set; }

        public int Source { get; set; }

        public int? Views { get; set; }

        public int? Purchases { get; set; }

        public virtual SourceCode SourceCode { get; set; }
    }
}
