using JKF.Commons;
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
    public class RoleBLL
    {
        private BaseDbContext _dbContext = null;
        private RoleService _roleService = null;
        private RoleUserService _roleUserService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public RoleBLL()
        {
            _dbContext = new BaseDbContext();
            _roleService = new RoleService(_dbContext);
            _roleUserService = new RoleUserService(_dbContext);

        }

        public void AddRole(Role entity)
        {
            entity.Create();
            _roleService.Add(entity);
        }

        public void UpdateRole(Role entity)
        {
            _roleService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void RemoveRole(Role entity)
        {
            _roleService.Remove(entity);
        }

        public void RemoveRoles(List<Role> entities)
        {
            _roleService.RemoveRange(entities);
        }

        public Role GetRole(string keyValue, bool beTraced = false)
        {

            //取第一个
            var role = _roleService.FindList(a => a.RoleId == keyValue,          
                a => a.SortCode, true, beTraced)[0];

            return role;

        }

        public IList<Role> GetRoles(Expression<Func<Role, bool>> whereExpression)
        {

            
            var roles = _roleService.FindList(whereExpression,
                a=>a.SortCode);

            return roles;
        }

        public IList<User> GetRoleUsers(string roleId)
        {
            var postUsers = _roleUserService.FindList(a => a.RoleId == roleId,
                includeExpression1: a => a.User,
                includeExpression2: a => a.User.Department,
                includeExpression3: a => a.User.Department.Company,
                a => a.User.RealName).Select(a => a.User).ToList();
            return postUsers;
        }

        public IList<Role> GetRoles(Pagination pagination, Sort sort, Expression<Func<Role, bool>> whereExpression)
        {

            return _baseBLL.GetObjects(_roleService, pagination, sort, whereExpression);
        }


        public bool IsExists(Expression<Func<Role, bool>> whereExpression)
        {
            return _roleService.IsExists(whereExpression);
        }

        public void UpdateRoleUsers(Role entity)
        {
            _roleService.UpdateRoleList(entity);
        }


    }
}
