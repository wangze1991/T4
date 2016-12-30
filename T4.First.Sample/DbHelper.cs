using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using T4.Sample.Domain;
namespace T4.First.Sample
{
    public class DbHelper
    {
        private static DbHelper _helper;

        private string _dataBaseName = "";

        private string _connStr = "Data Source=.;Initial Catalog={0};User ID=sa;Password=123!@#qwe;";

        private const string SQL_SELECT_COLUMN = @"SELECT d.name as 'TableName',  
                                                    a.colorder  AS 'InnerOrder' ,
                                                    a.name  As  'ColumnName',
                                                    (case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 1  else 0 end) as 'IsUnique', 
                                                    (case when (SELECT count(*) FROM sysobjects  
                                                    WHERE (name in (SELECT name FROM sysindexes  
                                                    WHERE (id = a.id) AND (indid in  
                                                    (SELECT indid FROM sysindexkeys  
                                                    WHERE (id = a.id) AND (colid in  
                                                    (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  
                                                    AND (xtype = 'PK'))>0 then 1 else 0 end) 'IsPrimary',
                                                    b.name as DbType,
                                                    COLUMNPROPERTY(a.id,a.name,'PRECISION') as Length,  
                                                    isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as 小数位数,
                                                    (case when a.isnullable=1 then 1 else 0 end) as 'IsNull',  
                                                    isnull(e.text,'') as 'DefaultValue',
                                                    isnull(g.[value], ' ') AS  'Description'
                                                    FROM  syscolumns a 
                                                    left join systypes b on a.xtype=b.xusertype  
                                                    inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
                                                    left join syscomments e on a.cdefault=e.id  
                                                    left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id
                                                    left join sys.extended_properties f on d.id=f.class and f.minor_id=0
                                                    where b.name is not null";
        private string _tableSelectStr = @"SELECT
                                            obj.name tablename,
                                            schem.name schemname,
                                            idx.rows,
                                            CAST
                                            (
                                                CASE 
                                                    WHEN (SELECT COUNT(1) FROM sys.indexes WHERE object_id= obj.OBJECT_ID AND is_primary_key=1) >=1 THEN 1
                                                    ELSE 0
                                                END 
                                            AS BIT) HasPrimaryKey                                         
                                            from Auth.sys.objects obj 
                                            inner join {0}.dbo.sysindexes idx on obj.object_id=idx.id and idx.indid<=1
                                            INNER JOIN {0}.sys.schemas schem ON obj.schema_id=schem.schema_id
                                            where type='U' 
                                            order by obj.name";

        /// <summary>
        /// 静态初始化
        /// </summary>
        static DbHelper()
        {
            _helper = new DbHelper();
        }
        public string Connstr { get { return string.Format(_connStr, _dataBaseName); } }

        private DbHelper()
        {

        }

        public DbHelper SetDataBaseName(string dataBaseName)
        {
            this._dataBaseName = dataBaseName;
            return this;
        }

        /// <summary>
        /// 获取查询表名的sql语句
        /// </summary>
        /// <returns></returns>
        private string GetSqlTableSelectStr()
        {

            return string.Format(_tableSelectStr, _dataBaseName);
        }


        public static DbHelper getInstance()
        {
            return _helper;
        }
        public IEnumerable<DbTable> GetTables()
        {

            return null;
        }


        /// <summary>
        /// 获取一个table的所有的column
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<DbColumn> GetColumns(DbTable table)
        {
            return null;
        }

        public async Task<IList<DbTable>> GetDataTables()
        {
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                conn.Open();//conn要打开，关闭
                IList<DbTable> dbTables = getDbTableName(conn);
                SqlCommand cmd = new SqlCommand(SQL_SELECT_COLUMN, conn);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string tableName = reader["TableName"].ToString();
                        int order = int.Parse(reader["InnerOrder"].ToString());
                        string columnName = reader["ColumnName"].ToString();
                        bool isUnique =convertToBool(reader["IsUnique"]);
                        bool isPrimary = convertToBool(reader["IsPrimary"]);
                        string dbType = reader["DbType"].ToString();
                        bool isNull = convertToBool(reader["IsNull"]);
                        string defaultValue = reader["DefaultValue"].ToString();
                        string description = reader["description"].ToString();
                        DbColumn dc = new DbColumn(order, columnName, isUnique, isPrimary, dbType, isNull, defaultValue, description);
                        int index=dbTables.Where(x => x.TableName.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)).Select((source,number)=>number).Single();
                        foreach (DbTable table in dbTables)
                        {
                            if (table.TableName.Equals(tableName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                table.DbColumns.Add(dc);
                                break;
                            }
                            
                        }
                        //dbTables[index].DbColumns.Add(dc);                        
                    }
                    return dbTables;
                    //reader要关闭
                }

            }
        }

        public IList<DbTable> getDbTableName(SqlConnection con)
        {
            IList<DbTable> dbTables = new List<DbTable>();
            SqlCommand cmd = new SqlCommand(GetSqlTableSelectStr(), con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DbTable dbTable = new DbTable(row["schemname"].ToString(), row["tablename"].ToString());
                    dbTables.Add(dbTable);
                }
                return dbTables;
            }
            throw new Exception("该数据库没有表");

        }


        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool convertToBool(object value)
        {
            if (value == DBNull.Value) return false;
            if (value==null||string.IsNullOrWhiteSpace(value.ToString())) return false;
            if (value.ToString() == "1" || value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
            if (value.ToString() == "0" || value.ToString().Equals("false", StringComparison.CurrentCultureIgnoreCase)) return false;
            bool result = false;
            if (bool.TryParse(value.ToString(), out result)) return result;
            return result;
           
        }
    }
}
