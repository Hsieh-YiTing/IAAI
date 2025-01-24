using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI.Models.ViewModel.AssnMemberVM
{
    public class MemberDetailedInfoVM
    {
        public int SelectJobTitleId { get; set; }

        public int SelectAssnMemberId { get; set; }

        public IEnumerable<SelectListItem> JobTitleOption { get; set; }

        public IEnumerable<SelectListItem> AssnMemberOption { get; set; }

        [Required(ErrorMessage = "必須要輸入資料")]
        [AllowHtml]
        public string Information { get; set; }
    }
}