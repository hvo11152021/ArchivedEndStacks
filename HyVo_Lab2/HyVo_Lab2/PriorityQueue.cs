using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

//Name: Hy Vo
//PROG1621: Lab 2

namespace HyVo_Lab2
{
    public class PriorityQueue : ICollection<Goal>
    {
        protected Goal firstGoal;
        protected int todayCount;
        protected int tmrCount;
        protected int weeklyCount;

        public bool IsReadOnly => false;

        public Goal this[int i]
        {
            get
            {
                Goal temp = firstGoal;
                for (int t = 0; t < i; t++)
                    temp = temp.Next;
                return temp;
            }
        }

        public int Count
        {
            get
            {
                Goal temp = firstGoal;
                int count = 0;
                while (temp != null)
                {
                    count++;
                    temp = temp.Next;
                }
                return count;
            }
        }

        public PriorityQueue()
        {
            Clear();
        }

        public PriorityQueue(Goal first)
        {
            this.firstGoal = first;
        }

        public void Enqueue(Goal newGoal)
        {
            if (firstGoal == null)
            {
                firstGoal = newGoal;
            }
            else if (Contains(newGoal) == true)
            {
                throw new Exception("Update failure! Similar goals found in the list!");
            }
            else
            {
                if (newGoal.TimePriority == Timeline.Today)
                {
                    newGoal.Next = firstGoal;
                    firstGoal = newGoal;
                    todayCount++;
                }
                else if (newGoal.TimePriority == Timeline.Tomorrow)
                {
                    Goal tmrGoal = newGoal;
                    Goal temp = firstGoal;
                    for (int i = 1; i < todayCount; i++)
                    {
                        temp = temp.Next;
                    }
                    tmrGoal.Next = temp.Next;
                    temp.Next = tmrGoal;
                    //Count++;
                    tmrCount += todayCount;
                    tmrCount++;
                }
                else if (newGoal.TimePriority == Timeline.This_Week)
                {
                    Goal weekGoal = newGoal;
                    Goal temp = firstGoal;
                    for (int i = todayCount; i < tmrCount; i++)
                    {
                        temp = temp.Next;
                    }
                    weekGoal.Next = temp.Next;
                    temp.Next = weekGoal;
                    //Count++;
                }
                else
                {
                    Goal additionalGoals = firstGoal;
                    while (additionalGoals.Next != null)
                    {
                        additionalGoals = additionalGoals.Next;
                    }
                    additionalGoals.Next = newGoal;
                    //Count++;
                }
            }
        }

        public bool Dequeue()
        {
            if (firstGoal.Next == null)
            {
                firstGoal = null;
                return false;
            }
            else
            {
                firstGoal = firstGoal.Next;
                switch (firstGoal.TimePriority)
                {
                    case Timeline.Today:
                        todayCount--;
                        break;
                    case Timeline.Tomorrow:
                        tmrCount--;
                        break;
                    case Timeline.This_Week:
                        weeklyCount--;
                        break;
                }
                return true;
            }
        }

        public Goal Peak(PriorityQueue input)
        {
            using (var loopGoals = input.GetEnumerator())
            {
                return loopGoals.Current;
            }
        }

        public bool Contains(Goal input)
        {
            Goal temp = firstGoal;
            while (temp != null)
            {
                if (temp.Equals(input))
                    return true;
                temp = temp.Next;
            }
            return false;
        }

        public void Add(Goal item) => Enqueue(item);

        public void Clear() => firstGoal = null;

        public void CopyTo(Goal[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(Goal item) => throw new NotImplementedException();

        public IEnumerator<Goal> GetEnumerator() => new QueueEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
