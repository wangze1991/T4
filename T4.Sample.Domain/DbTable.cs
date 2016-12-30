using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4.Sample.Domain
{
    /// <summary>
    /// 表
    /// </summary>
    public class DbTable
    {

        /// <summary>
        /// 架构
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }



        /// <summary>
        /// rows
        /// </summary>

        public IList<DbColumn> DbColumns { get; set; }


        public DbTable()
        {
            this.DbColumns = new List<DbColumn>();
        }

        public DbTable(string schemaName, string tableName)
            : this()
        {
            this.SchemaName = schemaName;
            this.TableName = tableName;
        }

        /// <summary>
        /// 返回所有列，用逗号分隔，换行
        /// </summary>
        /// <returns></returns>
        public string ToColumnListStr() {
            return string.Join(",\r\n",this.DbColumns.Select(x => string.Format("\t\t\t\t[{0}]", x.ColumnName)).ToArray());
        }
    }
}
