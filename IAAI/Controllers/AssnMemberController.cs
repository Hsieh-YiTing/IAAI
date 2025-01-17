using Antlr.Runtime.Misc;
using IAAI.Models.ViewModel.AssnMemberVM;
using IAAI.Services.AssnMemberService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
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
                return Json(new { isSuccess = false , message = allVaildationErrors });
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
    }
}