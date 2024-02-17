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
    public class Authorize : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Authorize()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _authorizeId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string AuthorizeId
        {
            get
            {
                return _authorizeId;
            }
            set
            {
                _authorizeId = value;
            }
        }

        private int _objectType;

        [Required]
        public int ObjectType
        {
            get
            {
                return _objectType;
            }
            set
            {
                _objectType = value;
                propInfoList.PutProperty<Authorize, int>(a => a.ObjectType);
            }
        }

        private string _objectId;

        [Required]
        [MaxLength(50)]
        public string ObjectId
        {
            get
            {
                return _objectId;
            }
            set
            {
                _objectId = value;
                propInfoList.PutProperty<Authorize, string>(a => a.ObjectId);
            }
        }

        private int _itemType;

        [Required]
        public int ItemType
        {
            get
            {
                return _itemType;
            }
            set
            {
                _itemType = value;
                propInfoList.PutProperty<Authorize, int>(a => a.ItemType);
            }
        }

        private string _itemId;

        [Required]
        [MaxLength(50)]
        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                propInfoList.PutProperty<Authorize, string>(a => a.ItemId);
            }
        }

        public void Create()
        {
            AuthorizeId = Guid.NewGuid().ToString();
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
