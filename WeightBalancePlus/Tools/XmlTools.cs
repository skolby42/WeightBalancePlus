using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Tools
{
    public static class XmlTools
    {
        public static void SaveToXml<T>(T file, string path)
        {
            try
            {
                CreateDirectory(path);

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, file);
                }
            }
            catch (Exception ex)
            {
                var error = $"{Resources.XmlWriteError}: {ex.Message}";
                MessageBox.Show(error, Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static T LoadFromXml<T>(string path)
        {
            try
            {
                CreateDirectory(path);

                if (!File.Exists(path))
                    return default;

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream fs = File.OpenRead(path))
                {
                    return (T)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                var error = $"{Resources.XmlReadError}: {ex.Message}";
                MessageBox.Show(error, Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
        }

        private static void CreateDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
        }
    }
}
