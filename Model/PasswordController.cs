using System;
using System.Text.Json.Nodes;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace passwrkk.Model
{
    public class PasswordController
    {
        public static string AddNewObjectToVaultByName(List<string> objcts, string name, string jsonFile, List<string> types)
        {
            try
            {
                JObject jsonObject = JObject.Parse(jsonFile);
                JArray passArray = (JArray)jsonObject[name] ?? new JArray();

                JObject obj = new();
                for(int i = 0; i < objcts.Count; i++)
                {
                    obj[types[i]] = objcts[i];
                }
                passArray.Add(obj);

                if (jsonObject[name] == null)
                {
                    jsonObject[name] = passArray;
                }
                return jsonObject.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return "ERROR";
        }

        public static List<List<string>> GetAllByTypes(string json, string name, List<string> types)
        {
            var jsonObject = JObject.Parse(json);
            JArray array = (JArray?)jsonObject[name];

            List<List<string>> list = new();
            for(int j = 0; j < array.Count; j++)
            {
                List<string> ls = new();
                for(int i = 0; i < types.Count; i++)
                {
                    ls.Add(array[j][types[i]].ToString());

                }
                list.Add(ls);
            }
            return list;
        }
    }
}
