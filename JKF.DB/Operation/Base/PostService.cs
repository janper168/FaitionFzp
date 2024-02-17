using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class PostService : BaseService<Post>
    {
        public PostService(BaseDbContext baseDbContext) : base(baseDbContext) { }

        public void UpdateUserList(Post entity)
        {

            var existsEntity = this.Single(entity.PostId);
            if (existsEntity != null)
            {
                
                _baseDbContext.Attach(existsEntity);
                existsEntity.PostUserList.Clear();

                if (entity.PostUserList != null)
                    foreach (var item in entity.PostUserList)
                        existsEntity.PostUserList.Add(item);
            }

            _baseDbContext.SaveChanges();
        }

    }
}
