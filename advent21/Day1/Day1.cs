namespace advent21;

public class Day1 : DayBase
{
    public void Run()
    {
        //var input = GetNumericalInput(".\\Day1\\testinput.txt"); //Test Input
        var input = GetNumericalInput(".\\Day1\\puzzleinput.txt"); //Puzzle Input
        ProcessInput(input);
    }

    private void ProcessInput(IEnumerable<int> input)
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

}