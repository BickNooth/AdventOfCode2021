namespace advent21;

public class Day2 : DayBase
{
    public void Run()
    {
        var input = GetStringAndNumberInput(".\\Day2\\puzzleinput.txt"); //Puzzle Input

        Console.WriteLine($"Total: {ProcessInputPart1(input)}"); ; // Part 1 
        Console.ReadKey();
    }

    private int ProcessInputPart1(IEnumerable<(string direction, int distance)> input)
    {        
        return input.Aggregate(
            (Depth: 0, Horizontal: 0),
            (direction, input) =>
        {
            switch (input.direction)
            {
                case "up":
                    direction.Depth -= input.distance;
                    break;
                case "down":
                    direction.Depth += input.distance;
                    break;
                case "forward":
                    direction.Horizontal += input.distance;
                    break;
            }
            return direction;
        }, (direction) => direction.Horizontal * direction.Depth);
    }
}