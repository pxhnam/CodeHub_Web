namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        public int ID { get; set; }

        public int? ReportTypeID { get; set; }

        public int? Reporter { get; set; }

        public int? Source { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; }

        public virtual ReportType ReportType { get; set; }

        public virtual SourceCode SourceCode { get; set; }
    }
}
