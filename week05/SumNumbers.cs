public class SumNumbers {
    if n == 1 {
        return 1;
    }
    return n + SumNumbers(n-1);
}