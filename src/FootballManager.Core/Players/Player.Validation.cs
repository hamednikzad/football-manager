namespace FootballManager.Core.Players;

public partial class Player
{
    private static void ValidateName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException(nameof(fullName));
        
    }

    private static void ValidateAge(int age)
    {
        if (age < 5)
            throw new ArgumentOutOfRangeException(nameof(age), age, "Not valid Age for playing football");
    }

    private static void ValidateHeight(int height)
    {
        if (height < 50)
            throw new ArgumentOutOfRangeException(nameof(height), height, "Not valid Height for playing football");
    }

    private static void ValidateNumber(int? number)
    {
        if(number is < 1)
            throw new ArgumentOutOfRangeException(nameof(number), number, "Not valid Number");

    }
}