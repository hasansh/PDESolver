using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    class SpatialDescritization
    {
        public int NodeNumber { get; set; }
        public BUSL.ElementType ElementType { get; set; }

        //public List<Element> Descritize(int nodeNumber, ElementType elementType, Geometry geometry)
        //{
        //    List<Element> elements = new List<Element>();
        //    elements = this.DescritizeSquareDomain(nodeNumber, nodeNumber, ElementType.P2Triangle, (Square)geometry);            
        //    return elements;

        //}

        //private List<Element> DescritizeSquareDomain(int xNodeNumber, int yNodeNumber, ElementType elementType, Square geometry)
        //{
        //    List<Element> elements = new List<Element>();
        //    //   29-30-31-32-33-34-35
        //    //    |\ 8  |\10  |\12  |
        //    //    | \   | \   | \   |
        //    //   22 23 24 25 26 27 28
        //    //    |   \ |   \ |   \ |
        //    //    |  7 \|  9 \| 11 \|
        //    //   15-16-17-18-19-20-21
        //    //    |\ 2  |\ 4  |\ 6  |
        //    //    | \   | \   | \   |
        //    //    8  9 10 11 12 13 14
        //    //    |   \ |   \ |   \ |
        //    //    |  1 \|  3 \|  5 \|
        //    //    1--2--3--4--5--6--7
        //    List<Point> Mainnodes = new List<Point>();
        //    List<Point> MiddleNodes = new List<Point>();
        //    List<Point> AllNodes = new List<Point>();
        //    double xMainIncrement = (geometry.Point2.X - geometry.Point1.X) / (xNodeNumber-1);
        //    double yMainIncrement = (geometry.Point4.Y - geometry.Point1.Y) / (yNodeNumber-1);
        //    double xStartPoint = geometry.Point1.X;
        //    double ystartPoint = geometry.Point1.Y;                           
        //    for (int i = 0; i < 2*yNodeNumber; i= i+2)
        //    {                
        //        for (int j = 1; j < 2*xNodeNumber; j = j + 2)
        //        {                    
        //            Point point = new Point(j+(i*(2*(xNodeNumber)-1)), xStartPoint + ((j-1)/2)*xMainIncrement, ystartPoint);
        //            Mainnodes.Add(point);                                    
        //        }               
        //        ystartPoint = ystartPoint + yMainIncrement;
        //    }
        //    ystartPoint = geometry.Point1.Y;
        //    for (int i = 0; i < 2 * yNodeNumber-1; i++)
        //    {
        //        for (int j = 1; j < 2 * xNodeNumber; j++)
        //        {
        //            Point point = new Point(j+(i*((2*xNodeNumber) - 1)), xStartPoint+ (j-1)*xMainIncrement/2, ystartPoint);
        //            AllNodes.Add(point);
        //        }
        //        ystartPoint = ystartPoint + yMainIncrement/2;
        //    }

        //    MiddleNodes = AllNodes.Where(p => !Mainnodes.Any(p2 => p2.Id == p.Id)).ToList();
        //    //MiddleNodes = AllNodes.Except(Mainnodes).ToList();

        //    for (int i = 1; i < (2*xNodeNumber-1)*(2*yNodeNumber-1)+1; i=i+1)
        //    {
        //        P2TriangularElement p2Element = new P2TriangularElement();
        //        foreach (var item in Mainnodes)
        //        {
        //            if (item.Id == i + 2 || item.Id == i || item.Id== i+(4*xNodeNumber-2))
        //            {
        //                if (p2Element.MainNode1 ==null)
        //                {
        //                    p2Element.MainNode1 = item;
        //                }
        //                else if (p2Element.MainNode2 ==null)
        //                {
        //                    p2Element.MainNode2 = item;
        //                }
        //                else if (p2Element.MiddlePoint3 == null)
        //                {
        //                    p2Element.MainNode3 = item;
        //                }
        //            }                   
        //        }
               
        //        foreach (var item in MiddleNodes)
        //        {
        //            if (item.Id == i + 1 || item.Id ==  i + (2 * xNodeNumber) || item.Id == i + (2 * xNodeNumber - 1))
        //            {
        //                if (p2Element.MiddlePoint1 == null)
        //                {
        //                    p2Element.MiddlePoint1 = item;
        //                }
        //                else if (p2Element.MiddlePoint3 == null)
        //                {
        //                    p2Element.MiddlePoint3 = item;
        //                }

        //                else if (p2Element.MiddlePoint2 == null)
        //                {
        //                    p2Element.MiddlePoint2 = item;
        //                }
                       
        //            }
        //        }
        //        if (p2Element.MainNode1!=null && p2Element.MainNode2 != null && p2Element.MainNode3 != null && p2Element.MiddlePoint1 != null && p2Element.MiddlePoint2 != null && p2Element.MiddlePoint3 != null)
        //        {
        //            p2Element.Id = elements.Count+1;
        //            p2Element.Refresh();
        //            elements.Add((Element)p2Element);
        //            P2TriangularElement nextElement = new P2TriangularElement();
        //            nextElement.MainNode1 = p2Element.MainNode2;
        //            nextElement.MainNode2 = (from node in Mainnodes
        //                         where node.Id == p2Element.MainNode2.Id + (4*xNodeNumber-2)
        //                         select node).FirstOrDefault();
        //            nextElement.MainNode3 = p2Element.MainNode3;
        //            nextElement.MiddlePoint1 = (from node in MiddleNodes
        //                                        where node.Id == p2Element.MiddlePoint2.Id + 1
        //                                        select node).FirstOrDefault();
        //            nextElement.MiddlePoint2 = (from node in MiddleNodes
        //                                        where node.Id == p2Element.MiddlePoint2.Id + 2*xNodeNumber-1
        //                                        select node).FirstOrDefault();
        //            nextElement.MiddlePoint3 = p2Element.MiddlePoint2;
        //            nextElement.Id = elements.Count + 1;
        //            nextElement.Refresh();
        //            elements.Add(nextElement);

        //        }                
        //    }


        //    return elements;                        
        //}
    }
}
