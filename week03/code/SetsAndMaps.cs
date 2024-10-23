using System.Text.Json;
using System.Xml.Schema;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        // Create a set to store the words for quick lookup
        HashSet<string> wordSet = new HashSet<string>(words);

        // List to store the results
        List<string> result = new List<string>();

        //Iterate through each word
        foreach(string word in words){
            // Generate the reverse of the word
            string reverseWord = new string(word.Reverse().ToArray());

            // Check if the reverse exists in the set and is not the same word (like 'aa')
            if (wordSet.Contains(reverseWord) && word != reverseWord){
                // Add the pair to the result (format it as "am & ma")
                result.Add($"{word} & {reverseWord}");

                // Remove both words from the set to prevent duplicates
                wordSet.Remove(word);
                wordSet.Remove(reverseWord);
            }
        }
        // Return the result as an array
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // Ensure there are at least 4 columns in the line
            if (fields.Length >= 4)
            {
                // The degree is in the 4th column (index 3)
                var degree = fields[3].Trim();  // Trim to remove any extra spaces

                // Check if the degree is already in the dictionary
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;  // Increment the count if already present
                }
                else
                {
                    degrees[degree] = 1;  // Add a new entry if not present
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // Normalize both words by converting to lowercase and removing spaces
        string normalizedWord1 = word1.ToLower().Replace(" ", "");
        string normalizedWord2 = word2.ToLower().Replace(" ", "");

        // If the lengths don't match after normalization, they can't be anagrams
        if (normalizedWord1.Length != normalizedWord2.Length)
        {
            return false;
        }

        // Create a dictionary to count the occurrences of letters in word1
        var letterCount = new Dictionary<char, int>();

        // Count each letter in the first word
        foreach (char letter in normalizedWord1)
        {
            if (letterCount.ContainsKey(letter))
            {
                letterCount[letter]++;
            }
            else
            {
                letterCount[letter] = 1;
            }
        }

        // For each letter in the second word, decrease the count in the dictionary
        foreach (char letter in normalizedWord2)
        {
            if (!letterCount.ContainsKey(letter))
            {
                // If the letter is not in the dictionary, the words are not anagrams
                return false;
            }

            letterCount[letter]--;

            // If the count drops below 0, the words are not anagrams
            if (letterCount[letter] < 0)
            {
                return false;
            }
        }

        // If all counts are zero, the words are anagrams
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        // const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        // using var client = new HttpClient();
        // using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        // using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        // using var reader = new StreamReader(jsonStream);
        // var json = reader.ReadToEnd();
        // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        // Using HttpClient to get the JSON data from the USGS
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        // Set options for JSON deserialization (case-insensitive property names)
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Deserialize JSON into our FeatureCollection class
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Create a list to store the earthquake summaries
        var earthquakeSummaries = new List<string>();

        // Loop through each feature (earthquake) and extract the place and magnitude
        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place;
            var magnitude = feature.Properties.Magnitude;

            // Create a summary string
            earthquakeSummaries.Add($"{place} - Mag {magnitude}");
        }

        // Return the list of earthquake summaries as an array
        return earthquakeSummaries.ToArray();
    }
}