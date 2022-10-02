class Lab1
{
    public static void Main()
    {
        Console.WriteLine(GetNumberOfLeter("ggdgdv", 'h'));
        Console.WriteLine(GetNumberOfLeter("sgrsfvhhhh", 's'));
        Console.WriteLine(GetNumberOfLeter("aaaaaaaba", 'a'));
    }

    public static int GetNumberOfLeter(string src, char letter)
    {
        //ініціалізація змінної кількості літер в стрічці
        int count = 0;

        //проходження по кожній літері циклу
        foreach (char obj in src)
        {
            //якщо літера такаж сама як аргумент функції додати кількість в лічильник
            if (obj == letter)
                // додання одиниці до лічильника
                count++;
        }
        //повертання кількості літер
        return count;
    }
}

