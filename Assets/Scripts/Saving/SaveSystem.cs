using System.IO;
using System.Linq;
using UnityEngine;

namespace Saving
{
    public static class SaveSystem
    {
        private static readonly string SaveFolder = Application.dataPath + "/Saves";
        private static readonly string PlayerInfo = SaveFolder + "/playerinf.json";
        private static readonly string InventoryInfo = SaveFolder + "/inventory.json";

        public static void Initialize()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }

        public static void Save(string playerString, string inventoryString)
        {
            File.WriteAllText(PlayerInfo, playerString);
            File.WriteAllText(InventoryInfo, inventoryString);
        }
   
        public static (string , string) Load()
        {
            string data1 = null;
            string data2 = null;
            if (File.Exists(PlayerInfo))
            {
                data1 = File.ReadAllText(PlayerInfo);
            }
            if (File.Exists(InventoryInfo))
            {
                data2 = File.ReadAllText(InventoryInfo);
            }
            return (data1, data2);
        }

        public static void Delete()
        {
            if (!Directory.EnumerateFileSystemEntries(SaveFolder).Any()) return;
            var filesPaths = Directory.GetFiles(SaveFolder);
            {
                foreach (var file in filesPaths)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
