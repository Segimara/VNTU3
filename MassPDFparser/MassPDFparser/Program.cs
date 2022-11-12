
using IronPdf;
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
                //int count = 25;
                //if (i + count > files.Length)
                //{
                //    count = files.Length - i;
                //}
                //Parallel.For(i, i + count, Parse);
                //if (i + count > files.Length)
                //{
                //    i += files.Length - i;
                //}
                //else
                //{
                //    i += 25;
                //}
                Parse(i);
                i++;
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
        try
        {
            PdfDocument document = PdfDocument.FromFile(Globalfiles[index]);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(document.ExtractTextFromPage(0));
            stringBuilder.Append(document.ExtractTextFromPage(1));
            stringBuilder.Append(document.ExtractTextFromPage(2));
            stringBuilder.Append(document.ExtractTextFromPage(3));
            IEnumerable<string> lines = stringBuilder.ToString().Split("\n");
            bool isLesson = false;
            bool nextLine = false;
            string LessonName = "";
            foreach (var item in lines)
            {
                if (item.ToLower().Contains("з дисципліни") || nextLine)
                {
                    isLesson = true;
                    LessonName = item;
                    LessonName = LessonName.Replace("з дисципліни", "");
                    LessonName = LessonName.Replace("З дисципліни", "");
                    foreach (var ch in BannedChars)
                    {
                        LessonName = LessonName.Replace(ch, "");
                    }
                    LessonName = LessonName.Trim();
                    if (string.IsNullOrEmpty(LessonName))
                    {
                        nextLine = true;
                    }
                    else
                    {
                        break;
                    }
                   
                    
                }
            }
            //Console.WriteLine(str);
            //Console.WriteLine(SortedPath + @"\" + LessonName + str.Remove(0, str.LastIndexOf(@"\")));
            if (isLesson)
            {
                if (ExistingLessons.Contains(LessonName))
                {
                    //Console.WriteLine(SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    File.Copy(Globalfiles[index], SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                }
                else
                {
                    //Console.WriteLine(SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    //Console.WriteLine(Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    ExistingLessons.Add(LessonName);
                    System.IO.Directory.CreateDirectory(SortedPath + @"\" + LessonName);
                    File.Copy(Globalfiles[index], SortedPath + @"\" + LessonName + Globalfiles[index].Remove(0, Globalfiles[index].LastIndexOf(@"\")));
                    Console.WriteLine(Globalfiles[index]);
                }
            }
            else
            {
                //File.Copy(file, NonSortedPath + file.Replace(folder, ""));
                //File.Copy(str, NonSortedPath + str.Remove(0, str.LastIndexOf(@"\")));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(Globalfiles[index]);
            Console.WriteLine(ex.Message);
            //File.Copy(file, NonSortedPath + file.Replace(folder, ""));
            //File.Copy(str, NonSortedPath + str.Remove(0, str.LastIndexOf(@"\")));
            countOfCorrupted++;
        }
    }

    //static void Parse(string str)
    //{
    //    try
    //    {
    //        PdfDocument document = PdfDocument.FromFile(str);
    //        StringBuilder stringBuilder = new StringBuilder();
    //        stringBuilder.Append(document.ExtractTextFromPage(0));
    //        stringBuilder.Append(document.ExtractTextFromPage(1));
    //        stringBuilder.Append(document.ExtractTextFromPage(2));
    //        stringBuilder.Append(document.ExtractTextFromPage(3));
    //        IEnumerable<string> lines = stringBuilder.ToString().Split("\n");
    //        bool isLesson = false;
    //        string LessonName = "Бази данних";
    //        foreach (var item in lines)
    //        {
    //            if (item.ToLower().Contains("бази данних"))
    //            {
    //                isLesson = true;
    //                break;
    //            }
    //        }
    //        //Console.WriteLine(str);
    //        //Console.WriteLine(SortedPath + @"\" + LessonName + str.Remove(0, str.LastIndexOf(@"\")));
    //        if (isLesson)
    //        {
    //            if (ExistingLessons.Contains(LessonName))
    //            {
    //                File.Copy(str, SortedPath + @"\" + LessonName + str.Remove(0, str.LastIndexOf(@"\")));
    //            }
    //            else
    //            {
    //                ExistingLessons.Add(LessonName);
    //                System.IO.Directory.CreateDirectory(SortedPath + @"\" + LessonName);
    //                File.Copy(str, SortedPath + @"\" + LessonName + str.Remove(0, str.LastIndexOf(@"\")));
    //                Console.WriteLine(str);
    //            }
    //        }
    //        else
    //        {
    //            //File.Copy(file, NonSortedPath + file.Replace(folder, ""));
    //            //File.Copy(str, NonSortedPath + str.Remove(0, str.LastIndexOf(@"\")));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(str);
    //        //File.Copy(file, NonSortedPath + file.Replace(folder, ""));
    //        //File.Copy(str, NonSortedPath + str.Remove(0, str.LastIndexOf(@"\")));
    //        countOfCorrupted++;
    //    }
    //}
}