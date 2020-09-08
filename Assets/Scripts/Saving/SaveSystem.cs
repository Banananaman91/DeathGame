using System.IO;
using UnityEngine;

namespace Saving
{
    public static class SaveSystem
    {
        private static readonly string SaveFolder = Application.dataPath + "/Saves";

        public static void Initialize()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }

        public static void Save(string saveString)
        {
            File.WriteAllText(SaveFolder + "/playerinf.json", saveString);
        }

        public static void SaveInventory(string saveString)
        {
            File.WriteAllText(SaveFolder + "/inventory.json", saveString);
        }

        public static string Load()
        {
            if (File.Exists(SaveFolder + "/playerinf.json"))
            {
                var data = File.ReadAllText(SaveFolder + "/playerinf.json");
                return data;
            }
            return null;
        }

        public static string LoadInventory()
        {
            if (File.Exists(SaveFolder + "/inventory.json"))
            {
                var data = File.ReadAllText(SaveFolder + "/inventory.json");
                return data;
            }
            return null;
        }

        public static void Delete()
        {
            if (File.Exists(SaveFolder + "/playerinf.json"))
            {
                File.Delete(SaveFolder + "/playerinf.json");
            }
            if (File.Exists(SaveFolder + "/inventory.json"))
            {
                File.Delete(SaveFolder + "/inventory.json");
            }
        }
    }
}
