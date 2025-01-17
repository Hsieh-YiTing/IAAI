using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI.Models.ViewModel.AssnMemberVM
{
    public class AssnMemberVM
    {
        public List<AssnMemberInfo> AssnMemberInfo { get; set; }

        public EditJobTitle EditJobTitle { get; set; }

        public AddJobTitle AddJobTitle { get; set; }

        public AddAssnMember AddAssnMember { get; set; }

        public EditAssnMember EditAssnMember { get; set; }
    }

    // 現有協會成員
    public class AssnMemberInfo
    {
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "職稱編號")]
        public int JobTitleId { get; set; }

        [Display(Name = "職別")]
        public string JobTitle { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public string Gender { get; set; }

        [Display(Name = "職務(經歷)")]
        public string Incumbent { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新時間")]
        public DateTime UpdateTime { get; set; }
    }

    //編輯協會職稱
    public class EditJobTitle
    {
        [Display(Name = "欲編輯職稱 : ")]
        [Required(ErrorMessage = "請選擇職稱")]
        public string SelectedJobTitle { get; set; }

        public IEnumerable<SelectListItem> Option { get; set; }

        [Display(Name = "修改職稱 : ")]
        [Required(ErrorMessage = "職稱必須要填寫")]
        [MaxLength(20, ErrorMessage = "長度不可超過{1}個字")]
        public string NewJobTitle { get; set; }
    }

    // 新增協會職稱
    public class AddJobTitle
    {
        [Display(Name = "新增職稱 : ")]
        [Required(ErrorMessage = "職稱必須要填寫")]
        [MaxLength(20, ErrorMessage = "長度不可超過{1}個字")]
        public string NewJobTitle { get; set; }
    }

    // 新增協會成員
    public class AddAssnMember
    {
        [Display(Name = "協會職稱 : ")]
        [Required(ErrorMessage = "必須要選擇協會職稱")]
        public string SelectedJobTitle { get; set; }

        public IEnumerable<SelectListItem> Option { get; set; }

        [Display(Name = "專家姓名 : ")]
        [Required(ErrorMessage = "必須要輸入專家姓名")]
        [MaxLength(20, ErrorMessage = "長度不可超過{1}個字")]
        public string Name { get; set; }

        [Display(Name = "專家性別 : ")]
        [Required(ErrorMessage = "必須要選擇性別")]
        public string Gender { get; set; }

        [Display(Name = "經歷 : ")]
        [Required(ErrorMessage = "必須要輸入專家經歷")]
        [MaxLength(50, ErrorMessage = "長度不可超過{1}個字")]
        public string Incumbent { get; set; }
    }

    // 編輯協會成員
    public class EditAssnMember
    {
        [Display(Name = "編號 : ")]
        public int Id { get; set; }

        [Display(Name = "協會職稱 : ")]
        [Required(ErrorMessage = "必須要選擇協會職稱")]
        public string SelectedJobTitle { get; set; }

        public IEnumerable<SelectListItem> Option { get; set; }

        [Display(Name = "專家姓名 : ")]
        [Required(ErrorMessage = "必須要輸入專家姓名")]
        [MaxLength(20, ErrorMessage = "長度不可超過{1}個字")]
        public string Name { get; set; }

        [Display(Name = "專家性別 : ")]
        [Required(ErrorMessage = "必須要選擇性別")]
        public string Gender { get; set; }

        [Display(Name = "經歷 : ")]
        [Required(ErrorMessage = "必須要輸入專家經歷")]
        [MaxLength(50, ErrorMessage = "長度不可超過{1}個字")]
        public string Incumbent { get; set; }
    }
}