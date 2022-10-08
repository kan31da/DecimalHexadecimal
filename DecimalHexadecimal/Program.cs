using System.Text;

const int readLength = 7;
const int substringLength = 6;
const int startIndexSubstring = 1;
const int formBaseToInt32 = 16;
const string fileName = "rfid.txt";

string input = string.Empty;
StringBuilder sb = new();

var taskKeys = new Task(ReadKeys);
var taskProcessFiles = new Task(ProcessFiles);

taskKeys.Start();
taskProcessFiles.Start();

var tasks = new[] { taskKeys };
Task.WaitAll(tasks);

do
{
    Console.Write("Enter rfid card fob: ");
    input = Console.ReadLine();

    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
    {
        Environment.Exit(0);
    }

    if (input == "EXIT")
    {
        Environment.Exit(0);
    }

    try
    {
        //System.Globalization.NumberStyles.HexNumber
        int decValue = int.Parse(input, System.Globalization.NumberStyles.Number);
        string hexValue = string.Format("{0:X}", decValue);

        sb.AppendLine($"DEC: {decValue}");

        if (hexValue.Length < readLength)
        {
            hexValue = hexValue.Insert(0, new string('0', readLength - hexValue.Length));
        }

        sb.AppendLine($"Full code HEX: {hexValue}");

        int value = Convert.ToInt32(hexValue.Substring(startIndexSubstring, substringLength), formBaseToInt32);

        //Console.WriteLine($"Full code DEC: {value:D10}");
        sb.AppendLine($"DEC 24 bit: {value}");

        using (StreamWriter writer = new(fileName, append: true))
        {
            await writer.WriteLineAsync(sb);
            writer.Close();
        }

    }
    catch (Exception)
    {
        Console.WriteLine("Enter again!");
    }

    Console.WriteLine(sb);
    sb.Clear();

    Console.WriteLine($"To Quit Enter: EXIT or pres Escape \r\n");


} while (true);


Environment.Exit(0);

static void ReadKeys()
{
    ConsoleKeyInfo key = new ConsoleKeyInfo();

    while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
    {

        key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Escape) Environment.Exit(0);
    }
}

static void ProcessFiles()
{
    var files = Enumerable.Range(1, 100).Select(n => "File" + n + ".txt");

    var taskBusy = new Task(BusyIndicator);
    taskBusy.Start();

    foreach (var file in files)
    {
        Thread.Sleep(1000);
        Console.WriteLine("Procesing file {0}", file);
    }

}

static void BusyIndicator()
{
    var busy = new ConsoleBusyIndicator();
    busy.UpdateProgress();
}

internal class ConsoleBusyIndicator
{
    int _currentBusySymbol;

    public char[] BusySymbols { get; set; }

    public ConsoleBusyIndicator()
    {
        BusySymbols = new[] { '|', '/', '-', '\\' };
    }
    public void UpdateProgress()
    {
        while (true)
        {
            Thread.Sleep(100);
            var originalX = Console.CursorLeft;
            var originalY = Console.CursorTop;

            Console.Write(BusySymbols[_currentBusySymbol]);

            _currentBusySymbol++;

            if (_currentBusySymbol == BusySymbols.Length)
            {
                _currentBusySymbol = 0;
            }

            Console.SetCursorPosition(originalX, originalY);
        }
    }
}