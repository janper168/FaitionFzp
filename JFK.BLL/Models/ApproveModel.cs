using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class ApproveModel
    {
        public bool isAllApprove{ get; set; }
        public List<ApproveRole> roles { get; set; }
        public List<ApproveCompany> companys { get; set; }
        public List<ApproveDepartment> departments { get; set; }
        public List<ApprovePost> posts { get; set; }
        public List<ApproveUser> users { get; set; }
    }

    public class ApproveRole
    { 
        public string RoleId { get; set; }
        public string RoleNameWithEnCode { get; set; }
    }

    public class ApproveCompany
    {
        public string CompanyId { get; set; }
        public string CompanyNameWithEnCode { get; set; }
    }

    public class ApproveDepartment
    {
        public string DepartmentId { get; set; }
        public string DepartmentNameWithEnCode { get; set; }
    }

    public class ApprovePost
    {
        public string PostId { get; set; }
        public string PostNameWithEnCode { get; set; }
    }

    public class ApproveUser
    {
        public string UserId { get; set; }
        public string UserNameWithEnCode { get; set; }
    }
}
