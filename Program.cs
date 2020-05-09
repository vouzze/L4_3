using System;
using System.IO;


namespace L4_3
{
    class SubjectHeading
    {
        private string[] Word = new string[Sizing() - 1];
        private int[] PageNumber = new int[Sizing() - 1];
        private int[] NumberofWords = new int[Sizing() - 1];
        public string Heading;
        public SubjectHeading()
        {

        }
        public SubjectHeading(string word, int pagenumber, int numberofwords)
        {
            if (word.Length == 0) throw new System.Exception("Word is not found.");
            if (pagenumber < 0) throw new System.Exception("Page number is incorrect.");
            if (numberofwords < 0) throw new System.Exception("Number of pages is incorrect.");
        }
        public string[] GetWord
        {
            get
            {
                return Word;
            }
            set
            {
                Word = value;
            }
        }
        public int[] GetPageNumber
        {
            get
            {
                return PageNumber;
            }
            set
            {
                PageNumber = value;
            }
        }
        public int[] GetNumberofWords
        {
            get
            {
                return NumberofWords;
            }
            set
            {
                NumberofWords = value;
            }
        }
        public string GetHeading
        {
            get
            {
                return Heading;
            }
            set
            {
                Heading = value;
            }
        }
        public static int Sizing()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int size = lines.Length;
            f.Close();
            return size;
        }
        public void Opening()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            GetHeading = lines[0];
            int count = lines.Length;
            int t = 0;
            for (int i = 1; i < count; i++)
            {
                if (lines[i].Length > t) t = lines[i].Length;

            }
            char[,] temps = new char[t,count-1];
            for (int i = 1; i < count; i++)
            {
                    char[] tem = lines[i].ToCharArray();
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                    temps[j, i - 1] = tem[j];
                    }
            }
            bool start = false;
            bool go = false;
            string worr = "";
            string pagg = "";
            string numm = "";
            for (int i = 1; i < count; i++)
            {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                    if (!start)
                        {

                            if (temps[j, i-1] == '\t')
                            {
                                start = true;
                                GetWord[i-1] = worr + "\t\t\t\t\t\t";
                                worr = "";
                                j += 5;
                            }
                            else if (!(char.IsControl(temps[j, i-1])) && (!(char.IsWhiteSpace(temps[j, i-1])))) worr = worr + temps[j, i-1];
                        }
                        else if (!go)
                        {
                            if (temps[j, i-1] == '\t')
                            {
                                go = true;
                                GetPageNumber[i-1] = Int32.Parse(pagg);
                                pagg = "";
                                j += 5;
                            }
                            if (char.IsNumber(temps[j, i-1])) pagg = pagg + temps[j, i-1];
                        }
                        else
                        {
                            if (char.IsNumber(temps[j, i-1])) numm = numm + temps[j, i-1];
                            if (j == lines[i].Length - 1)
                            {
                                GetNumberofWords[i-1] = Int32.Parse(numm);
                                numm = "";
                                start = false;
                                go = false;
                                break;
                            }
                        }
                }
            }
            Console.WriteLine(Heading);
            for (int i = 0; i < GetWord.Length; i++)
            {
                Console.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
            }

        }
        public void Adding(string word, int pagenumber, int numberofwords)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Array.Resize(ref Word, Word.Length + 1);
            Array.Resize(ref PageNumber, PageNumber.Length + 1);
            Array.Resize(ref NumberofWords, NumberofWords.Length + 1);
            Word[GetWord.Length - 1] = word + "\t\t\t\t\t\t";
            PageNumber[GetPageNumber.Length - 1] = pagenumber;
            NumberofWords[GetNumberofWords.Length - 1] = numberofwords;
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetWord.Length; i++)
            {
                f.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
                Console.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
            }
            f.Close();
        }
        public int Searching(string w)
        {
            int n = -1;
            for (int i = 0; i < GetWord.Length; i++)
            {
                if (GetWord[i] == w + "\t\t\t\t\t\t") n = i;
            }
            if (n >= 0)
            {
                Console.WriteLine("____________________________________________________________________________________________________________\n");
                Console.WriteLine(GetWord[n] + GetPageNumber[n] + "\t\t\t\t\t\t" + GetNumberofWords[n]);
                Console.WriteLine("____________________________________________________________________________________________________________\n");
            }
            else Console.WriteLine("The word does not exist in the file.\n");
            return n;
        }
        public void Editing(int n, string word, int pagenumber, int numberofwords)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            if (word.Length == 0) throw new System.Exception("Word is not found.");
            if (pagenumber < 0) throw new System.Exception("Page number is incorrect.");
            if (numberofwords < 0) throw new System.Exception("Number of pages is incorrect.");
            GetWord[n] = word + "\t\t\t\t\t\t";
            GetPageNumber[n] = pagenumber;
            GetNumberofWords[n] = numberofwords;
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetWord.Length; i++)
            {
                f.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
                Console.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
            }
            f.Close();
        }
        public void Deleting(int n)
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            string[] a = GetWord;
            int[] ar = GetPageNumber;
            int[] arr = GetNumberofWords;
            bool start = false;
            for (int i = 0; i < GetWord.Length; i++)
            {
                if (i == n) start = true;
                if (i == GetWord.Length - 1)
                {
                    Array.Resize(ref a, a.Length - 1);
                    Array.Resize(ref ar, ar.Length - 1);
                    Array.Resize(ref arr, arr.Length - 1);
                    GetWord = a;
                    GetPageNumber = ar;
                    GetNumberofWords = arr;
                    break;
                }
                if (start)
                {
                    GetWord[i] = GetWord[i + 1];
                    GetPageNumber[i] = GetPageNumber[i + 1];
                    GetNumberofWords[i] = GetNumberofWords[i + 1];
                }
            }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetWord.Length; i++)
            {
                f.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
                Console.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
            }
            f.Close();
        }
        public void Sorting()
        {
            var fileStream = new FileStream(@"C:\Users\User\source\repos\L4_3\text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            if ((GetWord.Length != GetPageNumber.Length) || (GetWord.Length != GetNumberofWords.Length) || (GetPageNumber.Length != GetNumberofWords.Length)) throw new System.Exception("Information is missed.");
            string temp_1;
            int temp_2;
            int temp_3;
            string ex = "";
            for (int i = 0; i < GetPageNumber.Length - 1; i++)
                for (int j = i + 1; j < GetPageNumber.Length; j++)
                    if (GetPageNumber[i] > GetPageNumber[j])
                    {

                        temp_1 = GetWord[i];
                        GetWord[i] = GetWord[j];
                        GetWord[j] = temp_1;

                        temp_2 = GetPageNumber[i];
                        GetPageNumber[i] = GetPageNumber[j];
                        GetPageNumber[j] = temp_2;

                        temp_3 = GetNumberofWords[i];
                        GetNumberofWords[i] = GetNumberofWords[j];
                        GetNumberofWords[j] = temp_3;
                    }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetWord.Length; i++)
            {
                f.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
                Console.WriteLine(GetWord[i] + GetPageNumber[i] + "\t\t\t\t\t\t" + GetNumberofWords[i]);
            }
            f.Close();
        }
    }
    class Program
    {
        static void Main()
        {
            SubjectHeading r = new SubjectHeading();
            string task = "";
            string func = "";
            string wo = "";
            string pa = "";
            string nu = "";
            int pagn = -1;
            int numofp = -1;
            do
            {
                Console.WriteLine("\nEnter 'o' to open the file and 'e' to exit.");
                task = Console.ReadLine();
                switch (task)
                {
                    case "e": break;
                    case "o":
                        {
                            r.Opening();
                            Console.WriteLine("\n");
                            r.Sorting();
                            do
                            {
                                Console.WriteLine("\n'a' to add new information, 's' to search and 'r' to return.");
                                func = Console.ReadLine();
                                switch (func)
                                {
                                    case "r": break;
                                    case "a":
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nEnter the word: ");
                                                wo = Console.ReadLine();
                                            } while (wo.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the page number: ");
                                                pa = Console.ReadLine();
                                                pagn = Int32.Parse(pa);
                                            } while (pagn < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the number of words on the page: ");
                                                nu = Console.ReadLine();
                                                numofp = Int32.Parse(nu);
                                            } while (numofp < 0);
                                            r.Adding(wo, pagn, numofp);
                                            break;
                                        }
                                    case "s":
                                        {
                                            string wors = "";
                                            string choice = "";
                                            do
                                            {
                                                Console.WriteLine("\nEnter the word to search for: ");
                                                wors = Console.ReadLine();
                                            } while (wors.Length == 0);
                                            int def = r.Searching(wors);
                                            if (def >= 0)
                                            {
                                                do
                                                {
                                                    Console.WriteLine("\nEnter 'e' to edit, 'd' to delete and 'r' to return.");
                                                    choice = Console.ReadLine();
                                                    switch (choice)
                                                    {
                                                        case "r": break;
                                                        case "e":
                                                            {
                                                                string ww = "";
                                                                string pp = "";
                                                                string nn = "";
                                                                int pap = -1;
                                                                int nun = -1;
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the word to replace the previous one: ");
                                                                    ww = Console.ReadLine();
                                                                } while (ww.Length == 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the page number to replace the previous one: ");
                                                                    pp = Console.ReadLine();
                                                                    pap = Int32.Parse(pp);
                                                                } while (pap < 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the number of words on the page to replace the previous one: ");
                                                                    nn = Console.ReadLine();
                                                                    nun = Int32.Parse(nn);
                                                                } while (nun < 0);
                                                                r.Editing(def, ww, pap, nun);
                                                                break;
                                                            }
                                                        case "d":
                                                            {
                                                                r.Deleting(def);
                                                                break;
                                                            }
                                                        default: Console.WriteLine("Try again. ('e' to edit, 'd' to delete and 'r' to return)"); break;
                                                    }
                                                } while (choice != "r");
                                            }
                                            break;
                                        }
                                    default: Console.WriteLine("Try again. ('a' to add, 's' to search and 'e' to exit)"); break;
                                }
                            } while (func != "r");
                            break;
                        }
                    default: Console.WriteLine("Try again. ('o' to open file and 'e' to exit)"); break;
                }
            } while (task != "e");
        }
    }
}

