using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4.Sample.Domain
{
    /// <summary>
    /// 数据库列
    /// </summary>
    public class DbColumn
    {
        /// <summary>
        /// 字段序号
        /// </summary>
        public int InnerSort { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimary { get; set; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }


        public DbColumn() {
        
        }

        public DbColumn(int order,string columnName,bool isUnique,bool isPrimarykey,string dbType,bool isNull,string defaultValue,string description) {
            this.ColumnName = columnName;
            this.InnerSort = order;
            this.IsUnique = isUnique;
            this.IsPrimary = isPrimarykey;
            this.DbType = dbType;
            this.IsNull = isNull;
            this.DefaultValue = defaultValue;
            this.Description = description;
        }

    }
}
