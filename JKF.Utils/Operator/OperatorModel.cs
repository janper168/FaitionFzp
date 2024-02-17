using System;
using System.Collections.Generic;
using System.Text;

namespace JKF.Utils
{
    /// <summary>
    /// 登录者模型
    /// </summary>
    public class OperatorModel
    {
        public string UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string RealName { get; set; }     
        public string CompanyId { get; set; }
        public string DepartmentId { get; set; }
        public string RoleIds { get; set; }
        public string PostIds { get; set; }
        public string PostIdsAndDownPostIds { get; set; }
        public string CompanyIdAndDownCompanyIds { get; set; }
        public string DepartmentIdAndDownDepartmentIds { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginIPAddressName { get; set; }
        public string LoginToken { get; set; }
        public string LoginTime { get; set; }
        public bool IsSystem { get; set; }
    }
}
