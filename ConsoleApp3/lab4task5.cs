
using System;
using System.Collections.Generic;

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
    private string name;
    private double hp, maxHp;
    private double energy, maxEnergy;
    private double armor;

    private List<Stats> skillStatistics = new List<Stats>();

    public Player(string name, double health, double energy, double armor)
    {
        this.name = name;
        hp = maxHp = Math.Max(0, health);
        this.energy = maxEnergy = Math.Max(0, energy);
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
        name = value;
    }

    public void LearnSkill(Stats skill)
    {
        skillStatistics.Add(skill);
    }

    public string Attack(Player target)
    {
        if (skillStatistics.Count == 0)
            return name + " has not learned any skills!";

        Stats skill = skillStatistics[0];

        if (energy < skill.Cost)
        {
            return name + " attempted to use " + skill.Name +
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

        string result = name + " used " + skill.Name + ", " +
                        skill.Description + ", against " +
                        target.name + ", doing " +
                        damage.ToString("F2") + " damage!";

        if (skill.Heal > 0)
            result += " " + name + " healed for " +
                      actualHeal.ToString("F2") + " health!";

        if (target.hp <= 0)
        {
            result += " " + target.name + " died.";
        }
        else
        {
            double percentage = target.maxHp > 0
                ? target.hp / target.maxHp * 100 : 0;

            result += " " + target.name + " is at " +
                      percentage.ToString("F2") + "% health.";
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Player alice = new Player("Alice", 110, 50, 10);
        Player bob = new Player("Bob", 100, 60, 20);

        Stats fireball = new Stats(
            "fireball", 23, 1.2, 5, 15,
            "a fiery magical attack");

        Stats superbeam = new Stats(
            "superbeam", 200, 50, 50, 75,
            "an overpowered attack, please nerf");

        alice.LearnSkill(fireball);
        bob.LearnSkill(superbeam);

        Console.WriteLine(alice.Attack(bob));
        Console.WriteLine(bob.Attack(alice));
    }
}
