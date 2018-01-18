using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class Apriori
    {
        internal static int itms = 0;
        internal static string[] frqitmset = new string[10];
        internal static int[] s = new int[10];
        // All the methods are defined here
        //*******************************************************************************************************************************************************
        public virtual void calc_support_method(string[] frqitmset, string[] articles, int initial, int stop, int index, int grpof, int support, string file)
        {
            Apriori instance_sup = new Apriori();
            if (index == grpof)
            {
                instance_sup.calc_support_method_base(articles, grpof, support, file);
                return;
            }
            for (int i = initial; i <= stop && stop - i + 1 >= grpof - index; i++)
            {
                articles[index] = frqitmset[i];
                instance_sup.calc_support_method(frqitmset, articles, i + 1, stop, index + 1, grpof, support, file);
            }
        }
        public virtual void calc_support_method_base(string[] frqitmset, int grpof, int support, string file)
        {
            int g = 0, dgt = 0;
            string[] ans = new string[grpof];
            string @string = "";
            System.IO.StreamReader filereader = new System.IO.StreamReader(file);
            while (!string.ReferenceEquals((@string = filereader.ReadLine()), null))
            {
                int d = 0;
                string[] token = @string.Split(',');
                for (int i = 0; i < grpof; i++)
                {
                    for (int q = 0; q < token.Length; q++)
                    {
                        if (frqitmset[i].Equals(token[q]))
                        {
                            ans[i] = frqitmset[i];
                            d++;
                        }
                    }
                }
                if (d == grpof)
                {
                    g++;
                }
            }
            dgt = g * 10;
            if (dgt > support)
            {
                Console.WriteLine("(" + string.Join(",", ans) + ") : " + dgt);
            }
        }
        public virtual void calc_confidence_method(string[] frqitmset, string[] articles, int initial, int stop, int no, int a, int confidence, string file)
        {
            Apriori inst3 = new Apriori();
            if (no == a)
            {
                for (int r = 1; r < stop; r++)
                {
                    string[] tmpdt = new string[r];
                    inst3.calc_confidence_method_base(frqitmset, tmpdt, 0, stop - 1, 0, r, confidence, articles, a, file);
                }
                return;
            }
            for (int i = initial; i <= stop && stop - i + 1 >= a - no; i++)
            {
                articles[no] = frqitmset[i];
                inst3.calc_confidence_method(frqitmset, articles, i + 1, stop, no + 1, a, confidence, file);
            }
        }
        public virtual void calc_confidence_method_base(string[] frqitmset, string[] tmpdt, int initial, int stop, int no, int r, int confidence, string[] articles, int a, string file)
        {
            Apriori inst4 = new Apriori();
            Apriori inst5 = new Apriori();
            if (no == r)
            {
                for (int j = 0; j < a; j++)
                {
                    for (int k = 0; k < r; k++)
                    {
                        if (articles[j].Equals(tmpdt[k]))
                        {
                            return;
                        }
                    }
                }
                inst4.result(confidence, r, file, tmpdt, articles, a);
                return;
            }
            for (int u = initial; u <= stop && stop - u + 1 >= r - no; u++)
            {
                tmpdt[no] = frqitmset[u];
                inst5.calc_confidence_method_base(frqitmset, tmpdt, u + 1, stop, no + 1, r, confidence, articles, a, file);
            }
        }
        // This method will compare the input confidence with the calculated confidence and gives output
        public virtual void result(int confidence, int r, string file, string[] tmpdt, string[] articles, int a)
        {
            int conf_var = 0, support = 0, calc_confidence = 0;
            System.IO.StreamReader br = new System.IO.StreamReader(file);
            string line = "";
            while (!string.ReferenceEquals((line = br.ReadLine()), null))
            {
                int var_temp = 0, var2_temp = 0;
                char[] delimiterChars = { ',', '\t' };
                string[] token = line.Split(delimiterChars);
                for (int i = 0; i < a; i++)
                {
                    for (int q = 0; q < token.Length; q++)
                    {
                        if (articles[i].Equals(token[q]))
                        {
                            var_temp++;
                            break;
                        }
                    }
                }
                if (var_temp == a)
                {
                    conf_var++;
                    for (int i = 0; i < r; i++)
                    {
                        for (int q = 0; q < token.Length; q++)
                        {
                            if (tmpdt[i].Equals(token[q]))
                            {
                                var2_temp++;
                                break;
                            }
                        }
                    }
                    if (var2_temp == r)
                    {
                        support++;
                    }
                }
            }
            if (conf_var > 0)
            {
                calc_confidence = support * 100 / conf_var;
            }
            if (calc_confidence > confidence)
            {
                Console.WriteLine("{" + string.Join(",", articles) + "} ---> {" +
                string.Join(",", tmpdt) + "} : " + calc_confidence + "%");
            }
        }
        //*********************************************************************************************************************
        // Main metod starts here
        public static void Main(string[] args)
        {
            // object initialization
            Apriori inst1 = new Apriori();
            Apriori inst2 = new Apriori();
            System.IO.StreamReader br = null;
            string readline = "";
            string[] articles;
            int initial = 0, index = 0;
            //initializing count of all items to 0;
            int[] count = new int[10];
            for (int i = 0; i < 10; i++)
            {
                count[i] = 0;
            }
            string sname = "";
            int storenumber;
            string[] search = new string[] { "Jackets", "Tshirts", "Deo", "Wallet", "Scarf", "Bands", "Cap", "Goggles", "Sneakers", "Giftcards" };
            //store selection happens here
            //*****************************************************************************************************
            Console.WriteLine("Hi! My name is Jarvis ");
            Console.WriteLine("Please select the store number(1 to 5) to get the assosiation rules based on apriori algorithm");
            Console.WriteLine("1-Target,2-Amazon,3-Alibaba,4-Gap,5-Guess:");
            storenumber = Convert.ToInt32(Console.ReadLine());
            if (storenumber <= 5)
            {
                switch (storenumber)
                {
                    case 1:
                        sname = "Target";
                        break;
                    case 2:
                        sname = "Amazon";
                        break;
                    case 3:
                        sname = "Alibaba";
                        break;
                    case 4:
                        sname = "Gap";
                        break;
                    case 5:
                        sname = "Guess";
                        break;
                    default:
                        Console.WriteLine("You have to choose the correct store number");
                        break;
                }
            }
            else
            {
                Console.WriteLine("you have entered wrong store number. It should be a number between 1 to 5");
            }
            Console.WriteLine("Store number " + storenumber + "->" + sname + " is selected");
            Console.WriteLine();
            string file = Path.Combine(Environment.CurrentDirectory,
            @"..\..\" + sname + ".txt");
            //*********************************************************************************************************
            // Support and condfidence parameters are enetered here
            Console.WriteLine("Enter the Value for Support:");
            Console.WriteLine("Note: Values of Support , confidence values must be entered in percentage Ex: 50 ");
            int support, confidence;
            support = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Value for Confidence:");
            confidence = Convert.ToInt32(Console.ReadLine());
            // File parsing happens here
            try
            {
                br = new System.IO.StreamReader(file); Console.WriteLine();
                Console.WriteLine("The Algorithm gives following as result:");
                while (!string.ReferenceEquals((readline = br.ReadLine()), null))
                {
                    //each line is read .i.e each transaction in the store is being parsed
                    articles = readline.Split(',');
                    for (int i = 0; i < articles.Length; i++)
                    //each item is selected in a transaction
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (articles[i].Equals(search[j]))
                            //each item in transaction is compared with all items and when it is found the counter iis increased by 1
                            {
                                count[j]++;
                            }
                        }
                    }
                }
                // now count array has the number of times each item appeared in the transactions
                Console.WriteLine("frequent item set(1) with respect to Support value is:");
                for (int i = 0; i < 10; i++)
                {
                    if (count[i] > support * 20 / 100)
                    {
                        //frequent itemset is collected by pruning
                        frqitmset[itms] = search[i];
                        s[itms] = count[i] * 100 / 20;
                        itms++;
                        //frequent item set and their suppoer is displayed here
                        Console.WriteLine(search[i] + " : " + s[itms - 1]);
                    }
                }
                int stop = itms - 1;// in terms of array index
                Console.WriteLine();
                Console.WriteLine("frequent item sets having more than entered Support value: ");
                for (int grpof = 2; grpof <= stop; grpof++)
                {
                    articles = new string[grpof];
                    //This method jons the frequent itemsets in groups of 2 , 3 and so on and gives sets > minimum support value
                    inst1.calc_support_method(frqitmset, articles, initial, stop, index, grpof, support, file);
                }
                Console.WriteLine();
                Console.WriteLine("Association Rules as per Confidence " + confidence + " %"); Console.WriteLine();
                for (int i = 1; i < itms; i++)
                {
                    articles = new string[i];
                    //This method jons the frequent itemsets in groups of 2 , 3 and so on and gives sets > minimum confidence value
                    inst2.calc_confidence_method(frqitmset, articles, initial, stop, index, i, confidence, file);
                }
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); Console.Write(e.StackTrace);
            }
        }
    }
}