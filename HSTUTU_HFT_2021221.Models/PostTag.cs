using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    [Table("PostTagTable")]
    public class PostTag
    {
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }

        [NotMapped]
        public virtual Post Post { get; set; }
        [NotMapped]
        public virtual Tag Tag { get; set; }
    }
}
