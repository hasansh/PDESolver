using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public interface Geometry
    {
        double Area { get; }

        BUSL.Shape Shape { get; set; }

        void Draw(System.Drawing.Graphics graphic, double scale);


    }
}
