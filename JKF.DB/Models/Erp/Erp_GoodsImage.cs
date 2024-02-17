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
    public class Erp_GoodsImage : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_GoodsImage()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_GoodsImageId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //图片路径
        private string _filePath;

        //图片名称
        private string _name;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_GoodsImageId
        {
            get
            {
                return _erp_GoodsImageId;
            }
            set
            {
                _erp_GoodsImageId = value;
            }
        }

        //商品
        [ForeignKey("GoodsId")]
        public virtual Erp_Goods Goods
        {
            get
            {
                return _goods;
            }
            set
            {
                _goods = value;
                referenceInfoList.PutProperty<Erp_GoodsImage, Erp_Goods>(a => a.Goods);
            }
        }

        //商品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Goods")]
        public string GoodsId
        {
            get
            {
                return _goodsId;
            }
            set
            {
                _goodsId = value;
                propInfoList.PutProperty<Erp_GoodsImage, string>(a => a.GoodsId);
            }
        }

        //图片路径
        [Required]
        [MaxLength(2000)]
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                propInfoList.PutProperty<Erp_GoodsImage, string>(a => a.FilePath);
            }
        }

        //图片名称
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
                propInfoList.PutProperty<Erp_GoodsImage, string>(a => a.Name);
            }
        }


        public void Create()
        {
            Erp_GoodsImageId = Guid.NewGuid().ToString();
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
