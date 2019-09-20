public enum Difficulty 
{

    Easy = 1,
    Normal = 2,
    Hard = 3
}

public static class DifficultyExtension
{
    public static Difficulty ToDifficulty(this int value)
    {
        switch (value)
        {
            case 1:
                return Difficulty.Easy;
            case 2:
                return Difficulty.Normal;
            case 3:
                return Difficulty.Hard;
            default:
                return Difficulty.Normal;
        }
    }

    public static int ToInt(this Difficulty value)
    {
        switch (value)
        {
            case Difficulty.Easy:
                return 1;
            case Difficulty.Normal:
                return 2;
            case Difficulty.Hard:
                return 3;
            default:
                return 2;
        }
    }

    public static float ToDifficultySpeedMultiplier(this Difficulty value)
    {
        switch (value)
        {
            case Difficulty.Easy:
                return 1.5f;
            case Difficulty.Normal:
                return 1.75f;
            case Difficulty.Hard:
                return 2f;
            default:
                return 1f;
        }
    }
}