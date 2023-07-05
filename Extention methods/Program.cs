using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

public static class Extientions
{
    public static string TitleCase(this string s)//Task 1
    {
        bool first = true;
        string toReturn = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsLetter(s[i]) && first == true)
            {
                toReturn += char.ToUpper(s[i]);
                first = false;
            }
            else
            {
                toReturn += s[i];
                if (!char.IsLetter(s[i]))
                    first = true;
            }
        }
        return toReturn;
    }
    public static List<T> SortBySpecificElement<T>(this List<T> f, Func<T,int> func) //Task 2
    {
        //TO TEST INSERT THIS COMMENT IN MAIN

        //Person person3 = new Person(); 
        //person3.age = 2;
        //Person person1 = new Person();
        //person1.age = 1;
        //Person person2 = new Person();
        //person2.age = 3;
        //List<Person> list = new List<Person>();
        //list.Add(person3);
        //list.Add(person1);
        //list.Add(person2);
        //list = list.SortBySpecificElement<Person>(p => p.age);
        //foreach (Person person in list)
        //{
        //    Console.WriteLine(person.age);
        //}
        //
        //class Person
        //{
        //    public int age;
        //}
    List<T> toReturn = f;
        int[] nums = new int[toReturn.Count];
        int k = 0;
        foreach (T item in toReturn)
        {
            nums[k++] = func(item);
        }

        int temp;
        T tempObj;
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] > nums[j])
                {
                    temp = nums[i];
                    tempObj = toReturn[i];

                    nums[i] = nums[j];
                    toReturn[i] = toReturn[j];

                    nums[j] = temp;
                    toReturn[j]= tempObj;
                }
            }
        }
        return toReturn;
    }
    public static string ToFriendlyDate(this DateTime dt) //Task 3
    {
        string[] mounths = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        return mounths[dt.Month - 1] + " " + dt.Day.ToString() + ", " + dt.Year.ToString();
    }

    public static bool IsValidEmail(this string str) //Task 4
    {
        int atIndex = str.IndexOf('@');
        int dotIndex = str.IndexOf('.');
        if (dotIndex < atIndex && dotIndex > 0)
            return true;
        return false;
    }
    public static string TrimByLevon(this string str) //Task 5
    {
        while(str[0] == ' ')
        {
           str = str.Remove(0, 1);
        }
        str.Reverse();
        while (str[0] == ' ')
        {
            str = str.Remove(0, 1);
        }
        str.Reverse();
        return str;
    }
    //public static string ToJson(this object obj)//Task 6 
    //{
    //    return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    //}
    public static double Round(this double num, int acuracity)//Task 7
    {
        string numStr = num.ToString();
        int index = numStr.IndexOf(".");
        numStr = numStr.Substring(0, index + acuracity + 1);

        return Convert.ToDouble(numStr);
    }
    public static bool FileExists(this string path)//Task 8
    {
        return File.Exists(path);
    }

    public static T ConvertToEnum<T>(this string arg)//Task 9
    {
        return (T)Enum.Parse(typeof(T),arg);
    }

    public static List<T> FilterList<T>(this IEnumerable<T> arrayToBeFiltered,Func<T,bool> condition)//Task 10
    {
        //List<T> toReturn = new List<T>();
        //for (int i = 0; i < arrayToBeFiltered.Count(); i++)
        //{
        //    if (condition(arrayToBeFiltered.ToList()[i]))
        //    {
        //        toReturn.Add(arrayToBeFiltered.ToList()[i]);
        //    }
        //}
        //return toReturn;
        return arrayToBeFiltered.Where(condition).ToList();
    }

    public static int CalaculateTotalDuration(this IEnumerable<CallRecord> arg,DateTime start,DateTime end)//Task 11
    {

        int totalDuration = 0;
        foreach (CallRecord record in arg)
        {
            if(record.callDate >= start && record.callDate <= end)
            totalDuration += record.duration;   
        }
        return totalDuration;
    }
   public static 

}
 public class CallRecord : IEnumerable<CallRecord>
    {
        public int duration;
        public int cost;
        public DateTime callDate;
        public CallRecord(int duration, int cost, DateTime arg)
        {
            this.duration = duration;
            this.cost = cost;
            callDate = arg;
        }

        public IEnumerator<CallRecord> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

class Program
{
    public static void Main()
    {

    }
}

