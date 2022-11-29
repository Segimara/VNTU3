
using IronPdf;
using MassPDFparser;
using System.Diagnostics.SymbolStore;
using System.Text;

class Program
{
    static readonly string path = @"C:\Users\Segimara\Documents\GitHub\VNTU\MassPDFparser\MassPDFparser\parse";
    static readonly string SortedPath = @"C:\Users\Segimara\Documents\GitHub\VNTU\MassPDFparser\MassPDFparser\Sorted";
    static readonly string NonSortedPath = @"C:\Users\Segimara\Documents\GitHub\VNTU\MassPDFparser\MassPDFparser\NonSorted";
    static readonly List<string> BannedChars = new List<string>()
    {
        @"/",
        @"\",
        @":",
        @";",
        "\"",
        "\'",
        @"<",
        @">",
        @",",
        @"|",
        @"_",
        @"*",
        @"«",
        @"»",
        "\r",
        "\n",
        "”",
        "“",
    };
    static string[] Globalfiles;
    static List<string> ExistingLessons = new List<string>();
    static int countOfCorrupted = 0;
    public static void Main()
    {
        string[] folders = Directory.GetDirectories(Program.path);
        foreach (var folder in folders)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine($"###START###");
            Console.WriteLine($"{folder}");
            string[] files = Directory.GetFiles(folder, "*.pdf");
            Globalfiles = files;
            //ParallelLoopResult res = Parallel.ForEach<string>(files, Parse);
            //foreach (string file in files)
            //{
            //    Parse(file);
            //}
            for (int i = 0; i < files.Length;)
            {
                int count = 3;
                if (i + count > files.Length)
                {
                    count = files.Length - i;
                }
                Parallel.For(i, i + count, Parse);
                if (i + count > files.Length)
                {
                    i += files.Length - i;
                }
                else
                {
                    i += 3;
                }
                //Parse(i);
                //i++;
            }
            Console.WriteLine($"###END###");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
        }
        Console.WriteLine(countOfCorrupted);
        //string[] folders = Directory.GetDirectories(Program.path);
        //foreach (var folder in folders)
        //{
        //    string[] files = Directory.GetFiles(folder, "*.pdf");
        //    foreach (string file in files)
        //    {
        //        Console.WriteLine("###################################################################################################");
        //        Console.WriteLine(file);
        //        Console.WriteLine("---------------------------------------------------------------------------------------------------");
        //        Console.WriteLine(SortedPath + @"\" + "AAAAA" + file.Remove(0, file.LastIndexOf(@"\")));
        //        Console.WriteLine("###################################################################################################");
        //    }
        //}
        //Console.WriteLine(countOfCorrupted);
    }

    static void Parse(int index)
    {
        //ParseTemp(index, SortByLesson);
        ParseTemp(index, SortByLang);
    }

    static void ParseTemp(int index, Func<IEnumerable<string>, ValidateObj> func)
    {
        try
        {
            PdfDocument document = PdfDocument.FromFile(Globalfiles[index]);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(document.ExtractTextFromPage(15));
            stringBuilder.Append(document.ExtractTextFromPage(16));
            stringBuilder.Append(document.ExtractTextFromPage(17));
            stringBuilder.Append(document.ExtractTextFromPage(18));
            stringBuilder.Append(document.ExtractTextFromPage(20));
            stringBuilder.Append(document.ExtractTextFromPage(21));
            stringBuilder.Append(document.ExtractTextFromPage(22));
            stringBuilder.Append(document.ExtractTextFromPage(23));
            stringBuilder.Append(document.ExtractTextFromPage(24));
            stringBuilder.Append(document.ExtractTextFromPage(25));
            stringBuilder.Append(document.ExtractTextFromPage(26));
            IEnumerable<string> lines = stringBuilder.ToString().Split("\n");
            ValidateObj LessonObj = func.Invoke(lines);
            
            //Console.WriteLine(str);
            //Console.WriteLine(SortedPath + @"\" + LessonName + str.Remove(0, str.LastIndexOf(@"\")));
            if (LessonObj.isCorrect)
            {
                if (ExistingLessons.Contains(LessonObj.LessonName))
                {
                    //Console.WriteLine(SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    File.Copy(Globalfiles[index], SortedPath + @"\" + LessonObj.LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                }
                else
                {
                    //Console.WriteLine(SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    ExistingLessons.Add(LessonObj.LessonName);
                    System.IO.Directory.CreateDirectory(SortedPath + @"\" + LessonObj.LessonName);
                    File.Copy(Globalfiles[index], SortedPath + @"\" + LessonObj.LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    Console.WriteLine(Globalfiles[index]);
                }
            }
            else
            {
                //File.Copy(Globalfiles[index], NonSortedPath + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(Globalfiles[index]);
            Console.WriteLine(ex.Message);
            //File.Copy(Globalfiles[index], NonSortedPath + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
            countOfCorrupted++;
        }
    }
    public static ValidateObj SortByLesson(IEnumerable<string> lines)
    {
        ValidateObj result = new ValidateObj();
        bool nextLine = false;
        foreach (var item in lines)
        {
            if (item.ToLower().Contains("з дисципліни") || nextLine)
            {
                result.isCorrect = true;
                result.LessonName = item;
                result.LessonName = result.LessonName.Replace("з дисципліни", "");
                result.LessonName = result.LessonName.Replace("З дисципліни", "");
                foreach (var ch in BannedChars)
                {
                    result.LessonName = result.LessonName.Replace(ch, "");
                }
                result.LessonName = result.LessonName.Trim();
                if (string.IsNullOrEmpty(result.LessonName))
                {
                    nextLine = true;
                }
                else
                {
                    break;
                }
            }
        }
        return result;
    }
    public static ValidateObj SortByTheme(IEnumerable<string> lines)
    {
        ValidateObj result = new ValidateObj();
        bool nextLine = false;
        foreach (var item in lines)
        {
            if (item.ToLower().Contains("на тему") || nextLine)
            {
                result.isCorrect = true;
                result.LessonName = item;
                result.LessonName = result.LessonName.Replace("на тему", "");
                result.LessonName = result.LessonName.Replace("На тему", "");
                result.LessonName = result.LessonName.Replace("на Тему", "");
                result.LessonName = result.LessonName.Replace("НА ТЕМУ", "");
                foreach (var ch in BannedChars)
                {
                    result.LessonName = result.LessonName.Replace(ch, "");
                }
                result.LessonName = result.LessonName.Trim();
                result.LessonName = result.LessonName.ToLower();
                if (string.IsNullOrEmpty(result.LessonName))
                {
                    nextLine = true;
                }
                else
                {
                    break;
                }
            }
        }
        return result;
    }
    public static ValidateObj SortByLang(IEnumerable<string> lines)
    {
        ValidateObj result = new ValidateObj();
        bool nextLine = false;
        bool correct = false;
        foreach (var item in lines)
        {
            if (item.ToLower().Contains("динамічний масив")|| nextLine)
            {
                result.isCorrect = true;
                result.LessonName = "динамічний масив";
                break;
            }
        }
        return result;
    }
}