using System.Reflection;
using DeerJson.Util;

namespace DeerJson.Tests;

public class UtilTest
{
    internal class A
    {
        public static int       s_int;
        public        int       count;
        public        List<int> m_ints;
        private       string    nullableInt;

        public int Stu
        {
            get => count;
            set => count = value * 2;
        }

        public int PublicAutoStu { get; internal set; }
        private int PrivateAutoStu { get; set; }

        public void M1()
        {
        }

        public static void MStatic1()
        {
        }

        public object this[int index]
        {
            get => 123;
            set { }
        }
    }

    [Test]
    public void IsAutoProperty()
    {
        var pis = typeof(A).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var res = new HashSet<string>();
        foreach (var pi in pis)
        {
            if (TypeUtil.IsAutoProperty(pi))
            {
                res.Add(pi.Name);
            }
        }

        var expected = new HashSet<string> { "PublicAutoStu", "PrivateAutoStu" };

        Assert.That(expected, Is.EqualTo(res));
    }
}