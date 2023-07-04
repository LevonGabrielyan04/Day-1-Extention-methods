using System.Collections.Generic;
using System.Reflection;
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
    public static string ToJson(this object obj)//Task 6
    {
        string toRetrun = "{" + "\n";
        var properties = obj.GetType().GetProperties();
        void recursion(PropertyInfo[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                //string str1 = properties[i].GetValue(obj).ToString();
                //string str2 = properties[i].PropertyType.ToString();
                try
                {
                    string str1 = properties[i].GetValue(obj).ToString();
                    string str2 = properties[i].PropertyType.ToString();
                    if (str1 == str2)
                    {
                        recursion(properties[i].GetType().GetProperties());
                    }
                    else
                    {
                        toRetrun += "'" + properties[i].Name + "':" + properties[i].GetValue(obj) + "\n";
                    }
                }
                catch (Exception)
                {
                    recursion(properties[i].GetType().GetProperties());
                }
            }
            toRetrun += "}";
        }
        recursion(properties);
        Console.WriteLine(toRetrun);
        return toRetrun;
    }
}
class School
{
    public Principal PrincipalName { get; set; }
    public string SchoolName { get; set; }
}
class Principal
{
    public string Name { get; set; }
    public int Age { get; set; }
}
class Program
{
    public static void Main()
    {
        Principal principal = new Principal();
        principal.Name = "Vazgen";
        principal.Age = 35;
        School school = new School();
        school.SchoolName = "Charenci anvan dproc";
        school.PrincipalName = principal;
        school.ToJson();
    }
}
