using System.ComponentModel;

public static class Divisors {
    /// <summary>
    /// Entry point for the Divisors class
    /// </summary>
    public static void Run() {
        List<int> list1 = FindDivisors(80);
        Console.WriteLine("<List>{" + string.Join(", ", list1) + "}"); // <List>{1, 2, 4, 5, 8, 10, 16, 20, 40}
        List<int> list2 = FindDivisors(79);
        Console.WriteLine("<List>{" + string.Join(", ", list2) + "}"); // <List>{1}
    }

    /// <summary>
    /// Create a list of all divisors for a number including 1
    /// and excluding the number itself. Modulo will be used
    /// to test divisibility.
    /// </summary>
    /// <param name="number">The number to find the divisor</param>
    /// <returns>List of divisors</returns>
    private static List<int> FindDivisors(int number) {
        List<int> results = new();
        // TODO problem 1
        for (int i = 1; i <= number / 2; i++) {
            if (number % i == 0) {
                results.Add(i);
            }
        }
        return results;
    }
}