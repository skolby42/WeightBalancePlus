using OxyPlot;
using System;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using WeightBalancePlus.Tools;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Settings : ICloneable
    {
        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "Not serialized")]
        public Color NormalColor { get; set; } = Colors.Green;
        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "Not serialized")]
        public Color UtilityColor { get; set; } = Colors.Orange;


        [XmlElement("NormalColor")]
        public string NormalColorAsArgb
        {
            get 
            { 
                return NormalColor.ToString(); 
            }
            set 
            { 
                NormalColor = (Color)ColorConverter.ConvertFromString(value); 
            }
        }

        [XmlElement("UtilityColor")]
        public string UtilityColorAsArgb
        {
            get
            {
                return UtilityColor.ToString();
            }
            set
            {
                UtilityColor = (Color)ColorConverter.ConvertFromString(value);
            }
        }

        public LineStyle NormalLineStyle { get; set; } = LineStyle.Solid;

        public LineStyle UtilityLineStyle { get; set; } = LineStyle.Dot;

        public bool ShowLegend { get; set; } = true;

        public bool ShowAnnotationDetail { get; set; } = false;


        public object Clone()
        {
            return new Settings()
            {
                NormalColor = NormalColor,
                UtilityColor = UtilityColor,
                NormalLineStyle = NormalLineStyle,
                UtilityLineStyle = UtilityLineStyle,
                ShowLegend = ShowLegend,
                ShowAnnotationDetail = ShowAnnotationDetail
            };
        }

        public void SaveToXml()
        {
            string xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.AppName, "Settings.xml");
            XmlTools.SaveToXml(this, xmlPath);
        }
    }
}
