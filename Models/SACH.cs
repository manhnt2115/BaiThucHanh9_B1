namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SACH")]
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            THAMGIAs = new HashSet<THAMGIA>();
        }

        [Key]
        [StringLength(10)]
        public string MaSach { get; set; }

        [StringLength(100)]
        public string TenSach { get; set; }

        public decimal? GiaBan { get; set; }

        public string MoTa { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        [StringLength(255)]
        public string AnhBia { get; set; }

        public int? SoLuongTon { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChuDe { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNXB { get; set; }

        public bool? Moi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual CHUDE CHUDE { get; set; }

        public virtual NHAXUATBAN NHAXUATBAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THAMGIA> THAMGIAs { get; set; }
    }
}
