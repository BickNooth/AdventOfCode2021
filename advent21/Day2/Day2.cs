namespace advent21;

public class Day2 : DayBase
{
    public void Run()
    {
        var input = GetStringAndNumberInput(".\\Day2\\puzzleinput.txt"); //Puzzle Input

        Console.WriteLine($"Total: {ProcessInputPart2(input)}"); ; 
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

    private int ProcessInputPart2(IEnumerable<(string direction, int distance)> input)
    {
        return input.Aggregate(
            (Depth: 0, Horizontal: 0, Aim: 0),
            (direction, input) =>
            {
                switch (input.direction)
                {
                    case "up":
                        direction.Aim -= input.distance;
                        break;
                    case "down":
                        direction.Aim += input.distance;
                        break;
                    case "forward":
                        direction.Horizontal += input.distance;
                        direction.Depth += (direction.Aim * input.distance);
                        break;
                }
                return direction;
            }, (direction) => direction.Horizontal * direction.Depth);
    }
}