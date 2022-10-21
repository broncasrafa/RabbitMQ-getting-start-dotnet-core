
namespace Producer
{
    public static class ConsoleAppHelpers
    {
        public static void ExitApplication()
        {
            Console.WriteLine("Press [enter] to exit .....");
            Console.ReadLine();
        }

        public static void DisplayConsoleText(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
