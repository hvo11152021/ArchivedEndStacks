using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Name: Hy Vo
//PROG1621: Lab 2

namespace HyVo_Lab2
{
    public class QueueEnumerator : IEnumerator<Goal>
    {
        private PriorityQueue _collection;
        private int curIndex;
        private Goal curGoal;

        public QueueEnumerator(PriorityQueue collection)
        {
            _collection = collection;
            curIndex = -1;
            curGoal = default(Goal);
        }

        public Goal Current => curGoal;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                curGoal = _collection[curIndex];
            }
            return true;
        }

        public void Reset()
        {
            curIndex = -1;
        }
    }
}
