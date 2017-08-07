using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.Data
{
   public class SettingsRepository
    {
        public static List<DTO.GlobalSetting> GetGlobalSettings()
        {
            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();
            var settings =  db.tbl_GlobalSettings.ToList();
            var DTOGlobalSettings = new List<DTO.GlobalSetting>();


            foreach (var setting in settings)
            {
                var DTOSetting = new DTO.GlobalSetting();

                DTOSetting.ID = setting.ID;
                DTOSetting.SystemName = setting.SystemName;
                DTOSetting.Description = setting.Description;
                DTOSetting.Value = setting.Value;

                DTOGlobalSettings.Add(DTOSetting);

            }
            return DTOGlobalSettings;

        }

        public static string GetGlobalSettingValue(string globalSettingName)
        {
            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();
            var settings = db.tbl_GlobalSettings.ToList();

            string value = settings.Find(item => item.SystemName == globalSettingName).Value;
            return value;

        }



        public static void UpdateGlobalSetting(int id, string systemName, string description, string value)
        {
            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();

            var query = from setting in db.tbl_GlobalSettings
                        where setting.ID == id
                        select setting;

            foreach (GlobalSetting setting in query)
            {
                setting.SystemName = systemName;
                setting.Description = description;
                setting.Value = value;

            }
            db.SaveChanges();
        }




    }




}
