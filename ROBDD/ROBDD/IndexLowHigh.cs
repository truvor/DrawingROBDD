using System;

namespace ROBDD
{
    class IndexLowHigh
    {
        private int index;
        private int low;
        private int high;

        public IndexLowHigh(int index, int low, int high)
        {
            this.index = index;
            this.low = low;
            this.high = high;
        }

        public int GetIndex()
        {
            return index;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetLow()
        {
            return low;
        }

        public void SetLow(int low)
        {
            this.low = low;
        }

        public int GetHigh()
        {
            return high;
        }

        public void SetHigh(int high)
        {
            this.high = high;
        }

        public bool equals(Object o)
        {
            if (this == o) return true;
            if (o == null || !Object.ReferenceEquals(GetType(), o.GetType())) return false;
            IndexLowHigh that = (IndexLowHigh)o;

            if (high != that.high) return false;
            if (index != that.index) return false;
            if (low != that.low) return false;

            return true;
        }
        
        public int HashCode()
        {
            int result = index;
            result = 31 * result + low;
            result = 31 * result + high;
            return result;
        }
    }
}
