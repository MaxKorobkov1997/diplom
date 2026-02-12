using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace diplom.Database_management
{
    public static class otkritie_tb
    {
        public static DataTable Otk_jurnal()
        {
            DataTable dt = new DataTable();
            using (var context = new DBpodkl())
            {
                dt = ToDataTable(context.Jurnals.Select(e => new { e.Id, e.Name, e.Id_Neme,  e.Fakultet, e.Id_Fakultet, e.VidGr,e.Id_VidGr }).ToList());
            }
            return dt;
        }

        public static DataTable otk_student()
        {
            DataTable dt = new DataTable();
            using (var context = new DBpodkl())
            {
                dt = ToDataTable(context.Students.Select(e => new { e.Id, e.Name, e.Pasport, e.Document_Soc_Gr }).ToList());
            }
            return dt;
        }

        public static DataTable otk_faculteet()
        {
            DataTable dt = new DataTable();
            using (var context = new DBpodkl())
            {
                dt = ToDataTable(context.Fakultets.Select(e => new { e.Id, e.Fakultets }).ToList());
            }
            return dt;
        }

        public static DataTable otk_vidgr()
        {
            DataTable dt = new DataTable();
            using (var context = new DBpodkl())
            {
                dt = ToDataTable(context.Vids.Select(e => new { e.Id, e.vid }).ToList());
            }
            return dt;
        }
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            // Get all properties of the type T
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create the columns
            foreach (PropertyInfo prop in Props)
            {
                // Handle Nullable types
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ?
                             Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;
                dataTable.Columns.Add(prop.Name, type);
            }

            // Populate the rows
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
