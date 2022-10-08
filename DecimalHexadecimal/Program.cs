const int readLength = 7;
const int substringLength = 6;
const int startIndexSubstring = 1;
const int formBaseToInt32 = 16;

string input = string.Empty;

while (input != "EXIT")
{
    Console.Write("Enter rfid card fob: ");
    input = Console.ReadLine();
    

    try
    {
        //System.Globalization.NumberStyles.HexNumber
        int decValue = int.Parse(input, System.Globalization.NumberStyles.Number);
        string hexValue = string.Format("{0:X}", decValue);

        Console.WriteLine($"DEC: {decValue}");

        Console.WriteLine($"Full code HEX: {string.Format("{0:X}", decValue)}");

        if (hexValue.Length < readLength)
        {
            hexValue = hexValue.Insert(0, new string('0', readLength - hexValue.Length));
        }

        Console.WriteLine(hexValue);

        int value = Convert.ToInt32(hexValue.Substring(startIndexSubstring, substringLength), formBaseToInt32);

        Console.WriteLine($"DEC 24 bit: {value}");

    }
    catch (Exception)
    {
        Console.WriteLine("Enter again!");
    }

    
    Console.WriteLine($"\r\nTo Quit Enter: EXIT \r\n");
}

Environment.Exit(0);