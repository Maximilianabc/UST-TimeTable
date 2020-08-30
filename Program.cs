using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace UST_Timetable
{
    public class Major
    { 
        public string Abbrieviation { get; set; }
        public List<Course> courses { get; set; } = new List<Course>();
        public Major() { }
        public Major(string abbr) => Abbrieviation = abbr;
    }
    public class Course
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public byte Credits { get; set; }
        public string Prerequisites { get; set; }
        public List<TimeSlot> timeSlots { get; set; } = new List<TimeSlot>();
        public Course() { }
        public Course(string code, string title) => (Code, Title) = (code, title);
    }
    public class TimeSlot
    {
        public string Section { get; set; }
        public string Code { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public string Instructor { get; set; }
        public ushort Quota { get; set; }
        public ushort Enrolled { get; set; }
        public ushort Available { get; set; }
        public ushort Waiting { get; set; }
    }

    class Program
    {
        static string[] MajorNames = new string[]
        { 
          "ACCT", "AESF", "BIBU", "BIEN", "BIPH", "BTEC",
          "CBME", "CENG", "CHEM", "CHMS", "CIEM", "CIVL", "COMP", "CPEG", "CSIT", "DSCT",
          "ECON", "EEMT", "EESM", "ELEC", "ENEG", "ENGG", "ENTR", "ENVR", "ENVS", "EVNG", "EVSM", "FINA", "GBUS", "GNED", "HART", "HLTH", "HMMA", "HUMA",
          "IBTM", "IDPO", "IEDA", "IIMP", "ISDN", "ISOM", "JEVE", "LABU", "LANG", "LIFS",
          "MAED", "MAFS", "MARK", "MASS", "MATH", "MECH", "MESF", "MFIT", "MGCS", "MGMT", "MILE", "MIMT", "MSBD", "MSDM", "MTLE",
          "NANO", "OCES", "PDEV", "PHYS", "PPOL", "RMBI", "SBMT", "SCIE", "SHSS", "SOSC", "SUST", "TEMG", "UROP", "WBBA" 
        };
        static List<Major> majors = new List<Major>();

        public static void Main(string[] args)
        {
            /*System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            w.Start();
            foreach (string name in MajorNames)
            {
                majors.Add(new Major(name));
            }

            string baseURL = "https://w5.ab.ust.hk/wcq/cgi-bin/2010/";
            string html = SendRequest(baseURL);
            MatchCollection mc = Regex.Matches(html, @"(?:,|\[)'([A-Z]{4} \w+) - ([^\.]*?) \/", RegexOptions.Singleline);
            foreach (Match match in mc)
            {
                Major major = majors.Find(m => m.Abbrieviation == match.Groups[1].Value.Substring(0, 4));
                major.courses.Add(new Course(match.Groups[1].Value, match.Groups[2].Value));
            }

            foreach(string name in MajorNames)
            {
                Task.Run(() =>
                {
                    html = SendRequest($"{baseURL}subject/{name}");
                    mc = Regex.Matches(html, @"");
                    Major major = majors.Find(m => m.Abbrieviation == name);
                    string[] courses = html.Split(new string[] { @"<div class=""courseinfo"">" }, StringSplitOptions.None).Skip(1).ToArray();
                    foreach (string course in courses)
                    {
                        Match m = Regex.Match(course, @"<h2>([A-Z]{4} \w+).*?\((\d)");
                        Course c = major.courses.Find(cc => cc.Code == m.Groups[1].Value);
                        c.Credits = byte.Parse(m.Groups[2].Value);

                        if (course.Contains("PRE-REQUISITE"))
                        {
                            c.Prerequisites = Regex.Match(course, @"<th>PRE-REQUISITE<\/th><td>(.*?)<\/td>").Groups[1].Value;
                        }
                        MatchCollection mm = Regex.Matches(course, @"<tr class=""(newsect )?(?:secteven|sectodd)"">\s?(?:<td align=""\w+""(?: rowspan=""\d"")?>\s?([A-Z]+\d+[A-Z]?) \((\d+)\)<\/td>\s)?(?:<td>([\w\s-]+))?<(?:td|br)>([\w\s-:]+)<\/td><td>([\w\s]+)<\/td><td>(?:<a .*?>)?([A-Za-z\s,]+)(?:<\/a>)?<\/td>(?:<\/tr>|.*?<(?:span|td.*?)>(\d+)<\/(?:span|td)>.*?<td.*?>(\d+)<\/td>.*?<td.*?>(?:<strong>)?(\d+)(?:<\/strong>)?<\/td>.*?<td.*?>(?:<strong>)?(\d+)(?:<\/strong>)?<\/td>.*?)", RegexOptions.Singleline);
                        foreach (Match mmm in mm)
                        {
                            string time = mmm.Groups[4].Value +
                                           (string.IsNullOrEmpty(mmm.Groups[4].Value)
                                           ? (mmm.Groups[5].Value == "TBA" ? "" : "Every ")
                                           : " ") + mmm.Groups[5].Value + ";";
                            if (!string.IsNullOrEmpty(mmm.Groups[1].Value)) // newsect
                            {
                                c.timeSlots.Add(new TimeSlot()
                                {
                                    Section = mmm.Groups[2].Value,
                                    Code = mmm.Groups[3].Value,
                                    Time = time,
                                    Room = mmm.Groups[6].Value,
                                    Instructor = mmm.Groups[7].Value,
                                    Quota = ushort.Parse(mmm.Groups[8].Value),
                                    Enrolled = ushort.Parse(mmm.Groups[9].Value),
                                    Available = ushort.Parse(mmm.Groups[10].Value),
                                    Waiting = ushort.Parse(mmm.Groups[11].Value)
                                });
                            }
                            else
                            {
                                c.timeSlots.Last().Time += $"\n{time}";
                            }
                        }
                    }
                });
            }
            w.Stop();
            Console.WriteLine(w.ElapsedMilliseconds);
            Console.ReadLine();*/
            // byte[] json = JsonSerializer.SerializeToUtf8Bytes(majors);
            // File.WriteAllBytes(@"C:\Users\Maximilian\Desktop\ttb.json", json);
            File.Delete(@"C:\Users\Maximilian\Desktop\Academic Stuff\ttb.json");
        }

        static string SendRequest(string URL)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            string ress;
            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                ress = sr.ReadToEnd();
            }
            return ress;
        }
    }
}
