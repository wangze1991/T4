using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using T4.Sample.Domain;
namespace T4.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {

           //IList<DbTable> tables=DbHelper.getInstance().SetDataBaseName("Auth").GetDataTables().Result;
           //foreach (DbTable table in tables)
           //{
           //    foreach (DbColumn column in table.DbColumns)
           //    {
           //        System.Console.WriteLine(column.ColumnName);
           //    }
           //}
           string[] array={"1","2","3"};
           array.ToList().Select(x => string.Format("\t\t\t\t{0}", x)).ToList().ForEach(x => { System.Console.WriteLine(x); });
           System.Console.ReadKey();
        }
    }
}
