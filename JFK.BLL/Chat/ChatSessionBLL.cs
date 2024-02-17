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
    public class ChatSessionBLL
    {
        private BaseDbContext _dbContext = null;
        private ChatSessionService _ChatSessionService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public ChatSessionBLL()
        {
            _dbContext = new BaseDbContext();
            _ChatSessionService = new ChatSessionService(_dbContext);
        }

        public void AddEntity(ChatSession entity)
        {
            entity.Create();
            _ChatSessionService.Add(entity);
        }

        public void AddEntities(IEnumerable<ChatSession> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _ChatSessionService.AddRange(entities);
        }

        public void UpdateEntity(ChatSession entity)
        {
            _ChatSessionService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<ChatSession> entities)
        {
           foreach (var e in entities)
            {
                _ChatSessionService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(ChatSession entity)
        {
            _ChatSessionService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<ChatSession> entities)
        {
            _ChatSessionService.RemoveRange(entities);
        }

        public ChatSession GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _ChatSessionService.FindList(a => a.ChatSessionId == keyValue,a => a.ChatSessionId, true, beTraced)[0];
             return entity;
        }

        public IList<ChatSession> GetEntities(Expression<Func<ChatSession, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _ChatSessionService.FindList(whereExpression, a => a.ChatSessionId, isSortAsc);
             return data;
        }

        public IList<ChatSession> GetEntites(Pagination pagination, Sort sort, Expression<Func<ChatSession, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_ChatSessionService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<ChatSession, bool>> whereExpression)
        {
            return _ChatSessionService.IsExists(whereExpression);
        }

    }
}
