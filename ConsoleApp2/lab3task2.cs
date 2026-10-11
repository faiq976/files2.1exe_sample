using System;

class VideoGame
{
    public string CharacterName;
    public string Level;
    public double HealthPoints;
    public double AttackPower;
    public double DefensePower;
    public double ExperiencePoints;
    public double Stamina;
    public string SpecialAbility;
    public VideoGame()
    {
        CharacterName = "Unknown";
        Level = 0;
        HealthPoints = 0;
        AttackPower = 00;
        DefensePower = 00;
        ExperiencePoints = 5;
        Stamina = 10;
        SpecialAbility = "Unknown";
    }
    public VideoGame(string CharacterName, string Level, double HealthPoints, double AttackPower, double DefensePower, double ExperiencePoints, double Stamina, string SpecialAbility)
    {
        this.CharacterName = CharacterName;
        this.Level = Level;
        this.HealthPoints = HealthPoints;
        this.AttackPower = AttackPower;
        this.DefensePower = DefensePower;
        this.ExperiencePoints = ExperiencePoints;
        this.Stamina = Stamina;
        this.SpecialAbility = SpecialAbility;
    }
    public void LevelIncrease()
    {
        Level += Level;
    }
    public void UpdateHealthPoints(double damage)
    {
        HealthPoints += HealthPoints;
    }
    public void StaminaRestore(string potion)
    {
        return Stamina;
    }
    public void TotalPower(double attack, double defense)
    {
        TotalPower = attack + defense;
    }
    public void Display()
    {
        Console.WriteLine("CharacterName:" + CharacterName);
        Console.WriteLine("Level:" + Level);
        Console.WriteLine("Health Points:" + HealthPoints);
        Console.WriteLine("Attack  Power:" + AttackPower);
        Console.WriteLine("Defense Power:" + DefensePower);
        Console.WriteLine("Experience Points:" + ExperiencePoints);
        Console.WriteLine("Stamina:" + Stamina);
        Console.WriteLine("Special Ability:" + SpecialAbility);
    }
    static void Main(string[] args)
    {
        VideoGame videogame = new VideoGame();
        videogame.LevelIncrease();
        videogame.UpdateHealthPoints(20);
        videogame.StaminaRestore(10);
        videogame.TotalPower(25, 20);
        videogame.Display();
    }
}