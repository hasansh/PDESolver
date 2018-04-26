using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.UTL
{
    public static class GlobalObjects
    {
        public static BUSL.Geometry Geometry;
        public static BUSL.Mesh Mesh;
        public static List<double> CenterErrors = new List<double>();
        public static List<double> StiffnessErrors = new List<double>();
        public static List<double> MassErrors = new List<double>();
        public static List<int> XNodesNumberValues = new List<int>();
        public static List<BUSL.Error> ErrorsList = new List<BUSL.Error>();
        public static List<BUSL.Error> XVelocityErrorsList = new List<BUSL.Error>();
        public static List<BUSL.Error> YVelocityErrorsList = new List<BUSL.Error>();
        public static double SteadyStateError=1.0;
    }
}
