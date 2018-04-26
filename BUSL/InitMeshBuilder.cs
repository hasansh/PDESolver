﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    class InitMeshBuilder: MeshBuilder
    {
        private readonly Mesh Mesh;        

        public override void BuildNodes()
        {
            Mesh.DescritizeSquareDomainToNodes();
        }

        public override void BuildElements()
        {
            Mesh.SetNodesToElements();
        }

        public override void BuildBoundaryNodes()
        {
            Mesh.SetBoundaryNodes();
        }

        public override void BuildBoundaryElements()
        {
            Mesh.SetBoundaryElements();
        }

        public override Mesh GetResults()
        {
            return this.Mesh;
        }

        public override void WriteToFile()
        {
            throw new NotImplementedException();
        }

        public InitMeshBuilder()
        {
            Mesh = Mesh.GetMesh(UTL.Constants.XNodesNumber, UTL.Constants.YNodesNumber, UTL.Constants.ElementsType ,UTL.GlobalObjects.Geometry);
        }
    }
}
