using IAAI.Models.BaseEntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI.Models.EFModel.AssnEF
{
    public class AssnJobTitle : BaseEntity
    {
        [Display(Name = "職務名稱")]
        [MaxLength(20)]
        [Required]
        public string JobtTitle { get; set; }

        public virtual ICollection<AssnMember> AssnMembers { get; set; }
    }
}