using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        //Create List to hold the multiples
        List<double> multiples = new List<double>();

        // Loop 'length' times to generate the multiples of 'number'
        for (int i = 1; i <= length; i++) {
            // Add multiples of 'number' to the list
            multiples.Add(number * i);
        }

        // Return multiples and converts a list to an array 
        return multiples.ToArray();
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Ensures the amount is within the bounds of the data count
        amount = amount % data.Count();

        // Extract the last 'amount' elements (elements that will be moved to the front)
        List<int> lastElements = data.GetRange(data.Count - amount, amount);

        // Extract the first 'amount' elements (elements that will come after the rotated part)
        List<int> firstElements =data.GetRange(0, data.Count - amount);
        
        // Clear the original list and rebuild it in the rotated order
        data.Clear();
        data.AddRange(lastElements);
        data.AddRange(firstElements);
    }
}