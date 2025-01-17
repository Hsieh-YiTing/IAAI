using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI.Models.ViewModel.AssnMemberVM
{
    public class AssnMemberInfoVM
    {
        public List<MemberMenu> MemberMenu { get; set; }

        public MemberDetailedInfo MemberDetailedInfo { get; set; }
    }

    // 協會職稱/成員選單
    public class MemberMenu
    {
        public int SelectJobTitleId { get; set; }

        public int SelectAssnMemberId { get; set; }

        public List<SelectListItem> JobTitleOption { get; set; }

        public List<SelectListItem> AssnMemberOption { get; set; }
    }

    // 協會成員詳細資料
    public class MemberDetailedInfo
    {
        public string Information { get; set; }
    }
}