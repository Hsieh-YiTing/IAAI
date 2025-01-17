using IAAI.Models.DbModel;
using IAAI.Models.EFModel;
using IAAI.Models.EFModel.AssnEF;
using IAAI.Models.ViewModel.AssnMemberVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.WebPages;

namespace IAAI.Services.AssnMemberService
{
    public class AssnService
    {
        DbConnect db = new DbConnect();

        // VM初始化
        public AssnMemberVM InitAssnMemberVM()
        {
            // 取得選單內容
            var listItem = db.AssnJobTitle.Select(a => new SelectListItem
            {
                Text = a.JobtTitle,
                Value = a.Id.ToString()
            }).ToList();

            // 取得協會成員資料
            var assnMember = db.AssnMembers.Select(m => new AssnMemberInfo
            {
                Id = m.Id,
                JobTitleId = m.AssnJobTitleId,
                JobTitle = m.AssnJobTitle.JobtTitle,
                Name = m.Name,
                Gender = m.Gender,
                Incumbent = m.Incumbent,
                CreateTime = m.CreateTime,
                UpdateTime = m.UpdateTime
            }).ToList();

            var editJobTitle = new EditJobTitle { Option = listItem };
            var addAssnMember = new AddAssnMember { Option = listItem };
            var editAssnMember = new EditAssnMember { Option = listItem };

            var model = new AssnMemberVM
            {
                AssnMemberInfo = assnMember,
                EditJobTitle = editJobTitle,
                AddAssnMember = addAssnMember,
                EditAssnMember = editAssnMember
            };
            return model;
        }

        // 新增職稱
        public (bool isSuccess, string message) AddJobTitle(string newJobTitle)
        {
            var isExist = db.AssnJobTitle.Any(a => a.JobtTitle == newJobTitle);

            if (!isExist)
            {
                var newTitle = new AssnJobTitle
                {
                    JobtTitle = newJobTitle
                };
                db.AssnJobTitle.Add(newTitle);
                db.SaveChanges();
                return (true, "新增成功");
            }
            return (false, "職稱已經存在");
        }

        // 編輯職稱
        public (bool isSuccess, string message) EditJobTitle(EditJobTitle editJobTitle)
        {
            var isExist = db.AssnJobTitle.Any(a => a.JobtTitle == editJobTitle.NewJobTitle);

            if (!isExist)
            {
                var existJobTitle = db.AssnJobTitle.FirstOrDefault(a => a.Id.ToString() == editJobTitle.SelectedJobTitle);
                existJobTitle.JobtTitle = editJobTitle.NewJobTitle;
                db.SaveChanges();
                return (true, "編輯成功");
            }
            return (false, "職稱已經存在");
        }

        // 更新選單
        public List<SelectListItem> UpdateJobTitleSelect()
        {
            var listItem = db.AssnJobTitle.Select(a => new SelectListItem
            {
                Text = a.JobtTitle,
                Value = a.Id.ToString()
            }).ToList();

            return listItem;
        }

        // 新增協會成員
        public (bool isSuccess, string message) AddAssnMember(AddAssnMember addAssnMember)
        {
            var isExist = db.AssnMembers.Any(a => a.Name == addAssnMember.Name && a.Incumbent == addAssnMember.Incumbent);

            if (!isExist)
            {
                var newMember = new AssnMember
                {
                    AssnJobTitleId = int.Parse(addAssnMember.SelectedJobTitle),
                    Name = addAssnMember.Name,
                    Gender = addAssnMember.Gender,
                    Incumbent = addAssnMember.Incumbent
                };

                db.AssnMembers.Add(newMember);
                db.SaveChanges();
                return (true, "新增成功");
            }
            return (false, "成員已經存在");
        }

        // 獲取新增的成員資料
        public AssnMemberInfo GetLatestAssnMember()
        {
            var newAssnMember = db.AssnMembers.OrderByDescending(m => m.CreateTime).Select(m => new AssnMemberInfo
            {
                Id = m.Id,
                JobTitleId = m.AssnJobTitleId,
                JobTitle = m.AssnJobTitle.JobtTitle,
                Name = m.Name,
                Gender = m.Gender,
                Incumbent = m.Incumbent,
                CreateTime = m.CreateTime,
                UpdateTime = m.UpdateTime
            }).FirstOrDefault();
            return newAssnMember;
        }

        // 取得要編輯的成員資料
        public AssnMemberInfo GetEditAssnMember(int id)
        {
            var searchMember = db.AssnMembers.FirstOrDefault(m => m.Id == id);
            if (searchMember != null)
            {
                var memberInfo = new AssnMemberInfo
                {
                    Id = searchMember.Id,
                    JobTitleId = searchMember.AssnJobTitleId,
                    JobTitle = searchMember.AssnJobTitle.JobtTitle,
                    Name = searchMember.Name,
                    Gender = searchMember.Gender,
                    Incumbent = searchMember.Incumbent
                };
                return memberInfo;
            }
            return null;
        }

        // 編輯協會成員
        public (bool isSuccess, string message) EditAssnMember(EditAssnMember editAssnMember)
        {
            var assnMemberInfo = db.AssnMembers.FirstOrDefault(m => m.Id == editAssnMember.Id);

            if (assnMemberInfo != null)
            {
                assnMemberInfo.AssnJobTitleId = int.Parse(editAssnMember.SelectedJobTitle);
                assnMemberInfo.Name = editAssnMember.Name;
                assnMemberInfo.Gender = editAssnMember.Gender;
                assnMemberInfo.Incumbent = editAssnMember.Incumbent;
                db.SaveChanges();
                return (true, "編輯成功");
            }
            return (false, "找不到要編輯的資料");
        }

        // 取得編輯後的成員資料
        public AssnMemberInfo GetRevisionAssnMember(int id)
        {
            var revisionMember = db.AssnMembers.FirstOrDefault(m => m.Id == id);

            if (revisionMember != null)
            {
                var memberInfo = new AssnMemberInfo
                {
                    Id = revisionMember.Id,
                    JobTitleId = revisionMember.AssnJobTitleId,
                    JobTitle = revisionMember.AssnJobTitle.JobtTitle,
                    Name = revisionMember.Name,
                    Gender = revisionMember.Gender,
                    Incumbent = revisionMember.Incumbent,
                    CreateTime = revisionMember.CreateTime,
                    UpdateTime = revisionMember.UpdateTime
                };
                return memberInfo;
            }
            return null;
        }

        // 刪除協會成員
        public (bool isSuccess, string message) DeleteAssnMember(int id)
        {
            var member = db.AssnMembers.FirstOrDefault(m => m.Id == id);

            if (member != null)
            {
                db.AssnMembers.Remove(member);
                db.SaveChanges();
                return (true, "刪除成功");
            }
            return (false, "找不到要刪除的編號資料");
        }
    }
}