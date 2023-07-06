using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
    public static List<Sms> FilterSms(this IEnumerable<Sms> listToFilter, string keyword)//Task 12
    {
        return listToFilter.Where(a => a.Text.Contains(keyword) || a.Sender == keyword).ToList();
    }
    public static string FormatNumber(this string number)//Task 13
    {
        return number.Insert(0,"(").Insert(4,")").Insert(1,"+").Insert(6,"-").Insert(9,"-").Insert(13,"-");
    }
    public static int CalaculateTotalDataUsage(this IEnumerable<DataUsage> arg,DateTime start,DateTime end)//Task 14
    {

        int totalDuration = 0;
        foreach (DataUsage record in arg)
        {
            if(record.Date >= start && record.Date <= end)
            totalDuration += record.DataUsed;   
        }
        return totalDuration;
    }
    static Plan plan1 = new Plan(3000,500,3);
    static Plan plan2 = new Plan(15000,10000,10);
    public static int CostCalculator(this IEnumerable<CallRecord> arg,Plan plan)//Task 15
    {
        int totalDuration = 0;
        int totalCost = 0;
        DateTime end = DateTime.Now.AddDays(DateTime.Now.Day * (-1));
        DateTime start = DateTime.Now.AddDays(DateTime.Now.Day * (-1)).AddMonths(-1);
        foreach (CallRecord record in arg)
        {
            if(record.callDate >= start && record.callDate <= end)
            totalDuration += record.duration;   
        }
        int minutesDiference = plan.Minutes - totalDuration;
        if (minutesDiference >= 0)
            return plan.Cost;
        return plan.Cost + minutesDiference * (-1) * plan.StandartMinutesCost;
    }
    public static int CostCalculator(this IEnumerable<DataUsage> arg,Plan plan)
    { 
        double totalGBs = 0;
        int totalCost = 0;
        DateTime end = DateTime.Now.AddDays(DateTime.Now.Day * (-1));
        DateTime start = DateTime.Now.AddDays(DateTime.Now.Day * (-1)).AddMonths(-1);
        foreach (DataUsage record in arg)
        {
            if(record.Date >= start && record.Date <= end)
            totalGBs += record.DataUsed;   
        }
        double InternetDiference = plan.Internet - totalGBs;
        if (InternetDiference >= 0)
            return plan.Cost;
        return plan.Cost + (int)(InternetDiference * (-1) * plan.StandartInternetCost);
    }
    public static GeoLocation FindNearestTower(this GeoLocation userLocation,List<GeoLocation> towers)//Task 20
    {
        double[] distances = new double[towers.Count];
        int i = 0;
        foreach (var item in towers)
        {
            distances[i++] = GeoLocation.CalculateDistance(userLocation, item);
        }
        return towers[Array.IndexOf(distances,distances.Min())];
    }
    public static double CalculateSpeed(this IEnumerable<NetworkSpeed> arg,DateTime start,DateTime end)//16
    {
        int S = 0;
        TimeSpan T = TimeSpan.Zero;
        foreach (var item in arg)
        {
            if (item.DateStart >= start && item.DateEnd <= end)
            {
                S += item.DataUsed;
                T += item.DateEnd - item.DateStart;
            }
        }
        return S / T.TotalSeconds;
    }
    public static List<CallRecord> RoamingDetector(this IEnumerable<CallRecord> callRecords)//17
    {
        List<CallRecord> toReturn = new List<CallRecord>();
        foreach (var callRecord in callRecords)
        {
            if (!CallRecord.ArmenianOperatorsList.Contains(callRecord.mobileOperator))
            {
                toReturn.Add(callRecord);
            }
        }
        return toReturn;
    }
    public static Plan PlanRecomendation(this IEnumerable<CallRecord> callRecords)//18
    {
        DateTime end = DateTime.Now.AddDays(DateTime.Now.Day * (-1));
        DateTime start = DateTime.Now.AddDays(DateTime.Now.Day * (-1)).AddMonths(-1);
        int totalMinutesUsed = callRecords.CalaculateTotalDuration(start, end);
        Plan.BuiltInPlans = Plan.BuiltInPlans.OrderBy( a => a.Minutes).ToArray();
        for (int i = 0; i < Plan.BuiltInPlans.Length; i++)
        {
            if (Plan.BuiltInPlans[i].Minutes >= totalMinutesUsed)
            {
                return Plan.BuiltInPlans[i];
            }
        }
        return new Plan(0,0,0);
    }

}
public class NetworkSpeed
{
    public int DataUsed;
    public DateTime DateStart;
    public DateTime DateEnd;
}
public class GeoLocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public static double CalculateDistance(GeoLocation A , GeoLocation B)
    {
        return 3440.1 * Math.Acos((Math.Sin(A.Latitude) * Math.Sin(B.Latitude)) + Math.Cos(A.Latitude) *Math.Cos(B.Latitude) * Math.Cos(A.Longitude - B.Longitude));
    }
}
public class Plan
{
    public int Cost;
    public int Minutes;
    public int Internet;
    public readonly int StandartMinutesCost = 10;
    public readonly int StandartInternetCost = 1000;
    public static Plan[] BuiltInPlans = {new Plan(1500,500,3),new Plan(3000,1000,6),new Plan(10000,5000,50) };
    public Plan(int cost, int minutes, int internet)
    {
        Cost = cost;
        Minutes = minutes;
        Internet = internet;
    }
}
public class Sms
{
    public Sms(string arg, string sender)
    {
        Text = arg;
        Sender = sender;
    }
    public string Text;
    public string Sender;
}
 public class CallRecord : IEnumerable<CallRecord>
    {
        public static readonly string[] ArmenianOperatorsList = {"Ucom","Beeline","Rostelecom" };
        public int duration;
        public int cost;
        public DateTime callDate;
        public string mobileOperator;
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
public class DataUsage
{
    public int DataUsed;
    public DateTime Date;
}

class Program
{
    public static void Main()
    {
        
    }
}

