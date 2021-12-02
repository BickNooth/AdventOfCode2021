namespace advent21
{
    public class DayBase
    {

        public static IEnumerable<int> GetNumericalInput(string pathToFile)
        {
            StreamReader reader = new StreamReader(pathToFile);
            string[] lines = reader.ReadToEnd().Split();
            return lines.Where(
                                line => !string.IsNullOrEmpty(line)
                            ).Select(
                                line => line.Trim()
                            ).Select(
                                line => int.Parse(line)
                            ).ToArray();
        }

        public static IEnumerable<(string direction, int distance)> GetStringAndNumberInput(string pathToFile)
        {
            StreamReader reader = new StreamReader(pathToFile);
            string[] lines = reader.ReadToEnd().Split(Environment.NewLine);
            return lines.Where(
                                line => !string.IsNullOrEmpty(line)
                            ).Select(
                                line => line.Split(' ')
                            ).Select(
                                lineArray => (
                                    lineArray[0],
                                    int.Parse(lineArray[1])
                                )
                            );
        }
    }
}