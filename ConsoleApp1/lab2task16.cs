using System;

class Enemy
{
    public char DisplayCharacter;
    public double X;
    public double Y;
    public Enemy(char DisplayCharacter, double X, double Y)
    {
        this.DisplayCharacter = DisplayCharacter;
        this.X = X;
        this.Y = Y;
    }
    public void Move(double dx, double dy)
    {
        X = X + dx;
        Y = Y + dy;
    }
}
class Player
{
    public char DisplayCharacter;
    public double X;
    public double Y;
    public Player(char DisplayCharacter, double X, double Y)
    {
        this.DisplayCharacter = DisplayCharacter;
        this.X = X;
        this.Y = Y;
    }
    static void Main(string[] args)
    {
        Player player = new Player('@', 5, 5);

        Enemy enemy1 = new Enemy('E', 10, 10);
        Enemy enemy2 = new Enemy('E', 20, 20);
        Enemy enemy3 = new Enemy('E', 30, 30);
        enemy1.Move(5, 0);
        enemy2.Move(0, 10);
        enemy3.Move(-5, 5);
        Console.WriteLine("Player:" + player.DisplayCharacter + "X=" + player.X + "Y=" + player.Y);
        Console.WriteLine("Enemy 1:" + enemy1.DisplayCharacter + "X=" + enemy1.X + "Y=" + enemy1.Y);
        Console.WriteLine("Enemy 2:" + enemy2.DisplayCharacter + "X=" + enemy2.X + "Y=" + enemy2.Y);
        Console.WriteLine("Enemy 3:" + enemy3.DisplayCharacter + "X=" + enemy3.X + "Y=" + enemy3.Y);
    }
}