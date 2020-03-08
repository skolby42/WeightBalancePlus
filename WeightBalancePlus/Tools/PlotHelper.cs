using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightBalancePlus.Tools
{
    static class PlotHelper
    {
        const double Plot_Size_Multiplier = 0.025;

        public static double AddPlotMaxBuffer(double plotMax)
        {
            return plotMax + plotMax * Plot_Size_Multiplier;
        }

        public static double AddPlotMinBuffer(double plotMin)
        {
            return plotMin - plotMin * Plot_Size_Multiplier;
        }
    }
}
