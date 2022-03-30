using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinomialAssetPricing
{
    class BinomialTree
    {
        private int steps;
        private double underlyingPrice;
        private double upSize;
        private double downSize;
        private Node[,] bTree;

        //getters and setters 
        public double DownSize
        {
            get { return downSize; }
            set { downSize = value; }
        }

        public double UpSize
        {
            get { return upSize; }
            set { upSize = value; }
        }

        public int Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public double UnderlyingPrice
        {
            get { return underlyingPrice; }
            set { underlyingPrice = value; }
        }

        //constructors
        public BinomialTree(int n, double s, double up, double down) 
        {
            this.steps = n+1;
            underlyingPrice = s;
            upSize = up;
            downSize = down;
            this.bTree = new Node[steps, steps];
        }

        public void BuildBt() 
        {
            double sharePrice = 0;
            Node baseNode = new Node(underlyingPrice, 0);
            //Node[,] bTree = new Node[steps, steps];
            bTree[0, 0] = baseNode;
            for(int i = 0; i< steps; i++)
            {
                for (int j = 0; (j <= i && j+i <steps); j++) 
                {
                    sharePrice = underlyingPrice * Math.Pow(upSize, i) * Math.Pow(downSize, j);
                    Node nodeU = new Node(sharePrice,-1);
                    bTree[i, j] = nodeU;
                    //bottom of teh tree
                    if (i != j){
                        sharePrice = underlyingPrice * Math.Pow(upSize, j) * Math.Pow(downSize, i);
                        Node nodeD = new Node(sharePrice, -1);
                        bTree[j, i] = nodeD;
                    }
                }
            }
        }

        public double GetOptionValue(double k, double p, double q, Boolean c)
        {
            
            int j;
            int i = 0;
            //set OptionValue at the last nodes of the tree
            if (c == true)
            {
                for (j = steps - 1; j >= 0; j--)
                {
                    bTree[j, i].OptionV = Math.Max(bTree[j, i].AssetV - k, 0);
                    // bTree[i, j].OptionV = Math.Max(bTree[i, j].AssetV - k, 0);
                    i++;
                }
            }

            else
            {
                for (j = steps - 1; j >= 0; j--)
                {
                    bTree[j, i].OptionV = Math.Max(k - bTree[j, i].AssetV, 0);
                    // bTree[i, j].OptionV = Math.Max(bTree[i, j].AssetV - k, 0);
                    i++;
                }
            }

            //set the intermediate nodes
            for (int l = steps - 2; l >= 0; l--)
            {
                i = 0;
                for (j = l; j >= 0; j--)
                {
                    double oV = p * bTree[j + 1, i].OptionV + q * bTree[j, i + 1].OptionV;
                    bTree[j, i].OptionV = oV;
                    i++;
                }
            }
            return bTree[0, 0].OptionV;

        }

        public void Display() 
        {
            for(int i = 0; i< steps; i++)
            {
                for (int j = 0; (j <= i && j + i < steps); j++) 
                {
                    System.Console.WriteLine("Asset Value at node i = {0} and j= {1} = {2}", i, j, bTree[i, j].AssetV.ToString());
                    System.Console.WriteLine("Option Value at node i = {0} and j= {1} = {2}", i, j, bTree[i, j].OptionV.ToString());

                    if (i != j)
                    {
                        System.Console.WriteLine("Asset Value at node i = {0} and j= {1} = {2}", j, i, bTree[j, i].AssetV.ToString());
                        System.Console.WriteLine("Option Value at node i = {0} and j= {1} = {2}", j, i, bTree[j, i].OptionV.ToString()); 
                    }
                }
            }
        }

    }
}
