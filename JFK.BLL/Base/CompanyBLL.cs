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
    public class CompanyBLL
    {
        private BaseDbContext _dbContext = null;
        private CompanyService _companyService = null;

        public CompanyBLL()
        {
            _dbContext = new BaseDbContext();
            _companyService = new CompanyService(_dbContext);
        }

        public void AddCompany(Company entity)
        {
            entity.Create();
            _companyService.Add(entity);
        }

        public void UpdateCompany(Company entity)
        {
            _companyService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void RemoveCompany(Company entity)
        {
            _companyService.Remove(entity);
        }

        public Company GetCompany(string keyValue, bool beTraced = false)
        {

            //取第一个
            var company = _companyService.FindList(a => a.CompanyId == keyValue,          
                a => a.SortCode, true, beTraced)[0];

            return company;

        }

        public IList<Company> GetCompanys(Expression<Func<Company, bool>> whereExpression)
        {

            var companys = _companyService.FindList(whereExpression,  
                a => a.SortCode);

            return companys;
        }

        public IList<Company> GetCompanys(string keyWord)
        {

            //var allCompanys = this.GetCompanys(a => true).ToList();
            //if (allCompanys == null || allCompanys.Count <= 0)
            //{
            //    return null;
            //}
            //if (keyWord == "")
            //{
            //    return allCompanys;
            //}

            var keyWordcompanys = this.GetCompanys(a =>
                (a.EnCode.Contains(keyWord) ||
                a.FullName.Contains(keyWord) ||
                a.ShortName.Contains(keyWord) ||
                a.Manager.Contains(keyWord) ||
                a.BusinessScope.Contains(keyWord) ||
                a.Description.Contains(keyWord)) && (a.EnabledMark == 1 && a.DeleteMark == 0)
                );
            return keyWordcompanys;
            //if (keyWordcompanys == null || keyWordcompanys.Count <= 0)
            //{
            //    return keyWordcompanys;
            //}

            //var resultCompanys = new List<Company>();

            //if (allCompanys.Count == keyWordcompanys.Count)
            //{
            //    resultCompanys = allCompanys;
            //}
            //else
            //{
            //    foreach (var keyWordCompany in keyWordcompanys)
            //    {
            //        AddParentCompanys(resultCompanys, keyWordCompany, allCompanys);
            //    }
            //}
            //return resultCompanys;

        }

        /// <summary>
        /// 添加父亲节点到结果集中
        /// </summary>
        /// <param name="resuleCompanys"></param>
        /// <param name="keyCompany"></param>
        /// <param name="allCompanys"></param>
        private void AddParentCompanys(List<Company> resultCompanys, Company keyCompany, List<Company> allCompanys)
        {
            //先添加自身
            var self = allCompanys.Single(a => a.CompanyId == keyCompany.CompanyId);
            if (!resultCompanys.Contains(self))
            {
                resultCompanys.Add(self);
            }


            //再添加父亲
            if (keyCompany.ParentId == null || keyCompany.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = keyCompany.ParentId;
                do
                {
                    var parentCompany = allCompanys.Single(a => a.CompanyId == parentId);
                    if (!resultCompanys.Contains(parentCompany))
                    {
                        resultCompanys.Add(parentCompany);
                        parentId = parentCompany.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }

        public bool IsExists(Expression<Func<Company, bool>> whereExpression)
        {
            return _companyService.IsExists(whereExpression);
        }

        public string GetDownLevelCompanyIds(User user)
        {
            string companyIds = user.Department.Company.CompanyId + ",";


            companyIds += GetDownLevelCompanyIds(user.Department.Company.CompanyId);

            companyIds = companyIds.Remove(companyIds.Length - 1, 1);

            companyIds = companyIds.ToList(",").Distinct().ToList().ToString(",");

            return companyIds;
        }

        public string GetDownLevelCompanyIds(string companyId)
        {
            var retIds = "";
            var downIds = _companyService.FindList(a => a.ParentId == companyId, a => a.CompanyId).Select(a => a.CompanyId).ToList();
            foreach (var id in downIds)
            {
                retIds += id + ",";
                retIds += GetDownLevelCompanyIds(id);
            }
            return retIds;
        }

    }
}
