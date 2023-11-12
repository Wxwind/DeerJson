namespace DeerJson.Tests.Type;

public enum Days
{
    Sunday = 0, // 星期天
    Monday,     // 星期一
    Tuesday,    // 星期二
    Wednesday,  // 星期三
    Thursday,   // 星期四
    Friday,     // 星期五
    Saturday    // 星期六
}

public class DayObject
{
    public Days day;


    public DayObject()
    {
    }

    public DayObject(Days day)
    {
        this.day = day;
    }
}