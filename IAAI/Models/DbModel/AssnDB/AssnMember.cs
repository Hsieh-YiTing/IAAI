using IAAI.Models.BaseEntityModel;
using IAAI.Models.EFModel.AssnEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI.Models.EFModel
{
    public class AssnMember : BaseEntity
    {
        public int AssnJobTitleId { get; set; }

        [ForeignKey("AssnJobTitleId")]
        public virtual AssnJobTitle AssnJobTitle { get; set; }

        [Display(Name = "專家名稱")]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "姓別")]
        [Required]
        public string Gender { get; set; }

        [Display(Name = "職務(經歷)")]
        [MaxLength(50)]
        [Required]
        public string Incumbent { get; set; }

        [Display(Name = "詳細資料")]
        public string Information { get; set; }
    }
}