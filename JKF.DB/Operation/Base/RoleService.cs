using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(BaseDbContext baseDbContext) : base(baseDbContext) { }

        public void UpdateRoleList(Role entity)
        {

            var existsEntity = this.Single(entity.RoleId);
            if (existsEntity != null)
            {

                _baseDbContext.Attach(existsEntity);
                existsEntity.RoleUserList.Clear();

                if (entity.RoleUserList != null)
                    foreach (var item in entity.RoleUserList)
                        existsEntity.RoleUserList.Add(item);
            }

            _baseDbContext.SaveChanges();
        }
    }
}
