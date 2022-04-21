using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace zomato.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        [StringLength(255)]
        public string RefreshToken { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
