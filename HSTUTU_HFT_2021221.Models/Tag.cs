using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    [Table("TagTable")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
