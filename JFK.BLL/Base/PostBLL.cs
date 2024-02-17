using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class PostBLL
    {
        private BaseDbContext _dbContext = null;
        private DepartmentService _departmentService = null;
        private PostService _postService = null;
        private PostUserService _postUserService = null;
        private BaseBLL  _baseBLL = new BaseBLL();


        public PostBLL()
        {
            _dbContext = new BaseDbContext();
            _departmentService = new DepartmentService(_dbContext);
            _postService = new PostService(_dbContext);
            _postUserService = new PostUserService(_dbContext);
        }

        public void AddPost(Post entity)
        {
            entity.Create();
            _postService.Add(entity);
        }

        public void UpdatePost(Post entity)
        {
            _postService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdatePostUsers(Post entity)
        {
             _postService.UpdateUserList(entity);
        }

        public void RemovePost(Post entity)
        {
            _postService.Remove(entity);
        }

        public Post GetPost(string keyValue)
        {

            //取第一个
            var post = _postService.FindList(a => a.PostId == keyValue, 
                includeExpression1:a=>a.Department,
                includeExpression2: a => a.Department.Company,
                a => a.Name)[0];

            return post;

        }

        public IList<User> GetPostUsers(string postId)
        {
            var postUsers = _postUserService.FindList(a => a.PostId == postId,
                includeExpression1: a => a.User,
                includeExpression2: a => a.User.Department,
                includeExpression3: a => a.User.Department.Company,
                a => a.User.RealName).Select(a=>a.User).ToList();
            return postUsers;
        }

        public IList<Post> GetPosts(Expression<Func<Post, bool>> whereExpression)
        {

            var posts = _postService.FindList(whereExpression,
                includeExpression1: a => a.Department,
                includeExpression2: a => a.Department.Company,
                a => a.Name);

            return posts;
        }

        public IList<Post> GetPosts(string keyWord,string departmentId)
        {

            keyWord = keyWord ?? "";

            //var allposts = this.GetPosts(a => a.Department.DepartmentId == departmentId &&
            //(a.EnabledMark == 1 && a.DeleteMark == 0)).ToList();
            //if (allposts == null || allposts.Count <=0)
            //{
            //    return null;
            //}

            //if (keyWord == "")
            //{
            //    return allposts;
            //}

            var keyWordPosts = this.GetPosts(a =>
                (a.EnCode.Contains(keyWord) ||
                a.Name.Contains(keyWord) ||
                a.Description.Contains(keyWord)) &&
                (a.EnabledMark == 1 && a.DeleteMark == 0) &&
                 a.Department.DepartmentId == departmentId
                );
            return keyWordPosts;

            //if (keyWordPosts == null || keyWordPosts.Count <= 0)
            //{
            //    return null;
            //}
            //var resultposts = new List<Post>();

            //if (allposts.Count == keyWordPosts.Count)
            //{
            //    resultposts = allposts;
            //}
            //else
            //{
            //    foreach (var keyWordPost in keyWordPosts)
            //    {
            //        AddParentposts(resultposts, keyWordPost, allposts);
            //    }
            //}
            //return resultposts;
        }

        /// <summary>
        /// 添加父亲节点到结果集中
        /// </summary>
        /// <param name="resuleposts"></param>
        /// <param name="keypost"></param>
        /// <param name="allposts"></param>
        private void AddParentposts(List<Post> resultposts, Post keypost, List<Post> allposts)
        {
            //先添加自身
            var self = allposts.Single(a => a.PostId == keypost.PostId);
            if (!resultposts.Contains(self))
            {
                resultposts.Add(self);
            }


            //再添加父亲
            if (keypost.ParentId == null || keypost.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = keypost.ParentId;
                do
                {
                    var parentpost = allposts.Single(a => a.PostId == parentId);
                    if (!resultposts.Contains(parentpost))
                    {
                        resultposts.Add(parentpost);
                        parentId = parentpost.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }

        public bool IsExists(Expression<Func<Post, bool>> whereExpression)
        {
            return _postService.IsExists(whereExpression);
        }

        public Department GetDepartment(string keyValue, bool beTraced = false)
        {

            //取第一个
            var department = _departmentService.FindList(a => a.DepartmentId == keyValue,
                a => a.SortCode, true, beTraced)[0];

            return department;

        }

        public string GetDownLevelPostIds(User user)
        {
            string postIds = "";
            if (user.PostUserList != null && user.PostUserList.Count > 0)
            {
                foreach (var postUser in user.PostUserList)
                {
                    postIds += postUser.PostId + ",";

                    postIds += GetDownLevelPostIds(postUser.PostId);
                }
                postIds = postIds.Remove(postIds.Length - 1, 1);
            }

            postIds = postIds.ToList(",").Distinct().ToList().ToString(",");

            return postIds;
        }

        public string GetDownLevelPostIds(string postIds)
        {
            var retPostIds = "";
            var downPostIds = _postService.FindList(a => a.ParentId == postIds, a => a.PostId).Select(a=>a.PostId).ToList();
            foreach (var id in downPostIds)
            {
                retPostIds += id + ",";
                retPostIds += GetDownLevelPostIds(id);
            }
            return retPostIds;

        }

    }
}
