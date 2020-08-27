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
            File.WriteAllText(SaveFolder + "/playerinf.txt", saveString);
        }

        public static string Load()
        {
            if (File.Exists(SaveFolder + "/playerinf.txt"))
            {
                var data = File.ReadAllText(SaveFolder + "/playerinf.txt");
                return data;
            }
            return null;
        }

        public static void Delete()
        {
            if (File.Exists(SaveFolder + "/playerinf.txt"))
            {
                File.Delete(SaveFolder + "/playerinf.txt");
            }
        }
    }
}
