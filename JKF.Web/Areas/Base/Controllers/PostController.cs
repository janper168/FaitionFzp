using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class PostController : BaseController
    {
        private PostBLL _postBLL = new PostBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult PostMemberForm()
        {
            return View();
        }

        public IActionResult GetPostUsers(string postId) {

            return this.ExecuteAction(() =>
            {
                var users = _postBLL.GetPostUsers(postId);

                return JsonSuccess(users );

            });

           
        }
        [InterfaceDefine("/Base/User/GetPosts", "JKF.DB.Models.Post")]
        public IActionResult GetPosts(string keyWord, string departmentId)
        {
            return this.ExecuteAction(() =>
            {
                var resultposts = _postBLL.GetPosts(keyWord, departmentId);
                return JsonSuccess(resultposts);
            });
            
           
        }
       
        public IActionResult GetForm(string postId)
        {

            return this.ExecuteAction(() =>
            {
                var post = _postBLL.GetPost(postId);

                return JsonSuccess(post);
            });


          
        }

        public IActionResult SaveForm(Post post, string departmentId)
        {
            return this.ExecuteAction(() =>
            {

                var department = _postBLL.GetDepartment(departmentId, beTraced: true);
                if (department == null)
                {
                    throw new Exception("部门数据出错：找不到");
                }

                if (string.IsNullOrEmpty(post.PostId))
                {
                    post.Department = department;
                    _postBLL.AddPost(post);
                }
                else
                {
                    post.Department = department;
                    _postBLL.UpdatePost(post);
                }


                return JsonSuccess(null);
            });

              
        }

        public IActionResult RemoveForm(string postId)
        {

            return this.ExecuteAction(() =>
            {
                int code = 0;
                string errMsg = "";

                if (!string.IsNullOrEmpty(postId))
                {
                    if (_postBLL.IsExists(a => a.ParentId == postId))
                    {
                        code = 400;
                        errMsg = "存在下级岗位，无法删除！";
                        return JsonFailure(code, errMsg);
                    }
                    else
                    {
                        _postBLL.RemovePost(new Post { PostId = postId });
                        return JsonSuccess(null);
                    }
                }
                return JsonSuccess(null);
            });

  
        }

        public IActionResult SaveMemberForm(Post post)
        {
            return this.ExecuteAction(() =>
            {            
                _postBLL.UpdatePostUsers(post);
              
                return JsonSuccess(null);
            });
        }

        [HttpPost]
        public IActionResult IsExistEnCode(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _postBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _postBLL.IsExists(a => a.EnCode == paramValue && a.PostId != keyValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }


            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult IsExistName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _postBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _postBLL.IsExists(a => a.Name == paramValue && a.PostId != keyValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }


            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }

    }
}
