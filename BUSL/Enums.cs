using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public enum ElementType
    {
        P1Triangle,
        P2Triangle,
    }
   public enum Shape
    {
        Square,
        Rectangle,
    }
    public enum PointValue
    {
        Temperature,
        Pressure,
        XVelocity,
        YVelocity,
    }
}
