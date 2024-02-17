using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class ConditionRuleBLL
    {
        private BaseDbContext _dbContext = null;
        private ConditionRuleService _ConditionRuleService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public ConditionRuleBLL()
        {
            _dbContext = new BaseDbContext();
            _ConditionRuleService = new ConditionRuleService(_dbContext);
        }

        public void AddEntity(ConditionRule entity)
        {
            entity.Create();
            _ConditionRuleService.Add(entity);
        }

        public void AddEntities(IEnumerable<ConditionRule> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _ConditionRuleService.AddRange(entities);
        }

        public void UpdateEntity(ConditionRule entity)
        {
            _ConditionRuleService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<ConditionRule> entities)
        {
           foreach (var e in entities)
            {
                _ConditionRuleService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(ConditionRule entity)
        {
            _ConditionRuleService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<ConditionRule> entities)
        {
            _ConditionRuleService.RemoveRange(entities);
        }

        public ConditionRule GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _ConditionRuleService.FindList(a => a.ConditionRuleId == keyValue, a => a.ConditionRuleId, true, beTraced)[0];
            return entity;
        }
        public ConditionRule GetEntity(Expression<Func<ConditionRule, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _ConditionRuleService.FindList(whereExpression, a => a.ConditionRuleId, true, beTraced)[0];
             return entity;
        }

        public IList<ConditionRule> GetEntities(Expression<Func<ConditionRule, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _ConditionRuleService.FindList(whereExpression, a => a.ConditionRuleId, isSortAsc);
             return data;
        }

        public IList<ConditionRule> GetEntites(Pagination pagination, Sort sort, Expression<Func<ConditionRule, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_ConditionRuleService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<ConditionRule, bool>> whereExpression)
        {
            return _ConditionRuleService.IsExists(whereExpression);
        }

    }
}
