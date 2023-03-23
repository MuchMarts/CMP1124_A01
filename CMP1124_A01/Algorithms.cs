namespace CMP1124_A01;

// This will have all of the sorting algorithms implemented in it as static methods

public class Algorithms
{
    //
    // SORTING ALGORITHMS
    //
    
    // Implement the following sorting algorithms:
    // 1. Bubble Sort O(n^2
    // 2. Insertion Sort O(n^2)
    // 3. Merge Sort O(n log n)


    // Bubble Sort
    // Taken from lecture slides
    public static int[] BubbleSort(int[] a, bool ascending = true)
    {
        int n = a.Length;
        int counter = 0;
        for (int i = 0; i < n-1; i++) {
            for (int j = 0; j < n-1-i; j++)
            {
                counter++;
                if (ascending)
                { 
                    if (a[j + 1] < a[j]) (a[j], a[j + 1]) = (a[j + 1], a[j]);
                }
                else
                {
                    if (a[j + 1] > a[j]) (a[j], a[j + 1]) = (a[j + 1], a[j]);
                }
                
            }
        }
        Console.WriteLine($"Bubble Sort: {counter} comparisons");
        return a;
    }
    
    // Insertion Sort
    // Taken from lecture slides
    public static int[] InsertionSort(int[] data, bool ascending = true)
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
                if (ascending)
                {
                    if (temp < data[index - 1]) data[index] = data[index - 1];
                    else break;
                }
                else
                {
                    if (temp > data[index - 1]) data[index] = data[index - 1];
                    else break;
                }
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
    private static void Merge(int[] data, int[] temp, int low, int middle, int high, ref int counter, bool ascending = true) 
    {

        var ri = low;
        var ti = low;
        var di = middle;
        
        while (ti < middle && di <= high)
        {
            counter++;
            if (ascending)
            {
                if (data[di] < temp[ti]) data[ri++] = data[di++];
                else data[ri++] = temp[ti++];
            }
            else
            {
                if (data[di] > temp[ti]) data[ri++] = data[di++];
                else data[ri++] = temp[ti++];
            }
        }
        
        while (ti < middle){
            data[ri++] = temp[ti++];
            counter++;
        }
    }
    private static void MergeSortRecursive(int[] data, int[] temp, int low, int high, ref int counter, bool ascending) {
        var n = high-low+1;
        var middle = low + n/2;
        int i;
        
        if (n < 2) return;

        for (i=low; i<middle; i++){
            temp[i] = data[i];
            counter++;
        }
        
        MergeSortRecursive(temp, data, low, middle-1, ref counter, ascending);
        MergeSortRecursive(data, temp, middle, high, ref counter,ascending);
        Merge(data, temp, low, middle, high, ref counter, ascending);
    }
    public static int[] MergeSort(int[] data, bool ascending = true) {
        int counter = 0;
        var n = data.Length;
        var temp = new int[n];
        MergeSortRecursive(data, temp, 0, n-1, ref counter, ascending);
        Console.WriteLine($"Merge Sort: {counter} comparisons");
        return data;
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
            var tDelta = Math.Abs(a[i] - key);
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
        result.Add(a[result[0]]);
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
        var result = new List<int>();
        result.Add(mid);

        for (int i = 1; i < a.Length; i++)
        {
            counter++;
            if (mid + i < a.Length && a[mid+i] == a[result[0]]) result.Add(mid+i);
            else break;
        }
        for (int i = 1; i < a.Length; i++)
        {
            counter++;
            if (mid - i >= 0 && a[mid-i] == a[result[0]]) result.Add(mid-i);
            else break;
        }
        Console.WriteLine($"Binary Search: {counter} comparisons");
        result.Add(a[result[0]]);
        return result.ToArray();
    }
    
}