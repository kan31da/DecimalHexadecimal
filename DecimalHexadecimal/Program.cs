using System.Text;

const int readLength = 7;
const int substringLength = 6;
const int startIndexSubstring = 1;
const int formBaseToInt32 = 16;
const string fileName = "rfid.txt";

string input = string.Empty;
StringBuilder sb = new();

do
{
    Console.Write("Enter rfid card fob: ");
    input = Console.ReadLine();   

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

    Console.WriteLine($"To quit pres Escape or any key to continue\r\n");

    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
    {
        Environment.Exit(0);
    }

} while (true);


Environment.Exit(0);