using System.Collections.Generic;

namespace ROBDD
{
    class HDictionary
    {
        private Dictionary<IndexLowHigh, int> HDict;

        public HDictionary()
        {
            this.HDict = new Dictionary<IndexLowHigh, int>();
        }

        public bool Member(IndexLowHigh ilh)
        {
            return HDict.ContainsKey(ilh);
        }

        public int Lookup(IndexLowHigh ilh)
        {
            return HDict[ilh];
        }

        public void Insert(IndexLowHigh ilh, int node)
        {
            HDict.Add(ilh, node);
        }

        public void Insert(int i, int l, int h, int node)
        {
            IndexLowHigh ilh = new IndexLowHigh(i, l, h);
            HDict.Add(ilh, node);
        }

        public int Size()
        {
            return HDict.Count;
        }

        public void Remove(IndexLowHigh ilh)
        {
            HDict.Remove(ilh);
        }
    }
}
