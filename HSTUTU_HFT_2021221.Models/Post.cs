using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    [Table("PostTable")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public virtual Blog Blog { get; set; }
        [ForeignKey(nameof(Blog))]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }

        [NotMapped]
        public virtual ICollection<PostTag> PostTags { get; set; }
        public int Likes { get; set; }
    }
}
