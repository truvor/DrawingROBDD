using System;
using System.Collections.Generic;
using System.Collections;

namespace ROBDD
{
    class RobddGraph
    {
        private TDictionary tDict;
        private HDictionary hDict;

        public RobddGraph()
        {
            tDict = new TDictionary();
            hDict = new HDictionary();
        }

        public TDictionary GettDict()
        {
            return tDict;
        }

        public void SettMap(TDictionary tDict)
        {
            this.tDict = tDict;
        }

        public HDictionary GethDict()
        {
            return hDict;
        }

        public void SethDict(HDictionary hDict)
        {
            this.hDict = hDict;
        }

        public int MakeNode(int i, int l, int h)
        {
            IndexLowHigh ilh = new IndexLowHigh(i, l, h);
            return MakeNode(ilh);
        }

        public int MakeNode(IndexLowHigh ilh)
        {
            if (ilh.GetLow().Equals(ilh.GetHigh()))
            {
                return ilh.GetLow();
            }
            else if (hDict.Member(ilh))
            {
                return hDict.Lookup(ilh);
            }
            else
            {
                int node = tDict.Add(ilh);
                hDict.Insert(ilh, node);
                return node;
            }
        }

        public int Build(BooleanExpression expr)
        {
            return BuildRecursively(expr, expr.GetVariablesCount(), 1);
        }

        public int BuildRecursively(BooleanExpression expr, int n, int i)
        {
            if (i > n)
            {
                return expr.Evaluate() ? 1 : 0;
            }
            else
            {
                //xi = 0
                BooleanExpression expr1 = expr.SetVariableValue(expr.GetVariableName(i - 1), false);
                int v0 = BuildRecursively(expr1, n, i + 1);
                //xi = 1
                BooleanExpression expr2 = expr.SetVariableValue(expr.GetVariableName(i - 1), true);
                int v1 = BuildRecursively(expr2, n, i + 1);
                return MakeNode(i, v0, v1);
            }
        }
        
        public int Restrict(int j, bool value)
        {
            int u = GettDict().Size() - 1;
            return RestrictRecursively(u, j, value);
        }

        private int RestrictRecursively(int u, int j, bool value)
        {
            int index = GettDict().Index(u);
            if (index == null)
            {
                return u;
            }
            else if (index > j)
            {
                return u;
            }
            else if (index < j)
            {
                int integer = MakeNode(index,
                        RestrictRecursively(GettDict().Low(u), j, value),
                        RestrictRecursively(GettDict().High(u), j, value));
                IndexLowHigh ilh = GettDict().Get(u);
                GethDict().Remove(ilh);
                GettDict().Remove(u);
                return integer;
            }
            else
            {
                if (!value)
                {
                    int integer = RestrictRecursively(GettDict().Low(u), j, value);
                    IndexLowHigh ilh = GettDict().Get(u);
                    GethDict().Remove(ilh);
                    GettDict().Remove(u);
                    return integer;
                }
                else
                {
                    int integer = RestrictRecursively(GettDict().High(u), j, value);
                    IndexLowHigh ilh = GettDict().Get(u);
                    GethDict().Remove(ilh);
                    GettDict().Remove(u);
                    return integer;
                }
            }
        }

        public BitArray AnySat()
        {
            return AnySatRecursively(GettDict().Size() - 1);
        }

        private BitArray AnySatRecursively(int u)
        {
            if (u == 0)
            {
                Console.WriteLine("error any sat");
                return null;
            }
            else if (u == 1)
            {
                return new BitArray(GettDict().Size());
            }
            else if (GettDict().Low(u) == 0)
            {
                BitArray bitArray = AnySatRecursively(GettDict().High(u));
                bitArray.Set(GettDict().Index(u) - 1, true);
                return bitArray;
            }
            else
            {
                BitArray bitArray = AnySatRecursively(GettDict().Low(u));
                bitArray.Set(GettDict().Index(u) - 1, false);
                return bitArray;
            }
        }

        public List<BitArray> AllSat()
        {
            return AllSatRecursively(GettDict().Size() - 1);
        }

        private List<BitArray> AllSatRecursively(int u) {
        if (u == 0) {
            return new List<BitArray>();
        }
        else if(u == 1) {
            List<BitArray> bitArrays = new List<BitArray>();
            bitArrays.Add(new BitArray(GettDict().Size())); // Или size-1
            return bitArrays;
        }
        else {
            int index = GettDict().Index(u);
            int low = GettDict().Low(u);
            int high = GettDict().High(u);
            List<BitArray> returnedListLow = AllSatRecursively(low);

            List<BitArray> returnedListHigh = AllSatRecursively(high);
            foreach (BitArray bitSet in returnedListHigh) {
                bitSet.Set(index - 1, true);
            }
            List<BitArray> list = new List<BitArray>();
            list.AddRange(returnedListLow);
            list.AddRange(returnedListHigh);
            return list;
        }
    }
    }
}
