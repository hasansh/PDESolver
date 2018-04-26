using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FEMProject.BUSL
{
    class ConcereteElementFactory : ElementFactoryBase
    {
        public override TriangularElement ElementFactoryMethod(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.P1Triangle:
                    return new P1TriangularElement();                    
                case ElementType.P2Triangle:
                    return new P2TriangularElement();                   
                default:
                    throw new ArgumentException("Invalid type.", "type");
            }
        }
    }
}
