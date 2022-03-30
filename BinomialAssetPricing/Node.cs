using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinomialAssetPricing
{
    public class Node
    {
        // Private member-variables
        private double assetV;
        private double optionV;

        //getters setters
        public double AssetV
        {
            get { return assetV; }
            set { assetV = value; }
        }

        public double OptionV
        {
            get { return optionV; }
            set { optionV = value; }
        }

        //constructor
        public Node()
        {
            assetV = -1;
            optionV = -1;
        }

        public Node(double s, double o)
        {
            assetV = s;
            optionV = o;
        }



     }
}
