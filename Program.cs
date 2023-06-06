using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FormattableString_VSCrasher;

internal class Program
{
    private static void Main(string[] args)
    {

        CrashWithFormattableString();

        // Bonus
        CrashWithInfiniteRecursion();
    }



    private static void CrashWithFormattableString()
    {
        var defCrash = FormattableStringFactory.Create("{1} yolo", "haha");


        var versionList = Enumerable.Range(0, 10000)
            .Select(x => FormattableStringHelper.Create($"v{x}"));

        var sql = versionList.Select(x => FormattableStringHelper.Create($"version = {x}"));

        var joined = sql.Join(" OR ");

        var sql1 = FormattableStringHelper.Create($"Select * from [abc] where {joined}");


        Debugger.Break();
        Console.WriteLine("This will never been called");
    }

    /// <summary>
    /// Bonus time :)
    /// </summary>
    private static void CrashWithInfiniteRecursion()
    {
        var infinite = new InfiniteCrash();
        Debugger.Break();
        Console.WriteLine("This will never been called");
    }
}

public class InfiniteCrash
{
    public string Infinite => this.Infinite;

    public override string ToString()
    {
        return Infinite;
    }
}


internal static class FormattableStringHelper
{
    public static FormattableString Create(FormattableString str)
    {
        return str;
    }

    public static FormattableString Join(this IEnumerable<FormattableString> strings, string separator)
    {
        FormattableString current = null;
        foreach (var str in strings)
        {
            if (current == null)
            {
                current = str;
            }
            else
            {
                current = $"{current}{separator}{str}";
            }
        }
        return current;
    }
}