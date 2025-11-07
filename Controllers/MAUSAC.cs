using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("MAUSAC")]
    public partial class MAUSAC
    {
        [Key]
        public int MaMau { get; set; }

        [Required, StringLength(50)]
        public string TenMau { get; set; }

        [StringLength(10)]
        public string MaMauHex { get; set; } // Ví dụ: #FF0000

        // Khóa ngoại đến SACH
        public int MaSach { get; set; }

        [ForeignKey("MaSach")]
        public virtual SACH SACH { get; set; }
    }
}
