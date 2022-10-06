using Lab1;
using Lab2;
class Program
{
    public static void Main()
    {
        BinaryNum num1 = new BinaryNum(-5);
        BinaryNum num2 = new BinaryNum(5);
        Console.WriteLine(BinaryNum.Add(num1, num2));
    }
}