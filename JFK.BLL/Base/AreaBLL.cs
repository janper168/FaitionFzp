using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class AreaBLL
    {
        private BaseDbContext _dbContext = null;
        private AreaService _areaService = null;

        public AreaBLL()
        {
            _dbContext = new BaseDbContext();
            _areaService = new AreaService(_dbContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void AddArea(Area entity)
        {
            entity.Create();

            _areaService.Add(entity);
        }

        public void UpdateArea(Area entity)
        {
            _areaService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void RemoveArea(Area entity)
        {

            if (IsExists(a => a.ParentId == entity.AreaId)) 
            {
                throw new Exception("存在下级区域");
            }

            _areaService.Remove(entity);          
        }


        public Area GetArea(string keyValue)
        {

            var area = _areaService.FindList(a => a.AreaId == keyValue,
                a => a.SortCode)[0];

            return area;

        }

        public IList<Area> GetAreas(Expression<Func<Area, bool>> whereExpression)
        {

            var areas = _areaService.FindList(whereExpression,
                a => a.SortCode);

            return areas;
        }

        public bool IsExists(Expression<Func<Area, bool>> whereExpression)
        {
            return _areaService.IsExists(whereExpression);
        }
    }
}
