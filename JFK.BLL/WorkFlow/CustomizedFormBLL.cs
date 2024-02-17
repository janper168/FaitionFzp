using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using NPOI.POIFS.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class CustomizedFormBLL
    {
        private BaseDbContext _dbContext = null;
        private CustomizedFormService _CustomizedFormService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public CustomizedFormBLL()
        {
            _dbContext = new BaseDbContext();
            _CustomizedFormService = new CustomizedFormService(_dbContext);
        }

        public void AddEntity(CustomizedForm entity)
        {
            entity.Create();
            _CustomizedFormService.Add(entity);
        }

        public void AddEntities(IEnumerable<CustomizedForm> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _CustomizedFormService.AddRange(entities);
        }

        public void UpdateEntity(CustomizedForm entity)
        {
            entity.Update();
            _CustomizedFormService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<CustomizedForm> entities)
        {
           foreach (var e in entities)
            {
                e.Update();
                _CustomizedFormService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(CustomizedForm entity)
        {
            _CustomizedFormService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<CustomizedForm> entities)
        {
            _CustomizedFormService.RemoveRange(entities);
        }

        public CustomizedForm GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _CustomizedFormService.FindList(a => a.CustomizedFormId == keyValue,a => a.CustomizedFormId, true, beTraced)[0];
             return entity;
        }

        public IList<CustomizedForm> GetEntities(Expression<Func<CustomizedForm, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _CustomizedFormService.FindList(whereExpression, a => a.CustomizedFormId, isSortAsc);
             return data;
        }

        public IList<CustomizedForm> GetEntities(Pagination pagination, Sort sort, Expression<Func<CustomizedForm, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_CustomizedFormService, pagination, sort, whereExpression,a=>a.CreateUser,a=>a.UpdateUser);
        }

        public bool IsExists(Expression<Func<CustomizedForm, bool>> whereExpression)
        {
            return _CustomizedFormService.IsExists(whereExpression);
        }

    }
}
