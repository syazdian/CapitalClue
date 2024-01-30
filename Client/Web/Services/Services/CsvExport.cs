using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Web.Services.Services
{
    public class CsvExport : ICsvExport
    {
        //q91
        public Stream ToCsv<T>(List<T> list)
        {
            StringBuilder sw = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Append(properties[i].Name + ",");
            }
            string lastProp = properties[properties.Length - 1].Name;
            sw.Append($"{lastProp}\n");

            foreach (var item in list)
            {
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Append(prop.GetValue(item)?.ToString()?.Replace(",", " ") + ",");
                }
                PropertyInfo lastPropVal = properties[properties.Length - 1];
                sw.Append(lastPropVal.GetValue(item) + "\n");
            }

            string s = sw.ToString();

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void CreateHeader<T>(List<T> list, StringBuilder sw)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Append(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sw.Append($"{lastProp}\n");
        }

        private void CreateRows<T>(List<T> list, StringBuilder sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Append(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Append(lastProp.GetValue(item) + "\n");
            }
        }
    }
}