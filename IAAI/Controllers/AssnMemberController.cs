using Antlr.Runtime.Misc;
using IAAI.Models.ViewModel.AssnMemberVM;
using IAAI.Services.AssnMemberService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace IAAI.Controllers
{
    public class AssnMemberController : Controller
    {
        public ActionResult MemberManagement()
        {
            var viewModel = new AssnService().InitAssnMemberVM();
            return View(viewModel);
        }

        public ActionResult MemberDetailedInfo()
        {
            var viewModel = new AssnService().InitMemberDetailedInfoVM();
            return View(viewModel);
        }

        // 新增職位，成功就傳回更新後的選單item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobTitle(AddJobTitle addJobTitle)
        {
            if (ModelState.IsValid)
            {
                var service = new AssnService();
                var result = service.AddJobTitle(addJobTitle.NewJobTitle);

                if (result.isSuccess)
                {
                    var option = service.UpdateJobTitleSelect();
                    return Json(new { result.isSuccess, result.message, option });
                }
                else
                {
                    return Json(new { result.isSuccess, result.message });
                }
            }
            else
            {
                var allVaildationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { isSuccess = false, message = allVaildationErrors });
            }
        }

        // 編輯職位，成功就傳回更新後的選單item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobTitle(EditJobTitle editJobTitle)
        {
            if (ModelState.IsValid)
            {
                var service = new AssnService();
                var result = service.EditJobTitle(editJobTitle);

                if (result.isSuccess)
                {
                    var option = service.UpdateJobTitleSelect();
                    return Json(new { result.isSuccess, result.message, option, titleName = editJobTitle.NewJobTitle });
                }
                else
                {
                    return Json(new { result.isSuccess, result.message });
                }
            }
            else
            {
                var allVaildationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { isSuccess = false, message = allVaildationErrors });
            }
        }

        // 新增協會成員
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAssnMember(AddAssnMember addAssnMember)
        {
            if (ModelState.IsValid)
            {
                var service = new AssnService();
                var result = service.AddAssnMember(addAssnMember);

                if (result.isSuccess)
                {
                    var latestMember = service.GetLatestAssnMember();
                    return Json(new { result.isSuccess, result.message, latestMember });
                }
                else
                {
                    return Json(new { result.isSuccess, result.message });
                }
            }
            else
            {
                var allVaildationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { isSuccess = false, message = allVaildationErrors });
            }
        }

        // 取得編輯成員資料API
        [HttpGet]
        public ActionResult GetEditAssnMember(int id)
        {
            var service = new AssnService();
            var memberInfo = service.GetEditAssnMember(id);

            if (memberInfo != null)
            {
                return Json(new { isSuccess = true, data = memberInfo }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { isSuccess = false, message = "找不到會員資料" });
        }

        // 編輯協會成員
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAssnMember(EditAssnMember editAssnMember)
        {
            if (ModelState.IsValid)
            {
                var service = new AssnService();
                var result = service.EditAssnMember(editAssnMember);

                if (result.isSuccess)
                {
                    var revisionMemberInfo = service.GetRevisionAssnMember(editAssnMember.Id);
                    return Json(new { result.isSuccess, result.message, data = revisionMemberInfo });
                }
                else
                {
                    return Json(new { result.isSuccess, result.message });
                }
            }
            else
            {
                var allVaildationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { isSuccess = false, message = allVaildationErrors });
            }
        }

        // 刪除協會成員
        [HttpPost]
        public ActionResult DeleteAssnMember(int id)
        {
            var service = new AssnService();
            var result = service.DeleteAssnMember(id);
            return Json(new { result.isSuccess, result.message });
        }

        // 依據JobTitleId篩選成員API
        [HttpGet]
        public ActionResult FilterAssnMember(int? jobTitleId)
        {
            var service = new AssnService();
            var memberList = service.GetAssnMemberByJobTitleId(jobTitleId);
            return Json(memberList, JsonRequestBehavior.AllowGet);
        }

        // CKE上傳照片，變數名稱必須為upload
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                // 允許的照片格式
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                // 取得上傳的照片格式
                string extension = Path.GetExtension(upload.FileName);

                // 檢查是否為允許的格式
                if (allowedExtensions.Contains(extension.ToLower()))
                {
                    // 產生檔名以及檔案路徑
                    string fileName = $"{Guid.NewGuid().ToString()}{extension}";
                    string filePath = Path.Combine(Server.MapPath("~/UploadImages/"), fileName);

                    // 儲存檔案
                    upload.SaveAs(filePath);

                    // 回傳JSON格式給CKE，需要有uploaded以及url兩個key
                    return Json(new { uploaded = true, url = $"/UploadImages/{fileName}" });
                }
                return Json(new { uploaded = false, error = "只接受 .jpg、.jpeg、.png的照片格式" });
            }
            return Json(new { uploaded = false, error = "請選擇檔案" });
        }

        // 更新成員資料
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemberDetailedInfo(MemberDetailedInfoVM memberDetailedInfo)
        {
            int selectAssnMemberId;

            if (!int.TryParse(Request.Form["SelectAssnMemberId"], out selectAssnMemberId))
            {
                return Json(new { isSuccess = false, message = "必須選擇成員" });
            }

            if (ModelState.IsValid)
            {
                var service = new AssnService();
                var result = service.UpdateMemberDetailedInfo(selectAssnMemberId, memberDetailedInfo.Information);

                if (result.isSuccess)
                {
                    var info = service.GetRevisionMemberDetailedInfo(selectAssnMemberId);
                    return Json(new { result.isSuccess, result.message, data = info });
                }
                else
                {
                    return Json(new { result.isSuccess, result.message });
                }
            }
            else
            {
                var allVaildationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { isSuccess = false, message = allVaildationErrors });
            }
        }

        // 依據成員Id取得對應的詳細資料
        [HttpGet]
        public ActionResult GetMemberDetailedInfo(int memberId)
        {
            var service = new AssnService();
            var result = service.GetMemberDetailedInfo(memberId);

            if (result.isSuccess)
            {
                return Json(new { result.isSuccess, result.detailedInfo }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result.isSuccess, result.detailedInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}