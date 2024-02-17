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
    public class ChatUserSessionBLL
    {
        private BaseDbContext _dbContext = null;
        private ChatUserSessionService _ChatUserSessionService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public ChatUserSessionBLL()
        {
            _dbContext = new BaseDbContext();
            _ChatUserSessionService = new ChatUserSessionService(_dbContext);
        }

        public void AddEntity(ChatUserSession entity)
        {
            entity.Create();
            _ChatUserSessionService.Add(entity);
        }

        public void AddEntities(IEnumerable<ChatUserSession> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _ChatUserSessionService.AddRange(entities);
        }

        public void UpdateEntity(ChatUserSession entity)
        {
            _ChatUserSessionService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<ChatUserSession> entities)
        {
           foreach (var e in entities)
            {
                _ChatUserSessionService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(ChatUserSession entity)
        {
            _ChatUserSessionService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<ChatUserSession> entities)
        {
            _ChatUserSessionService.RemoveRange(entities);
        }

        public ChatUserSession GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _ChatUserSessionService.FindList(a => a.ChatUserSessionId == keyValue,a => a.ChatUserSessionId, true, beTraced)[0];
             return entity;
        }

        public IList<ChatUserSession> GetEntities(Expression<Func<ChatUserSession, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _ChatUserSessionService.FindList(whereExpression, a => a.ChatUserSessionId, isSortAsc);
             return data;
        }

        public IList<ChatUserSession> GetEntites(Pagination pagination, Sort sort, Expression<Func<ChatUserSession, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_ChatUserSessionService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<ChatUserSession, bool>> whereExpression)
        {
            return _ChatUserSessionService.IsExists(whereExpression);
        }

    }
}
