using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public abstract class MeshBuilder
    {
        
        public abstract void BuildNodes();

        public abstract void BuildElements();
        public abstract void BuildBoundaryNodes();

        public abstract void BuildBoundaryElements();

        public abstract Mesh GetResults();

        public abstract void WriteToFile();

        

    }
}
