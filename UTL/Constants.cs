using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.UTL
{
    public static class  Constants
    {
        public static float ScalingFactor = 10;
        public static int XNodesNumber = 11;
        public static int YNodesNumber = 11;
        public static BUSL.ElementType ElementsType = BUSL.ElementType.P2Triangle;
        public static double Accuracy = 0.000000000000001;
        public static double DT = 0.01;        
        public static double EndT = 1.0;
        public static int WritDT = 100;
        public static double Time = 0.0;
        public static double Reynolds = 1000;
        public static double SteadyStateCondition = 0.00001;
        public static string SoftwareVersion = "Finite Element Solver Version 1.0.0";
    }
}
