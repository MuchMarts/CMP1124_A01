namespace CMP1124_A01;

// This will have all of the sorting algorithms implemented in it as static methods

public class Algorithms
{
    //
    // SORTING ALGORITHMS
    //
    
    // Implement the following sorting algorithms:
    // 1. Bubble Sort
    // 2. Insertion Sort
    // 3. Merge Sort
    // 4. Quick Sort
    // 5. Heap Sort


    // Bubble Sort
    // Taken from lecture slides
    public static int[] BubbleSort(int[] a)
    {
        int n = a.Length;
        int counter = 0;
        for (int i = 0; i < n-1; i++) {
            for (int j = 0; j < n-1-i; j++)
            {
                counter++;
                if (a[j + 1] < a[j]) {
                    (a[j], a[j + 1]) = (a[j + 1], a[j]);
                }
            }
        }
        Console.WriteLine($"Bubble Sort: {counter} comparisons");
        return a;
    }
    
    // Insertion Sort
    // Taken from lecture slides
    static int[] InsertionSort(int[] data)
    {
        var counter = 0;
        var numSorted = 1;
        int index;
        var n = data.Length;
        while (numSorted < n)
        {
            var temp = data[numSorted];
            for (index=numSorted; index>0; index--)
            {
                counter++;
                if (temp < data[index-1]) data[index] = data[index - 1];
                else break;
            }
            
            // reinsert value
            data[index] = temp;
            numSorted++;
        }
        Console.WriteLine($"Insertion Sort: {counter} comparisons");
        return data;
    }
    
    // Merge Sort
    // Taken from lecture slides
    private static void Merge(int[] data, int[] temp, int low, int middle, int high, ref int counter) {

        var ri = low;
        var ti = low;
        var di = middle;
        
        while (ti < middle && di <= high)
        {
            if (data[di] < temp[ti]) data[ri++] = data[di++];
            else data[ri++] = temp[ti++];
            counter++;
        }
        
        while (ti < middle){
            data[ri++] = temp[ti++];
            counter++;
        }
    }
    private static void MergeSortRecursive(int[] data, int[] temp, int low, int high, ref int counter) {
        var n = high-low+1;
        var middle = low + n/2;
        int i;
        
        if (n < 2) return;

        for (i=low; i<middle; i++){
            temp[i] = data[i];
            counter++;
        }
        
        MergeSortRecursive(temp, data, low, middle-1, ref counter);
        MergeSortRecursive(data, temp, middle, high, ref counter);
        Merge(data, temp, low, middle, high, ref counter);
    }
    public int[] MergeSort(int[] data) {
        int counter = 0;
        var n = data.Length;
        var temp = new int[n];
        MergeSortRecursive(data, temp, 0, n-1, ref counter);
        Console.WriteLine($"Merge Sort: {counter} comparisons");
        return data;
    }
 
    // Quick Sort
    // Taken from lecture slides
    public static int[] QuickSort(int[] data)
    {
        var counter = 0;
        Quick_Sort(data, 0, data.Length - 1, ref counter);
        Console.WriteLine($"Quick Sort: {counter} comparisons");
        return data;
    }
    private static void Quick_Sort(int[] data, int left, int right, ref int counter)
    {
        int i, j;
        int pivot, temp;
        i = left;
        j = right;
        pivot = data[(left + right) / 2];
    
        do
        {
            while ((data[i] < pivot) && (i < right)) i++; counter++;
            while ((pivot < data[j]) && (j > left)) j--; counter++;
            counter++;
            if (i <= j)
            {
                temp = data[i];
                data[i] = data[j];
                data[j] = temp;
                i++;
                j--;
            }
        } while (i <= j);
        
        if (left < j) Quick_Sort(data, left, j, ref counter);
        if (i < right) Quick_Sort(data, i, right, ref counter);
    }

    // Heap Sort
    // Taken from lecture slides
    public static int[] HeapSort(int[] heap)
    {
        var counter = 0;
        var heapSize = heap.Length;
        int i;
        for (i = (heapSize - 1) / 2; i >= 0; i--)
        {
            Max_Heapify(heap, heapSize, i, ref counter);
        }
        for (i = heap.Length - 1; i > 0; i--)
        {
            (heap[i], heap[0]) = (heap[0], heap[i]);
            heapSize--;
            Max_Heapify(heap, heapSize, 0, ref counter);
        }
        Console.WriteLine($"Heap Sort: {counter} comparisons");
        return heap;
    }

    private static void Max_Heapify(int[] heap, int heapSize, int index, ref int counter)
    {
        while (true)
        {
            counter++;
            var left = (index + 1) * 2 - 1;
            var right = (index + 1) * 2;
            var largest = 0;
            if (left < heapSize && heap[left] > heap[index])
            {
                largest = left;
            }
            else
            {
                largest = index;
            }

            if (right < heapSize && heap[right] > heap[largest])
            {
                largest = right;
            }

            if (largest == index) return;
            (heap[index], heap[largest]) = (heap[largest], heap[index]);
            index = largest;
        }
    }
    
    //
    // SEARCHING ALGORITHMS
    //
    
    // Implement the following searching algorithms:
    // 1. Sequential Search
    // 2. Binary Search

    // Sequential Search
    // Taken from lecture slides
    public static int[] SequentialSearch(int[] a, int key)
    {
        var counter = 0;
        var result = new List<int>();
        // Delta is the difference between the index and the key used to determine closest values to the key
        var delta = int.MaxValue;

        for (int i = 0; i < a.Length; i++)
        {
            counter++;
            var tDelta = Math.Abs(i - key);
            // if the difference is less than the current delta, clear the list and add the current index
            // if new delta is smaller then all old values are no longer the closest
            if (tDelta < delta)
            {
                delta = tDelta;
                result.Clear();
                result.Add(i);
            }
            else if (tDelta == delta)
            {
                result.Add(i);
            }
        }
        
        Console.WriteLine($"Sequential Search: {counter} comparisons");
        return result.ToArray();
    }
    
    // Binary Search
    // Taken from lecture slides
    // REQUIRMENT: Array must be sorted

    public static int[] BinarySearch(int[] a, int key)
    {
        var counter = 0;
        
        var begin = 0;
        var end = a.Length - 1;
        var mid = end / 2;
        
        var result = new List<int>();
        
        while ((begin <= end) && (a[mid] != key))
        {
            counter++;
            if (key < a[mid])
            {
                end = mid - 1;
            }
            else
            {
                begin = mid + 1;
            }
            mid = (begin + end) / 2;
        }
        
        // Find all values that are the same as the key or near the key
        // Might be an inneficent way to do this
        result.Add(mid);

        for (int i = 1; i < a.Length; i++)
        {
            counter++;
            if (mid + i < a.Length && a[mid+i] == result[0]) result.Add(mid+i);
            else break;
        }
        for (int i = 1; i < a.Length; i++)
        {
            counter++;
            if (mid - i >= 0 && a[mid-i] == result[0]) result.Add(mid-i);
            else break;
        }

        return result.ToArray();
    }
    
}