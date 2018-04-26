using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public abstract class ElementFactoryBase
    {
        public abstract TriangularElement ElementFactoryMethod(ElementType elementType);
    }
}
