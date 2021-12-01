namespace advent21;

public class Day1 : DayBase
{
    public void Run()
    {
        var input = GetNumericalInput(".\\Day1\\puzzleinput.txt"); //Puzzle Input
        //ProcessInputIndividual(input); // Part 1 
        ProcessInputWithThreeMeasurementSpan(input);
    }

    private void ProcessInputIndividual(IEnumerable<int> input)
    {
        var depths = input.ToList();
        int increasedFromPreviousMeasurement = 0;
        int decreasedFromPreviousMeasurement = 0;

        for (int i = 1; i < depths.Count(); i++) {
            if (depths[i] > depths[i-1])
            {
                increasedFromPreviousMeasurement++;
                continue;
            } 
            decreasedFromPreviousMeasurement++;
        }

        Console.WriteLine($"Increased: {increasedFromPreviousMeasurement}");
        Console.WriteLine($"Decreased: {decreasedFromPreviousMeasurement}");
        Console.ReadKey();
    }

    private void ProcessInputWithThreeMeasurementSpan(IEnumerable<int> input)
    {
        var depths = input.ToList();
        int increasedFromPreviousMeasurement = 0;
        int decreasedFromPreviousMeasurement = 0;


        for (int i = 1; i < depths.Count(); i++)
        {
            int currentDepth = 
                depths.ElementAtOrDefault(i) + 
                depths.ElementAtOrDefault(i + 1) + 
                depths.ElementAtOrDefault(i + 2);
            int previousDepth = 
                depths.ElementAtOrDefault(i-1) + 
                depths.ElementAtOrDefault(i) + 
                depths.ElementAtOrDefault(i + 1);

            if (currentDepth > previousDepth)
            {
                increasedFromPreviousMeasurement++;
                continue;
            }
            decreasedFromPreviousMeasurement++;
        }

        Console.WriteLine($"Increased: {increasedFromPreviousMeasurement}");
        Console.WriteLine($"Decreased: {decreasedFromPreviousMeasurement}");
        Console.ReadKey();
    }

}