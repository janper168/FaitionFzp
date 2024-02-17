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
    public class InterfaceBLL
    {
        private BaseDbContext _dbContext = null;
      
        private InterfaceService _interfaceService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public InterfaceBLL()
        {
            _dbContext = new BaseDbContext();
            _interfaceService = new InterfaceService(_dbContext);
        }

        public void AddInterface(Interface entity)
        {
            entity.Create();
            _interfaceService.Add(entity);
        }

        public void UpdateInterface(Interface entity)
        {
            _interfaceService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void RemoveInterface(Interface entity)
        {
            _interfaceService.Remove(entity);
        }

        public void RemoveInterfaces(List<Interface> entities)
        {
            _interfaceService.RemoveRange(entities);
        }

        public Interface GetInterface(string keyValue, bool beTraced = false)
        {

            //取第一个
            var entity = _interfaceService.FindList(a => a.InterfaceId == keyValue,          
                a => a.Name, true, beTraced)[0];

            return entity;

        }

        public IList<Interface> GetInterfaces(Expression<Func<Interface, bool>> whereExpression)
        {

            
            var interfaces = _interfaceService.FindList(whereExpression,
                a=>a.Name);

            return interfaces;
        }

      
        public IList<Interface> GetInterfaces(Pagination pagination, Sort sort, Expression<Func<Interface, bool>> whereExpression)
        {

            return _baseBLL.GetObjects(_interfaceService, pagination, sort, whereExpression);
        }


        public bool IsExists(Expression<Func<Interface, bool>> whereExpression)
        {
            return _interfaceService.IsExists(whereExpression);
        }

    }
}
