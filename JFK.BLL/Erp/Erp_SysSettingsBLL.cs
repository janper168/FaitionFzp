using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class SysSettingsConfig
    { 

        public static Dictionary<string,string>  configs = new Dictionary<string,string>();
        public static void InitSysSettingsConfig() 
        {
            var datas = new Erp_SysSettingsBLL().GetEntities(a => true);
            foreach (var data in datas)
            {
                configs.Add(data.KeyName, data.KeyValue);
            }
        }

    }

    public class Erp_SysSettingsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SysSettingsService _Erp_SysSettingsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SysSettingsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SysSettingsService = new Erp_SysSettingsService(_dbContext);
        }

        public void AddEntity(Erp_SysSettings entity)
        {
            entity.Create();
            _Erp_SysSettingsService.Add(entity);
            SysSettingsConfig.configs.Add(entity.KeyName, entity.KeyValue);
        }

        public void AddEntities(IEnumerable<Erp_SysSettings> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SysSettingsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SysSettings entity)
        {
            _Erp_SysSettingsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
            SysSettingsConfig.configs[entity.KeyName] =  entity.KeyValue;
        }

        public void UpdateEntities(IEnumerable<Erp_SysSettings> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SysSettingsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SysSettings entity)
        {
            _Erp_SysSettingsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SysSettings> entities)
        {
            _Erp_SysSettingsService.RemoveRange(entities);
        }

        public Erp_SysSettings GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SysSettingsService.FindList(a => a.Erp_SysSettingsId == keyValue,a => a.Erp_SysSettingsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SysSettings> GetEntities(Expression<Func<Erp_SysSettings, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SysSettingsService.FindList(whereExpression, a => a.Erp_SysSettingsId, isSortAsc);
             return data;
        }

        public IList<Erp_SysSettings> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_SysSettings, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SysSettingsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_SysSettings, bool>> whereExpression)
        {
            return _Erp_SysSettingsService.IsExists(whereExpression);
        }

    }
}
