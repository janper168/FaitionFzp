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
    public class DepartmentBLL
    {
        private BaseDbContext _dbContext = null;
        private DepartmentService _departmentService = null;
        private CompanyService _companyService = null;

        public DepartmentBLL()
        {
            _dbContext = new BaseDbContext();
            _departmentService = new DepartmentService(_dbContext);
            _companyService = new CompanyService(_dbContext);
        }

        public void AddDepartment(Department entity)
        {
            entity.Create();
            _departmentService.Add(entity);
        }

        public void UpdateDepartment(Department entity)
        {
            _departmentService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void RemoveDepartment(Department entity)
        {
            _departmentService.Remove(entity);
        }

        public Department GetDepartment(string keyValue)
        {

            //取第一个
            var department = _departmentService.FindList(a => a.DepartmentId == keyValue,          
                a => a.SortCode)[0];

            return department;

        }

        public IList<Department> GetDepartments(string keyWord,string companyId)
        {

            keyWord = keyWord ?? "";

            //var allDepartments = this.GetDepartments(a => a.Company.CompanyId == companyId &&
            //(a.EnabledMark == 1 && a.DeleteMark == 0)).ToList();
            //if (allDepartments == null || allDepartments.Count <= 0)
            //{
            //    return null;
            //}
            //if (keyWord == "")
            //{
            //    return allDepartments;
            //}
            var resultDepartments = this.GetDepartments(a =>
                (a.EnCode.Contains(keyWord) ||
                a.FullName.Contains(keyWord) ||
                a.ShortName.Contains(keyWord) ||
                a.Manager.Contains(keyWord) ||
                a.Description.Contains(keyWord)) &&
                (a.EnabledMark == 1 && a.DeleteMark == 0) &&
                 a.Company.CompanyId == companyId
                );
            return resultDepartments;
            //if (keyWordcompanys == null || allDepartments.Count <= 0)
            //{
            //    return null;
            //}

            //var resultDepartments = new List<Department>();

            //if (allDepartments.Count == keyWordcompanys.Count)
            //{
            //    resultDepartments = allDepartments;
            //}
            //else
            //{
            //    foreach (var keyWordDepartment in keyWordcompanys)
            //    {
            //        AddParentDepartments(resultDepartments, keyWordDepartment, allDepartments);
            //    }
            //}
            //return resultDepartments;
        }

        public IList<Department> GetDepartments(Expression<Func<Department, bool>> whereExpression)
        {

            var departments = _departmentService.FindList(whereExpression,
                a => a.SortCode);

            return departments;
        }

        /// <summary>
        /// 添加父亲节点到结果集中
        /// </summary>
        /// <param name="resuleDepartments"></param>
        /// <param name="keyDepartment"></param>
        /// <param name="allDepartments"></param>
        private void AddParentDepartments(List<Department> resultDepartments, Department keyDepartment, List<Department> allDepartments)
        {
            //先添加自身
            var self = allDepartments.Single(a => a.DepartmentId == keyDepartment.DepartmentId);
            if (!resultDepartments.Contains(self))
            {
                resultDepartments.Add(self);
            }


            //再添加父亲
            if (keyDepartment.ParentId == null || keyDepartment.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = keyDepartment.ParentId;
                do
                {
                    var parentDepartment = allDepartments.Single(a => a.DepartmentId == parentId);
                    if (!resultDepartments.Contains(parentDepartment))
                    {
                        resultDepartments.Add(parentDepartment);
                        parentId = parentDepartment.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }

        public bool IsExists(Expression<Func<Department, bool>> whereExpression)
        {
            return _departmentService.IsExists(whereExpression);
        }

        public Company GetCompany(string keyValue, bool beTraced = false)
        {

            //取第一个
            var company = _companyService.FindList(a => a.CompanyId == keyValue,
                a => a.SortCode, true, beTraced)[0];

            return company;

        }

        public string GetDownLevelDepartmentIds(User user)
        {
            string departmentIds = user.Department.DepartmentId + ",";


            departmentIds += GetDownLevelDepartmentIds(user.Department.DepartmentId);

            departmentIds = departmentIds.Remove(departmentIds.Length - 1, 1);

            departmentIds = departmentIds.ToList(",").Distinct().ToList().ToString(",");

            return departmentIds;
        }

        public string GetDownLevelDepartmentIds(string departmentId)
        {
            var retIds = "";
            var downIds = _departmentService.FindList(a => a.ParentId == departmentId, a => a.DepartmentId).Select(a => a.DepartmentId).ToList();
            foreach (var id in downIds)
            {
                retIds += id + ",";
                retIds += GetDownLevelDepartmentIds(id);
            }

            return retIds;
        }

    }
}
