using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGenerator
    {

        public string CreateEntityCode(string entityName, List<EntityDefine> list)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using JKF.DB.extension;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Reflection;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();
            sb.AppendLine("namespace JKF.DB.Models");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {entityName} : BaseEntity");
            sb.AppendLine("    {");

         
            sb.AppendLine("        private List<PropertyInfo> propInfoList = null;");

            sb.AppendLine();
         
            sb.AppendLine("        private List<PropertyInfo> referenceInfoList = null;");

            sb.AppendLine();

            sb.AppendLine($"        public {entityName}()");
            sb.AppendLine("        {");
            sb.AppendLine("            propInfoList = DBUtils.GetPropertyInfoList();");
            sb.AppendLine("            referenceInfoList = DBUtils.GetPropertyInfoList();");
            sb.AppendLine("        }");

            sb.AppendLine();

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();


            var mainKey = "";
            foreach (var entityDefine in list)
            {
                
                var field = "_" +　entityDefine.PropertyName.Substring(0, 1).ToLower() 
                    + entityDefine.PropertyName.Substring(1);
                var propertyName = entityDefine.PropertyName;
                var key = entityDefine.IsPrimaryKey ? "[Key]" : "";
                if (mainKey == "")
                {
                    mainKey = entityDefine.IsPrimaryKey ? propertyName : "";
                }
                
                var isDefaultType = !string.IsNullOrWhiteSpace(entityDefine.DefaultType);
                var required = !entityDefine.IsNull && isDefaultType ? "[Required]" : "";
                var type = "";                if (!string.IsNullOrWhiteSpace(entityDefine.DefaultType))
                    type = entityDefine.DefaultType;
                else if (!string.IsNullOrWhiteSpace(entityDefine.CustomerType))
                    type = entityDefine.CustomerType;
                if (required == "" && type != "string" && isDefaultType)
                    type += "?";
                var foreignKey = entityDefine.ForeignKey;

                var annotaion = entityDefine.Annotation;
               
                var maxLength = "";
                if (entityDefine.Length != "")
                    maxLength = $"[MaxLength({entityDefine.Length})]";

                if (!string.IsNullOrWhiteSpace(annotaion))
                {
                    sb1.AppendLine($"        //{annotaion}");
                }
                sb1.AppendLine($"        private {type} {field};");
                sb1.AppendLine();

                if (!string.IsNullOrWhiteSpace(annotaion))
                {
                    sb2.AppendLine($"        //{annotaion}");
                }

                if (key != "")
                    sb2.AppendLine("        " + key);
                if (required != "" && key == "")
                    sb2.AppendLine("        " + required);
                if (maxLength != "")
                    sb2.AppendLine("        " + maxLength);

                if (!string.IsNullOrWhiteSpace(foreignKey))
                { 
                    sb2.AppendLine($"        [ForeignKey(\"{foreignKey}\")]");
                }
                
                if (isDefaultType)
                    sb2.AppendLine($"        public {type} {propertyName}");
                else
                    sb2.AppendLine($"        public virtual {type} {propertyName}");

                sb2.AppendLine("        {");
                sb2.AppendLine("            get");
                sb2.AppendLine("            {");
                sb2.AppendLine($"                return {field};");
                sb2.AppendLine("            }");
                sb2.AppendLine("            set");
                sb2.AppendLine("            {");
                sb2.AppendLine($"                {field} = value;");
                
                if (propertyName != mainKey)
                {
                    if (isDefaultType)
                        sb2.AppendLine($"                propInfoList.PutProperty<{entityName}, {type}>(a => a.{propertyName});");
                    else
                        sb2.AppendLine($"                referenceInfoList.PutProperty<{entityName}, {type}>(a => a.{propertyName});");
                }
               
                sb2.AppendLine("            }");
                sb2.AppendLine("        }");

                sb2.AppendLine();


            }

            sb.AppendLine(sb1.ToString());
            sb.AppendLine(sb2.ToString());

            sb.AppendLine("        public void Create()");
            sb.AppendLine("        {");
            sb.AppendLine($"            {mainKey} = Guid.NewGuid().ToString();");
            sb.AppendLine("        }");
            
            sb.AppendLine();
            
            sb.AppendLine("        public void ClearModifyInfo()");
            sb.AppendLine("        {");
            sb.AppendLine("            propInfoList.Clear();");
            sb.AppendLine("            referenceInfoList.Clear();");
            sb.AppendLine("        }");

            sb.AppendLine("        public List<PropertyInfo> GetPropInfoList()");
            sb.AppendLine("        {");
            sb.AppendLine("            return propInfoList;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public List<PropertyInfo> GetReferenceInfoList()");
            sb.AppendLine("        {");
            sb.AppendLine("            return referenceInfoList;");
            sb.AppendLine("        }");

            sb.AppendLine("    }");
            sb.AppendLine("}");


            return sb.ToString();
        }
        public string CreateDbSetCode(string entityName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using JKF.DB.Models;");
            sb.AppendLine("using Microsoft.EntityFrameworkCore;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");

            sb.AppendLine();
            
            sb.AppendLine("namespace JKF.DB.DbContexts");
            sb.AppendLine("{");

            sb.AppendLine("    public partial class BaseDbContext");
            sb.AppendLine("    {");
            sb.AppendLine($"        public DbSet<{entityName}> {entityName} {{ get; set; }}");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string CreateServiceCode(string entityName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using JKF.DB.DbContexts;");
            sb.AppendLine("using JKF.DB.Models;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");

            sb.AppendLine();

            sb.AppendLine("namespace JKF.DB.Operation");
            sb.AppendLine("{");

            sb.AppendLine($"    public class {entityName}Service : BaseService<{entityName}>");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {entityName}Service(BaseDbContext baseDbContext) : base(baseDbContext) {{ }}");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string CreateBLLCode(string entityName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using JKF.BLL.Base;");
            sb.AppendLine("using JKF.Commons;");
            sb.AppendLine("using JKF.DB.DbContexts;");
            sb.AppendLine("using JKF.DB.Models;");
            sb.AppendLine("using JKF.DB.Operation;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Linq.Expressions;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");

            sb.AppendLine();

            sb.AppendLine("namespace JKF.BLL");
            sb.AppendLine("{");

            sb.AppendLine($"    public class {entityName}BLL");
            sb.AppendLine("    {");
            sb.AppendLine("        private BaseDbContext _dbContext = null;");
            sb.AppendLine($"        private {entityName}Service _{entityName}Service = null;");
            sb.AppendLine($"        private BaseBLL _baseBLL = new BaseBLL();");
            sb.AppendLine();
            sb.AppendLine($"        public {entityName}BLL()");
            sb.AppendLine("        {");
            sb.AppendLine("            _dbContext = new BaseDbContext();");
            sb.AppendLine($"            _{entityName}Service = new {entityName}Service(_dbContext);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void AddEntity({entityName} entity)");
            sb.AppendLine("        {");
            sb.AppendLine("            entity.Create();");
            sb.AppendLine($"            _{entityName}Service.Add(entity);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void AddEntities(IEnumerable<{entityName}> entities)");
            sb.AppendLine("        {");
            sb.AppendLine("            foreach (var e in entities)");
            sb.AppendLine("            {");
            sb.AppendLine("                 e.Create();");
            sb.AppendLine("            }");
            sb.AppendLine($"            _{entityName}Service.AddRange(entities);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void UpdateEntity({entityName} entity)");
            sb.AppendLine("        {");
            sb.AppendLine($"            _{entityName}Service.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void UpdateEntities(IEnumerable<{entityName}> entities)");
            sb.AppendLine("        {");
            sb.AppendLine($"           foreach (var e in entities)");
            sb.AppendLine("            {");
            sb.AppendLine($"                _{entityName}Service.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());");
            sb.AppendLine("            }");            
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void RemoveEntity({entityName} entity)");
            sb.AppendLine("        {");
            sb.AppendLine($"            _{entityName}Service.Remove(entity);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public void RemoveEntities(IEnumerable<{entityName}> entities)");
            sb.AppendLine("        {");
            sb.AppendLine($"            _{entityName}Service.RemoveRange(entities);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public {entityName} GetEntity(string keyValue, bool beTraced = false)");
            sb.AppendLine("        {");
            sb.AppendLine($"            var entity = _{entityName}Service.FindList(a => a.{entityName}Id == keyValue,a => a.{entityName}Id, true, beTraced)[0];");
            sb.AppendLine("             return entity;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public IList<{entityName}> GetEntities(Expression<Func<{entityName}, bool>> whereExpression, bool isSortAsc = true)");
            sb.AppendLine("        {");
            sb.AppendLine($"            var data = _{entityName}Service.FindList(whereExpression, a => a.{entityName}Id, isSortAsc);");
            sb.AppendLine("             return data;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public IList<{entityName}> GetEntites(Pagination pagination, Sort sort, Expression<Func<{entityName}, bool>> whereExpression)");
            sb.AppendLine("        {");
            sb.AppendLine($"            return _baseBLL.GetObjects(_{entityName}Service, pagination, sort, whereExpression);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public bool IsExists(Expression<Func<{entityName}, bool>> whereExpression)");
            sb.AppendLine("        {");
            sb.AppendLine($"            return _{entityName}Service.IsExists(whereExpression);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

    }
}
