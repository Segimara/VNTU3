using Lab1;
using Lab2;
class Program
{
    public static void Main()
    {
        //BinaryNum num1 = new BinaryNum(-24);
        //BinaryNum num2 = new BinaryNum(4);
        //Console.WriteLine(BinaryNum.Add(num1, num2));
        Console.WriteLine(NumberSystemsConverter.toNumberBase("2232.4435", 10, 2));

        Console.WriteLine(NumberSystemsConverter.toNumberBase("2232.4435", 10, 8));

        Console.WriteLine(NumberSystemsConverter.toNumberBase("2232.4435", 10, 16));
    }
}