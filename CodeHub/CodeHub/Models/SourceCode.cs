namespace CodeHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SourceCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SourceCode()
        {
            Carts = new HashSet<Cart>();
            DetailCodes = new HashSet<DetailCode>();
            ImageUrls = new HashSet<ImageUrl>();
            Orders = new HashSet<Order>();
            Reports = new HashSet<Report>();
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }

        public int Coder { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int? LanguageID { get; set; }

        public int? TypeID { get; set; }

        [StringLength(255)]
        public string LinkVideo { get; set; }

        public string Description { get; set; }

        public string SourceLink { get; set; }

        public int Fee { get; set; }

        public bool? IsShow { get; set; }

        public bool? IsDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCode> DetailCodes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageUrl> ImageUrls { get; set; }

        public virtual Language Language { get; set; }

        public virtual Manager Manager { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Type Type { get; set; }
    }
}
