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
    public class ChatMessageBLL
    {
        private BaseDbContext _dbContext = null;
        private ChatMessageService _ChatMessageService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public ChatMessageBLL()
        {
            _dbContext = new BaseDbContext();
            _ChatMessageService = new ChatMessageService(_dbContext);
        }

        public void AddEntity(ChatMessage entity)
        {
            entity.Create();
            _ChatMessageService.Add(entity);
        }

        public void AddEntities(IEnumerable<ChatMessage> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _ChatMessageService.AddRange(entities);
        }

        public void UpdateEntity(ChatMessage entity)
        {
            _ChatMessageService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<ChatMessage> entities)
        {
           foreach (var e in entities)
            {
                _ChatMessageService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(ChatMessage entity)
        {
            _ChatMessageService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<ChatMessage> entities)
        {
            _ChatMessageService.RemoveRange(entities);
        }

        public ChatMessage GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _ChatMessageService.FindList(a => a.ChatMessageId == keyValue,a => a.ChatMessageId, true, beTraced)[0];
             return entity;
        }

        public IList<ChatMessage> GetEntities(Expression<Func<ChatMessage, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _ChatMessageService.FindList(whereExpression,a=>a.SendUser, a => a.Time, isSortAsc);
             return data;
        }

        public IList<ChatMessage> GetEntites(Pagination pagination, Sort sort, Expression<Func<ChatMessage, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_ChatMessageService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<ChatMessage, bool>> whereExpression)
        {
            return _ChatMessageService.IsExists(whereExpression);
        }

    }
}
