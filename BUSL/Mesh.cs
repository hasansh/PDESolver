using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public class Mesh
    {
        private static Mesh _instance;

        private ElementType _elementType;

        public ElementType ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }


        private int _xNodesNumber;

        public int XNodesNumber
        {
            get { return _xNodesNumber; }
            set { _xNodesNumber = value; }
        }

        private int _yNodesNumber;

        public int YNodesNumber
        {
            get { return _yNodesNumber; }
            set { _yNodesNumber = value; }
        }

        private Geometry _geometry;

        public Geometry Geometry
        {
            get { return _geometry; }
            set { _geometry = value; }
        }

        public List<Point> MiddleNodesList;

        public List<Point> MainNodesList;

        private List<Point> _nodesList = new List<Point>();

        public List<Point> NodesList
        {
            get { return _nodesList; }
            set { _nodesList = value; }
        }

        private List<Element> _elementsList ;

        public List<Element> ElementsList
        {
            get { return _elementsList; }
            set { _elementsList = value; }
        }

        private List<Element> _boundryElements;

        public List<Element> BoundryElements
        {
            get { return _boundryElements; }
            set { _boundryElements = value; }
        }

        private List<Point> _boundryNodes;

        public List<Point> BoundryNodes
        {
            get { return _boundryNodes; }
            set { _boundryNodes = value; }
        }
       
        protected Mesh(int xNodesNumber, int yNodesNumber, ElementType elementType ,Geometry geometry)
        {
            this._xNodesNumber = xNodesNumber;
            this._yNodesNumber = yNodesNumber;
            this.ElementType = elementType;
            this._geometry = geometry;
            this._boundryElements = new List<Element>();
            this._boundryNodes = new List<Point>();
            this._nodesList = new List<Point>();
            this._elementsList = new List<Element>();
            this.MiddleNodesList = new List<Point>();
            this.MainNodesList = new List<Point>();
            

        }

        public static Mesh GetMesh(int xNodesNumber, int yNodesNumber, ElementType elementType, Geometry geometry)
        {           
            if (_instance == null)
            {
                _instance = new Mesh(xNodesNumber, yNodesNumber, elementType, geometry);
            } 
                  
            return _instance;
        }

        public void ReadFromFile(string fileName)
        {

        }

        public void WritToFile(string fileName)
        {

        }          

        public void ClearMesh()
        {
            this.ElementsList.Clear();
            this.BoundryNodes.Clear();
            this.BoundryElements.Clear();
            this.NodesList.Clear();
            this.MainNodesList.Clear();
            this.MiddleNodesList.Clear();
        }

        public void DescritizeSquareDomainToNodes()
        {
            MainNodesList.Clear();
            MiddleNodesList.Clear();
            NodesList.Clear();
            double xMainIncrement = (((Square)this.Geometry).Point2.X - ((Square)this.Geometry).Point1.X) / (this.XNodesNumber - 1);
            double yMainIncrement = (((Square)this.Geometry).Point4.Y - ((Square)this.Geometry).Point1.Y) / (this.YNodesNumber - 1);
            double xStartPoint = ((Square)this.Geometry).Point1.X;
            double ystartPoint = ((Square)this.Geometry).Point1.Y;
            for (int i = 0; i < 2 * this.YNodesNumber; i = i + 2)
            {
                for (int j = 1; j < 2 * this.XNodesNumber; j = j + 2)
                {
                    Point point = new Point(((j+1)/2)+(i/2)*XNodesNumber, j + (i * (2 * (this.XNodesNumber) - 1)), xStartPoint + ((j - 1) / 2) * xMainIncrement, ystartPoint);
                   
                    MainNodesList.Add(point);

                }
                ystartPoint = ystartPoint + yMainIncrement;
            }
            ystartPoint = ((Square)this.Geometry).Point1.Y;
            for (int i = 0; i < 2 * this.YNodesNumber - 1; i++)
            {
                for (int j = 1; j < 2 * this.XNodesNumber; j++)
                {
                    Point point = new Point(j + (i * ((2 * XNodesNumber) - 1)), xStartPoint + (j - 1) * xMainIncrement / 2, ystartPoint);
                    NodesList.Add(point);
                }
                ystartPoint = ystartPoint + yMainIncrement / 2;
            }
            foreach (var item in NodesList)
            {
                foreach (var mainItem in MainNodesList)
                {
                    if (item.Id == mainItem.Id)
                    {
                        item.MainId = mainItem.MainId;
                    }
                }
            }
            MiddleNodesList = NodesList.Where(p => !MiddleNodesList.Any(p2 => p2.Id == p.Id)).ToList();           
           
        }

        public void SetNodesToElements()
        {
            ElementsList.Clear();
            for (int i = 1; i < (2 * this.XNodesNumber - 1) * (2 * this.YNodesNumber - 1) + 1; i = i + 1)
            {
                ElementFactoryBase factory = new ConcereteElementFactory();
                TriangularElement element = factory.ElementFactoryMethod(this.ElementType);
                foreach (var item in MainNodesList)
                {
                    if (item.Id == i + (4 * this.XNodesNumber-2) || item.Id == i || item.Id == i + (4 * this.XNodesNumber))
                    {
                        if (element.MainNode1 == null)
                        {
                            element.MainNode1 = item;
                        }
                        else if (element.MainNode3 == null)
                        {
                            element.MainNode3 = item;
                        }
                        else if (element.MainNode2 == null)
                        {
                            element.MainNode2 = item;
                        }
                       
                    }
                }
                if (this.ElementType==ElementType.P2Triangle)
                {
                    foreach (var item in MiddleNodesList)
                    {
                        if (item.Id == i + (4 * this.XNodesNumber - 1) || item.Id == i + (2 * this.XNodesNumber-1) || item.Id == i + (2 * this.XNodesNumber))
                        {
                            if (((P2TriangularElement)element).MiddlePoint3 == null)
                            {
                                ((P2TriangularElement)element).MiddlePoint3 = item;
                            }
                            else if(((P2TriangularElement)element).MiddlePoint1 == null)
                            {
                                ((P2TriangularElement)element).MiddlePoint1 = item;
                            }                           
                            else if (((P2TriangularElement)element).MiddlePoint2 == null)
                            {
                                ((P2TriangularElement)element).MiddlePoint2 = item;
                            }

                        }
                    }
                }
                
                if (element.MainNode1 != null && element.MainNode2 != null && element.MainNode3 != null)
                {                                     
                    TriangularElement nextElement = factory.ElementFactoryMethod(this.ElementType);
                    nextElement.MainNode1 = element.MainNode1;
                    nextElement.MainNode2 = (from node in MainNodesList
                                             where node.Id == element.MainNode1.Id + 2
                                             select node).FirstOrDefault();
                    nextElement.MainNode3 = element.MainNode2;
                    if (this.ElementType == ElementType.P2Triangle && ((P2TriangularElement)element).MiddlePoint1 != null && ((P2TriangularElement)element).MiddlePoint2 != null && ((P2TriangularElement)element).MiddlePoint3 != null)
                    {
                        ((P2TriangularElement)nextElement).MiddlePoint1 = (from node in MiddleNodesList
                                                    where node.Id == ((P2TriangularElement)element).MainNode1.Id + 1
                                                    select node).FirstOrDefault();
                        ((P2TriangularElement)nextElement).MiddlePoint2 = (from node in MiddleNodesList
                                                    where node.Id == ((P2TriangularElement)element).MiddlePoint1.Id + 1
                                                    select node).FirstOrDefault();
                        ((P2TriangularElement)nextElement).MiddlePoint3 = ((P2TriangularElement)element).MiddlePoint1;
                    }
                    element.Id = ElementsList.Count + 1;
                    element.Refresh();
                    ElementsList.Add((Element)element);
                    nextElement.Id = ElementsList.Count + 1;
                    nextElement.Refresh();                    
                    ElementsList.Add(nextElement);

                }
            }
           
        }        
        public void SetBoundaryNodes()
        {
            BoundryNodes.Clear();
            foreach (P2TriangularElement element in ElementsList)
            {
                foreach (var node in element.PointsList)
                {
                    if ((Math.Abs(node.X - ((Square)this.Geometry).Point1.X) < UTL.Constants.Accuracy || (Math.Abs(node.X - ((Square)this.Geometry).Point2.X) < UTL.Constants.Accuracy)) && !BoundryNodes.Contains(node))
                    {
                        
                        BoundryNodes.Add(node);
                        if (!BoundryElements.Contains(element))
                        {
                            BoundryElements.Add(element);
                        }
                    }
                    else if ((Math.Abs(node.Y - ((Square)this.Geometry).Point1.Y) < UTL.Constants.Accuracy || (Math.Abs(node.Y - ((Square)this.Geometry).Point3.Y) < UTL.Constants.Accuracy)) && !BoundryNodes.Contains(node))
                    {
                                         
                        BoundryNodes.Add(node);
                    }
                    


                }
            }

            NodesList = NodesList.OrderBy(q => q.Id).ToList();
            BoundryNodes = BoundryNodes.OrderBy(q => q.Id).ToList();
            foreach (var item in BoundryNodes)
            {
                item.IsBoundaryNode = true;
            }
            foreach (var nocd in NodesList)
            {
                foreach (var item in BoundryNodes)
                {
                    if (nocd.Id == item.Id)
                    {
                        nocd.IsBoundaryNode = true;
                    }
                }
            }
        }
        public void SetBoundaryElements()
        {
            BoundryElements.Clear();
            foreach (P2TriangularElement element in ElementsList)
            {
                foreach (var node in element.PointsList)
                {
                    if ((node.X == ((Square)this.Geometry).Point1.X || node.X == ((Square)this.Geometry).Point2.X) && !BoundryNodes.Contains(node))
                    {
                        
                        if (!BoundryElements.Contains(element))
                        {
                            BoundryElements.Add(element);
                        }
                    }
                    else if ((Math.Abs(node.Y - ((Square)this.Geometry).Point1.Y) < 0.00001 || (Math.Abs(node.Y - ((Square)this.Geometry).Point3.Y) < 0.00001 && !BoundryNodes.Contains(node))))
                    {                        
                        if (!BoundryElements.Contains(element))
                        {
                            BoundryElements.Add(element);
                        }

                    }

                }
            }

            NodesList = NodesList.OrderBy(q => q.Id).ToList();
            BoundryNodes = BoundryNodes.OrderBy(q => q.Id).ToList();
        }

        //private List<Element> DescritizeSquareDomain(int xNodeNumber, int yNodeNumber, ElementType elementType, Square geometry)
        //{
            
        //    List<Element> elements = new List<Element>();
            
        //    List<Point> Mainnodes = new List<Point>();
        //    List<Point> MiddleNodes = new List<Point>();
        //    List<Point> AllNodes = new List<Point>();
        //    double xMainIncrement = (geometry.Point2.X - geometry.Point1.X) / (xNodeNumber - 1);
        //    double yMainIncrement = (geometry.Point4.Y - geometry.Point1.Y) / (yNodeNumber - 1);
        //    double xStartPoint = geometry.Point1.X;
        //    double ystartPoint = geometry.Point1.Y;
        //    for (int i = 0; i < 2 * yNodeNumber; i = i + 2)
        //    {
        //        for (int j = 1; j < 2 * xNodeNumber; j = j + 2)
        //        {
        //            Point point = new Point(j + (i * (2 * (xNodeNumber) - 1)), xStartPoint + ((j - 1) / 2) * xMainIncrement, ystartPoint);
        //            Mainnodes.Add(point);
        //        }
        //        ystartPoint = ystartPoint + yMainIncrement;
        //    }
        //    ystartPoint = geometry.Point1.Y;
        //    for (int i = 0; i < 2 * yNodeNumber - 1; i++)
        //    {
        //        for (int j = 1; j < 2 * xNodeNumber; j++)
        //        {
        //            Point point = new Point(j + (i * ((2 * xNodeNumber) - 1)), xStartPoint + (j - 1) * xMainIncrement / 2, ystartPoint);
        //            AllNodes.Add(point);
        //        }
        //        ystartPoint = ystartPoint + yMainIncrement / 2;               
        //    }

        //    MiddleNodes = AllNodes.Where(p => !Mainnodes.Any(p2 => p2.Id == p.Id)).ToList();
        //    //MiddleNodes = AllNodes.Except(Mainnodes).ToList();

        //    for (int i = 1; i < (2 * xNodeNumber - 1) * (2 * yNodeNumber - 1) + 1; i = i + 1)
        //    {
        //        P2TriangularElement p2Element = new P2TriangularElement();
        //        foreach (var item in Mainnodes)
        //        {
        //            if (item.Id == i + 2 || item.Id == i || item.Id == i + (4 * xNodeNumber - 2))
        //            {
        //                if (p2Element.MainNode1 == null)
        //                {
        //                    p2Element.MainNode1 = item;
        //                }
        //                else if (p2Element.MainNode2 == null)
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
        //            if (item.Id == i + 1 || item.Id == i + (2 * xNodeNumber) || item.Id == i + (2 * xNodeNumber - 1))
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
        //        if (p2Element.MainNode1 != null && p2Element.MainNode2 != null && p2Element.MainNode3 != null && p2Element.MiddlePoint1 != null && p2Element.MiddlePoint2 != null && p2Element.MiddlePoint3 != null)
        //        {
        //            p2Element.Id = elements.Count + 1;
        //            p2Element.Refresh();
        //            elements.Add((Element)p2Element);
        //            P2TriangularElement nextElement = new P2TriangularElement();
        //            nextElement.MainNode1 = p2Element.MainNode2;
        //            nextElement.MainNode2 = (from node in Mainnodes
        //                                     where node.Id == p2Element.MainNode2.Id + (4 * xNodeNumber - 2)
        //                                     select node).FirstOrDefault();
        //            nextElement.MainNode3 = p2Element.MainNode3;
        //            nextElement.MiddlePoint1 = (from node in MiddleNodes
        //                                        where node.Id == p2Element.MiddlePoint2.Id + 1
        //                                        select node).FirstOrDefault();
        //            nextElement.MiddlePoint2 = (from node in MiddleNodes
        //                                        where node.Id == p2Element.MiddlePoint2.Id + 2 * xNodeNumber - 1
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
