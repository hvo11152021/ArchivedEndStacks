using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] testArray = { 52, 65, 51, 29, 58 };

            //BubbleSort(testArray);
            //SelectionSort(testArray);
            //QuickSort(testArray);

            //foreach (int i in testArray)
            //    Console.Write(i + ", ");

            //Comparison of complexities of the three sorting algorithms... how fast can each one sort this large array?

            int[] array1 = new int[100000];
            int[] array2 = new int[100000];
            int[] array3 = new int[100000];

            Random r = new Random();
            for (int i = 0; i < 100000; i++)
            {
                array1[i] = r.Next(10000);
                array2[i] = array1[i];
                array3[i] = array1[i];
            }

            //using BubbleSort
            long diff = DateTime.Now.Ticks;
            BubbleSort(array1);
            diff = DateTime.Now.Ticks - diff;

            TimeSpan span = new TimeSpan(diff);
            Console.WriteLine($"BubbleSort: {span.TotalSeconds}");

            //Using SelectionSort
            diff = DateTime.Now.Ticks;
            SelectionSort(array2);
            diff = DateTime.Now.Ticks - diff;

            span = new TimeSpan(diff);
            Console.WriteLine($"SelectionSort: {span.TotalSeconds}");


            //Using QuickSort
            diff = DateTime.Now.Ticks;
            QuickSort(array2);
            diff = DateTime.Now.Ticks - diff;

            span = new TimeSpan(diff);
            Console.WriteLine($"QuickSort: {span.TotalSeconds}");



            Console.ReadKey();
        }

        static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
        static void QuickSort(int[] arr, int left, int right)
        {
            //stop when left == right
            if (left < right)
            {
                int pivot = Partition(arr, left, right);      //index of the pivot value

                //Call QuickSort on the left part and the right part of the pivot
                if (pivot > 1)
                    QuickSort(arr, left, pivot - 1);        //don't sort the 'left' branch when there are less than two elements there
                
                if (pivot + 1 < right)
                    QuickSort(arr, pivot + 1, right);       //don't sort the 'right' branch when there are less than two elements there
            }
        }
        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];  //the value chosen as pivot is the leftmost value (chosen randomly)

            while (true)            //keep moving indexes and swapping values until left == right
            {
                while (arr[left] < pivot)   //move left pointer up until its value >= pivot
                    left++;

                while (arr[right] > pivot)  //move right pointer down until its value <= pivot
                    right--;

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;      //handle duplicate values - no point in swapping them

                    //perform the swap
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;   //or return left, they are equal at this point.
                }

            }
        }

        static void SelectionSort(int[] arr)
        {
            for (int pass = 0; pass < arr.Length-1; pass++)     //'pass' is the element we want to position
            {
                int best = pass;    //start the best at current position then move up if we find a better one

                for (int i=pass+1; i<arr.Length; i++)
                {
                    if (arr[i] < arr[best])             
                        best = i;                       //store the new 'best' (lowest) value.
                }

                //at this point we have the current 'best' value for this pass
                //swap 'best' with 'pass'
                int temp = arr[pass];
                arr[pass] = arr[best];
                arr[best] = temp;
            }
        }

        static void BubbleSort(int[] arr)
        {
            int i, j;                       //two indexes for comparing elements
            int upperBound = arr.Length;

            bool swapped = true;

            while(swapped)
            {
                i = 0;
                j = 1;
                upperBound--;               //optimization - we don't need to check the end of the array because BubbleSort positions the last element correctly on each pass
                swapped = false;

                while (j <= upperBound)
                {
                    if (arr[i] > arr[j])
                    {
                        //swap 
                        int temp = arr[i];  //store the left value
                        arr[i] = arr[j];
                        arr[j] = temp;

                        swapped = true;     //set swapped to true if a swap is made during this pass
                    }

                    i++;            //move indexes up to the next pair of elements to compare
                    j++;
                }
            }
        }
    }
}
