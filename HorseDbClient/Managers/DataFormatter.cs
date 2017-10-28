using HorseDbClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseDbClient.Managers
{
    public static class DataFormatter
    {
        public static BindingSource FormatData(List<Horse> horses)
        {
            BindingSource bindingSource = new BindingSource();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(CreateColumn("Nimi/Name", "name", Type.GetType("System.String")));
            dataTable.Columns.Add(CreateColumn("Synt. aika/Born", "dateofbirth", Type.GetType("System.String")));
            dataTable.Columns.Add(CreateColumn("Sukupuoli/Gender", "gender", Type.GetType("System.String")));
            dataTable.Columns.Add(CreateColumn("Rotu/Breed", "breed", Type.GetType("System.String")));

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            foreach (Horse horse in horses)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["name"] = horse.Name;
                dataRow["dateofbirth"] = horse.DateOfBirth.ToString();
                dataRow["gender"] = GetGender(horse.Gender);
                dataRow["breed"] = GetBreed(horse.Breed);
                dataTable.Rows.Add(dataRow);
            }

            bindingSource.DataSource = dataTable;

            return bindingSource;
        }

        private static DataColumn CreateColumn(string caption, string name, Type type)
        {
            DataColumn column = new DataColumn();
            column.Caption = caption;
            column.ColumnName = name;
            column.DataType = type;
            column.ReadOnly = true;
            return column;
        }

        private static string GetGender(Gender gender)
        {
            string res = "Ori/Stallion";
            switch(gender)
            {
                case Gender.MARE:
                    res = "Tamma/Mare";
                    break;
                case Gender.GELDING:
                    res = "Ruuna/Gelding";
                    break;
            }
            return res;
        }

        private static string GetBreed(Breed breed)
        {
            string res = "LV/Warmblood";
            if (breed == Breed.COLDBLOOD)
                res = "KV/Cold blood";
            return res;
        }
    }
}
