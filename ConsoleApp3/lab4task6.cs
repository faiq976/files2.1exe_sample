
using System;
using System.Collections.Generic;
using System.Linq;

class Stats
{
    public string Name;
    public double Damage;
    public double Penetration;
    public double Heal;
    public double Cost;
    public string Description;

    public Stats(string name, double damage, double penetration,
                 double heal, double cost, string description)
    {
        Name = name;
        Damage = damage;
        Penetration = penetration;
        Heal = heal;
        Cost = cost;
        Description = description;
    }
}

class Player
{
    public string Name { get; private set; }

    private double hp, maxHp;
    private double energy, maxEnergy;
    private double armor;

    private List<Stats> skillStatistics = new List<Stats>();

    public Player(string name, double health, double maxHealth,
                  double energy, double maxEnergy, double armor)
    {
        Name = name;
        maxHp = Math.Max(0, maxHealth);
        maxEnergy = Math.Max(0, maxEnergy);
        hp = Math.Clamp(health, 0, maxHp);
        this.energy = Math.Clamp(energy, 0, maxEnergy);
        this.armor = Math.Clamp(armor, 0, 100);
    }

    public void UpdateHealth(double value)
    {
        hp = Math.Clamp(value, 0, maxHp);
    }

    public void UpdateEnergy(double value)
    {
        energy = Math.Clamp(value, 0, maxEnergy);
    }

    public void UpdateArmor(double value)
    {
        armor = Math.Clamp(value, 0, 100);
    }

    public void UpdateName(string value)
    {
        Name = value;
    }

    public void LearnSkill(Stats skill)
    {
        if (skillStatistics.Any(s =>
            s.Name.Equals(skill.Name,
                StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Player already knows this skill.");
            return;
        }

        skillStatistics.Add(skill);
        Console.WriteLine(Name + " learned " + skill.Name + "!");
    }

    public List<Stats> GetSkills()
    {
        return skillStatistics;
    }

    public string Attack(Player target, string skillName)
    {
        Stats skill = skillStatistics.FirstOrDefault(s =>
            s.Name.Equals(skillName,
                StringComparison.OrdinalIgnoreCase));

        if (skill == null)
            return Name + " has not learned that skill!";

        if (hp <= 0)
            return Name + " cannot attack because they are defeated!";

        if (target.hp <= 0)
            return target.Name + " is already defeated!";

        if (energy < skill.Cost)
        {
            return Name + " attempted to use " + skill.Name +
                   ", but didn't have enough energy!";
        }

        double effectiveArmor =
            Math.Clamp(target.armor - skill.Penetration, 0, 100);

        double damage =
            skill.Damage * (100 - effectiveArmor) / 100.0;

        UpdateEnergy(energy - skill.Cost);
        target.UpdateHealth(target.hp - damage);

        double actualHeal = Math.Min(skill.Heal, maxHp - hp);
        UpdateHealth(hp + skill.Heal);

        string result = Name + " used " + skill.Name + ", " +
                        skill.Description + ", against " +
                        target.Name + ", doing " +
                        damage.ToString("F2") + " damage!";

        if (skill.Heal > 0)
            result += " " + Name + " healed for " +
                      actualHeal.ToString("F2") + " health!";

        if (target.hp <= 0)
        {
            result += " " + target.Name + " died.";
        }
        else
        {
            double percentage = target.maxHp > 0
                ? target.hp / target.maxHp * 100 : 0;

            result += " " + target.Name + " is at " +
                      percentage.ToString("F2") + "% health.";
        }

        return result;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("\nPlayer: " + Name);
        Console.WriteLine($"Health: {hp:F2}/{maxHp:F2}");
        Console.WriteLine($"Energy: {energy:F2}/{maxEnergy:F2}");
        Console.WriteLine($"Armor: {armor:F2}");

        Console.Write("Learned skills: ");

        if (skillStatistics.Count == 0)
            Console.WriteLine("None");
        else
            Console.WriteLine(string.Join(", ",
                skillStatistics.Select(s => s.Name)));
    }
}

class Program
{
    static List<Player> players = new List<Player>();
    static List<Stats> skillsStatistics = new List<Stats>();

    static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    static double ReadNumber(string message)
    {
        double value;

        while (true)
        {
            Console.Write(message);

            if (double.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    static Player FindPlayer(string name)
    {
        return players.FirstOrDefault(p =>
            p.Name.Equals(name,
                StringComparison.OrdinalIgnoreCase));
    }

    static Stats FindSkill(string name)
    {
        return skillsStatistics.FirstOrDefault(s =>
            s.Name.Equals(name,
                StringComparison.OrdinalIgnoreCase));
    }

    static void AddPlayer()
    {
        string name = ReadText("Enter player name: ");

        if (FindPlayer(name) != null)
        {
            Console.WriteLine("Player already exists!");
            return;
        }

        double maxHealth = ReadNumber("Enter maximum health: ");
        double health = ReadNumber("Enter current health: ");
        double maxEnergy = ReadNumber("Enter maximum energy: ");
        double energy = ReadNumber("Enter current energy: ");
        double armor = ReadNumber("Enter armor (0-100): ");

        if (maxHealth < 0 || maxEnergy < 0 ||
            health < 0 || health > maxHealth ||
            energy < 0 || energy > maxEnergy ||
            armor < 0 || armor > 100)
        {
            Console.WriteLine("Invalid values!");
            return;
        }

        players.Add(new Player(name, health, maxHealth,
                               energy, maxEnergy, armor));

        Console.WriteLine("Player added successfully!");
    }

    static void AddSkill()
    {
        string name = ReadText("Enter skill name: ");

        if (FindSkill(name) != null)
        {
            Console.WriteLine("Skill already exists!");
            return;
        }

        double damage = ReadNumber("Enter damage: ");
        double penetration = ReadNumber("Enter penetration: ");
        double heal = ReadNumber("Enter heal amount: ");
        double cost = ReadNumber("Enter energy cost: ");
        string description = ReadText("Enter description: ");

        if (damage < 0 || penetration < 0 ||
            heal < 0 || cost < 0)
        {
            Console.WriteLine("Values cannot be negative!");
            return;
        }

        skillsStatistics.Add(new Stats(
            name, damage, penetration, heal, cost, description));

        Console.WriteLine("Skill statistics added successfully!");
    }

    static void DisplayPlayers()
    {
        if (players.Count == 0)
        {
            Console.WriteLine("No players available.");
            return;
        }

        foreach (Player player in players)
            player.DisplayInfo();
    }

    static void LearnSkill()
    {
        string playerName = ReadText("Enter player name: ");
        Player player = FindPlayer(playerName);

        if (player == null)
        {
            Console.WriteLine("Player not found!");
            return;
        }

        string skillName = ReadText("Enter skill name: ");
        Stats skill = FindSkill(skillName);

        if (skill == null)
        {
            Console.WriteLine("Skill not found!");
            return;
        }

        player.LearnSkill(skill);
    }

    static void Attack()
    {
        string attackerName =
            ReadText("Enter attacking player's name: ");

        Player attacker = FindPlayer(attackerName);

        if (attacker == null)
        {
            Console.WriteLine("Attacking player not found!");
            return;
        }

        string targetName = ReadText("Enter target player's name: ");
        Player target = FindPlayer(targetName);

        if (target == null)
        {
            Console.WriteLine("Target player not found!");
            return;
        }

        if (attacker == target)
        {
            Console.WriteLine("A player cannot attack themselves!");
            return;
        }

        List<Stats> learned = attacker.GetSkills();

        if (learned.Count == 0)
        {
            Console.WriteLine("This player has no learned skills!");
            return;
        }

        Console.WriteLine("Available skills:");

        foreach (Stats skill in learned)
        {
            Console.WriteLine(
                $"{skill.Name} | Damage: {skill.Damage} | " +
                $"Penetration: {skill.Penetration} | " +
                $"Heal: {skill.Heal} | Cost: {skill.Cost}");
        }

        string skillName = ReadText("Enter skill to use: ");

        Console.WriteLine(attacker.Attack(target, skillName));
    }

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n=== MAGICAL DUEL MENU ===");
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. Add Skill Statistics");
            Console.WriteLine("3. Display Player Info");
            Console.WriteLine("4. Learn a Skill");
            Console.WriteLine("5. Attack");
            Console.WriteLine("6. Exit");

            if (!int.TryParse(ReadText("Enter your choice: "),
                              out choice))
            {
                Console.WriteLine("Invalid choice!");
                choice = 0;
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddPlayer();
                    break;

                case 2:
                    AddSkill();
                    break;

                case 3:
                    DisplayPlayers();
                    break;

                case 4:
                    LearnSkill();
                    break;

                case 5:
                    Attack();
                    break;

                case 6:
                    Console.WriteLine("Exiting game...");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

        } while (choice != 6);
    }
}
