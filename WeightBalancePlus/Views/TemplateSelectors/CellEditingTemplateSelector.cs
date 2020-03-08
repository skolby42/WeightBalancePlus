using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeightBalancePlus.Models;

namespace WeightBalancePlus.Views
{
    public class CellEditingTemplateSelector: DataTemplateSelector
    {
        public DataTemplate BaggageCellEditingTemplate { get; set; }
        public DataTemplate PersonCellEditingTemplate { get; set; }
        public DataTemplate LoadItemCellEditingTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case Baggage _:
                    return BaggageCellEditingTemplate;
                case Person _:
                    return PersonCellEditingTemplate;
                case LoadItem _:
                    return LoadItemCellEditingTemplate;
                default:
                    return base.SelectTemplate(item, container);
            }
        }
    }
}
