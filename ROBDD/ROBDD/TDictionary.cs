using System.Collections.Generic;

namespace ROBDD
{
    class TDictionary
    {
        private int AutoId = 0;
        private Dictionary<int, IndexLowHigh> TDict;

        public TDictionary()
        {
            this.TDict = new Dictionary<int, IndexLowHigh>();
            Init();
        }

        public void Init()
        {
            Add(null);
            Add(null);
        }

        public int Add(IndexLowHigh data)
        {
            TDict.Add(AutoId++, data);
            return AutoId - 1;
        }

        public int Index(int node)
        {
            IndexLowHigh ilh = TDict[node];
            
            if (TDict.TryGetValue(node, out ilh))
            {
                return ilh.GetIndex();
            }
            return -1;  //Изменить условие и в классе ROBDD
            //return null;
        }

        public int Low(int node)
        {
            IndexLowHigh ilh = TDict[node];
            if (TDict.TryGetValue(node, out ilh))
            {
                return ilh.GetLow();
            }
            return -1;  //Изменить условие и в классе ROBDD
        }

        public int High(int node)
        {
            IndexLowHigh ilh = TDict[node];
            if (TDict.TryGetValue(node, out ilh))
            {
                return ilh.GetHigh();
            }
            return -1;  //Изменить условие и в классе ROBDD
        }

        public int Size()
        {
            return TDict.Count;
        }

        public void Remove(int u)
        {
            TDict.Remove(u);
        }

        public IndexLowHigh Get(int index)
        {
            return TDict[index];
        }

        public Dictionary<int, IndexLowHigh> GettDict()
        {
            return TDict;
        }
    }
}