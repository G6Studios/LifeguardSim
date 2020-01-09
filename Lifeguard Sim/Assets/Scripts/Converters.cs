// Can't save bools to playerprefs so we use these

public static class Converters
{
    public static int BoolToInt(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }

    public static bool IntToBool(int value)
    {
        if (value != 0)
            return true;
        else
            return false;
    }
}