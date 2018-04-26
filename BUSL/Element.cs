using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public abstract class Element
    {
        public double Area { get; }
        public abstract void Draw(System.Drawing.Graphics graphic, double scale);              
    }
}
