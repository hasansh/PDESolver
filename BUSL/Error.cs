using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public class Error
    {
        private double _lInfinityError;

        public double LInfinityError
        {
            get { return _lInfinityError; }
            set { _lInfinityError = value; }
        }

        private double _l2Error;

        public double L2Error
        {
            get { return _l2Error; }
            set { _l2Error = value; }
        }

        private double _h1Error;

        public double H1Error
        {
            get { return _h1Error; }
            set { _h1Error = value; }
        }

        private int _nodesNumber;

        public int NodesNumber
        {
            get { return _nodesNumber; }
            set { _nodesNumber = value; }
        }

        private double _dT;

        public double DT
        {
            get { return _dT; }
            set { _dT = value; }
        }



        public Error(double lInfinityError, double l2Error, double h1Error,int nodesNumber,double dt)
        {
            this._lInfinityError = lInfinityError;
            this._l2Error = l2Error;
            this._h1Error = h1Error;
            this._nodesNumber = nodesNumber;
            this._dT = dt;
        }

    }
}
