using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public  class MeshDirector
    {
       

        public MeshDirector()
        {
            
        }

        public void Construct(MeshBuilder builder)
        {
            
            builder.BuildNodes();
            builder.BuildElements();
            builder.BuildBoundaryNodes();
            builder.BuildBoundaryElements();
            
        }
    }
}
