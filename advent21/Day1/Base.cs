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
    }
}