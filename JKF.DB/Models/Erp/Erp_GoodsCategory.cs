using JKF.DB.extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Models
{
    public class Erp_GoodsCategory : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_GoodsCategory()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_GoodsCategoryId;

        //分类
        private string _name;

        //备注
        private string _remark;


        //关键字
        [Required]
        [MaxLength(50)]
        public string Erp_GoodsCategoryId
        {
            get
            {
                return _erp_GoodsCategoryId;
            }
            set
            {
                _erp_GoodsCategoryId = value;
                propInfoList.PutProperty<Erp_GoodsCategory, string>(a => a.Erp_GoodsCategoryId);
            }
        }

        //分类
        [Required]
        [MaxLength(250)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<Erp_GoodsCategory, string>(a => a.Name);
            }
        }

        //备注
        [MaxLength(2000)]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                propInfoList.PutProperty<Erp_GoodsCategory, string>(a => a.Remark);
            }
        }


        public void Create()
        {
            Erp_GoodsCategoryId = Guid.NewGuid().ToString();
        }

        public void ClearModifyInfo()
        {
            propInfoList.Clear();
            referenceInfoList.Clear();
        }
        public List<PropertyInfo> GetPropInfoList()
        {
            return propInfoList;
        }

        public List<PropertyInfo> GetReferenceInfoList()
        {
            return referenceInfoList;
        }
    }
}
