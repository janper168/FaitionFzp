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
    public class DataAuthorizeBLL
    {
        private BaseDbContext _dbContext = null;
        private DataAuthorizeService _dataAuthorizeService = null;
        private InterfaceService _interfaceService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public DataAuthorizeBLL()
        {
            _dbContext = new BaseDbContext();
            _dataAuthorizeService = new DataAuthorizeService(_dbContext);
            _interfaceService = new InterfaceService(_dbContext);

        }

        public void AddDataAuthorize(DataAuthorize entity)
        {
            entity.Create();
            _dataAuthorizeService.Add(entity);
        }

        public void UpdateDataAuthorize(DataAuthorize entity)
        {
            _dataAuthorizeService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void RemoveDataAuthorize(DataAuthorize entity)
        {
            _dataAuthorizeService.Remove(entity);
        }

        public void RemoveDataAuthorizes(List<DataAuthorize> entities)
        {
            _dataAuthorizeService.RemoveRange(entities);
        }

        public DataAuthorize GetDataAuthorize(string keyValue, bool beTraced = false)
        {
            //取第一个
            var dataAuthorize = _dataAuthorizeService.FindList(a => a.DataAuthorizeId == keyValue,
                includeExpressoin:a=>a.Interface,
                a => a.DataAuthorizeId, true, beTraced)[0];

            return dataAuthorize;
        }

        public IList<DataAuthorize> GetDataAuthorizes(Expression<Func<DataAuthorize, bool>> whereExpression)
        {
            var datas = _dataAuthorizeService.FindList(whereExpression,
                a => a.Interface,
                a => a.Interface.Name);

            return datas;
        }

        public IList<DataAuthorize> GetDataAuthorizes(Pagination pagination, Sort sort, Expression<Func<DataAuthorize, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_dataAuthorizeService, pagination, sort, whereExpression, a=>a.Interface, a=>a.Interface);
        }

        public Interface GetInterface(string keyValue, bool beTraced = false)
        {

            //取第一个
            var inter = _interfaceService.FindList(a => a.InterfaceId == keyValue,
                a => a.InterfaceId, true, beTraced)[0];

            return inter;

        }


        public bool IsExists(Expression<Func<DataAuthorize, bool>> whereExpression)
        {
            return _dataAuthorizeService.IsExists(whereExpression);
        }



    }
}
