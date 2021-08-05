using System.Linq;


public class FastParse
{
    public static short ShortParse(string value)
    {
        return value.Aggregate<char, short>(0, (current, character) => (short) (10 * current + (character - 48)));
    }

    public static int IntParse(string value)
    {
        return value.Aggregate<char, int>(0, (current, character) => 10 * current + (character - 48));
    }

    public static long LongParse(string value)
    {
        return value.Aggregate<char, long>(0, (current, character) => 10 * current + (character - 48));
    }

    public static double DoubleParse(string value)
    {
        return value.Aggregate<char, double>(0, (current, character) => 10 * current + (character - 48));
    }

    public static float FloatParse(string value)
    {
        return value.Aggregate<char, float>(0, (current, character) => 10 * current + (character - 48));
    }
}

