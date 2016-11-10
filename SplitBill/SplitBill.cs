using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitBill
{
    class EmptyFile : Exception  //Customize Exception Class
    {
        public override string Message
        {
            get
            {
                return "File is Empty";  //If file is empty it give error on Console 
            }
        }
    }

    public class SplitBill
    {
        static List<float> temp_output = new List<float>();
        static List<float> input = new List<float>();

        static void Main(string[] args)
        {
            //Please add correct directory path
            //string path = "C:\\Users\\Bhavik\\Desktop\\Project_NET\\SplitBill\\";
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

            string file_name = Console.ReadLine();

            SplitBill a = new SplitBill();

            ///////Read file that is enterted by User////////////////////////////////////////////////////
            string line;
            try
            {

                System.IO.StreamReader file = new System.IO.StreamReader(path +"\\" +file_name + ".txt") ;
                //////// If file is empty thrown exception with error message/////////////////////////////
               
                while ((line = file.ReadLine()) != null)
                {
                    input.Add(float.Parse(line));
                }


                file.Close();


                if (input.Count == 0)
                {
                    throw new EmptyFile();
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            catch (EmptyFile ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();

            }
            //////////////////////////////////////////////////////////////////////////////////////////////

            int i = 0;

            //Check if output file already exists then delete data from the file/////
            if (File.Exists(path + file_name + ".txt.out"))
            {
                File.WriteAllText(path + file_name + ".txt.out", String.Empty);
            }

            //////////////////////////////////////////////////////////////////////////
            
            while (input[i] != 0)
            {
                int first_num = Convert.ToInt32(input[i]); //number of peopling participating in the camping trip
                List<float> temporary = new List<float>();

                int x = 0;
                while (x != first_num)
                {
                    i++;
                    int y = Convert.ToInt32(input[i]); // Number of bills per person
                    int z = 0; float sum = 0;
                    while (z != y)

                    {
                        i++;
                        sum = sum + input[i];
                        z++;
                    }
                    temporary.Add(sum); //Add Bills per person per trip : Example = 10.00+20.00 =30.00
                    x++;
                }

                // Call outputdata function to Calculate how much money each participant should collect from group 

                temp_output.AddRange(a.Cost_per_person(temporary)); 
                

                /////////// Write Output in New Generated Output text file///////////////////////////////
                if (temp_output != null)
                {
                    string output_file = path +"\\"+ file_name + ".txt" + ".out";

                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(output_file, true);

                    foreach (float x1 in temp_output)
                    {
                        if (x1 < 0)    // If value of Money ($) is negative then add value in ( ) : Example : -1.99 then ($1.99)
                        {
                            file1.WriteLine("($" + Math.Abs(x1) + ")");
                        }


                        else
                        {
                            file1.WriteLine("$" + x1);  // Simply write price with Dollar sign

                        }
                    }
                    file1.WriteLine("\n");    // Add blank Space after end of every trip


                    file1.Close();
                    temp_output.Clear();
                    i++;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////
            }

        }

        //Count how much money each participant should collect from group/////////////////////////////////////
        public List<float> Cost_per_person(List<float> lst)
        {
            float cost_per_person = lst.Average();
            List<float> t = new List<float>();
            foreach (float item in lst)
            {   
                float d = (float)Math.Round(cost_per_person - item, 2, MidpointRounding.ToEven);
                t.Add(d);
            }
            return t;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

