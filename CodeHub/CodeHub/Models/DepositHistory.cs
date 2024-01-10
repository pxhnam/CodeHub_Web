namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepositHistory")]
    public partial class DepositHistory
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int Amount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public bool? TransactionType { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public virtual User User { get; set; }
    }
}
