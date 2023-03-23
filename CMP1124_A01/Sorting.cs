namespace CMP1124_A01;

// This will have all of the sorting algorithms implemented in it as static methods

public class Sorting
{
    
    // Bubble Sort
    // Taken from lecture slides
    public static int[] BubbleSort(int[] a, int n){
        for (int i = 0; i < n-1; i++) {
            for (int j = 0; j < n-1-i; j++){
                if (a[j + 1] < a[j]) {
                    (a[j], a[j + 1]) = (a[j + 1], a[j]);
                }
            }
        }

        return a;
    }
    
}