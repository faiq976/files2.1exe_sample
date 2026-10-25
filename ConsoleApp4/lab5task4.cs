
using System;

// ======================== POINT ========================
class Point
{
    public int x { get; set; }
    public int y { get; set; }

    public Point()
    {
        x = 0;
        y = 0;
    }

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

// ======================== BOUNDARY ========================
class Boundary
{
    public Point TopLeft { get; set; }
    public Point TopRight { get; set; }
    public Point BottomLeft { get; set; }
    public Point BottomRight { get; set; }

    public Boundary()
    {
        TopLeft = new Point(0, 0);
        TopRight = new Point(0, 90);
        BottomLeft = new Point(90, 0);
        BottomRight = new Point(90, 90);
    }

    public Boundary(Point topLeft, Point topRight,
                    Point bottomLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
    }
}

// ======================== GAME OBJECT ========================
class GameObject
{
    public char[,] Shape { get; set; }
    public Point StartingPoint { get; set; }
    public Boundary Premises { get; set; }
    public string Direction { get; set; }

    private bool patrolMovingLeft = true;
    private int projectileStage = 0;
    private int projectileSteps = 0;
    private bool projectileFinished = false;

    // Default constructor
    public GameObject()
    {
        Shape = new char[,] { { '-', '-', '-' } };
        StartingPoint = new Point(0, 0);
        Premises = new Boundary();
        Direction = "LeftToRight";
    }

    // Constructor with shape and starting point
    public GameObject(char[,] shape, Point startingPoint)
    {
        Shape = shape;
        StartingPoint = startingPoint;
        Premises = new Boundary();
        Direction = "LeftToRight";
    }

    // Constructor with all four attributes
    public GameObject(char[,] shape, Point startingPoint,
                      Boundary premises, string direction)
    {
        Shape = shape;
        StartingPoint = startingPoint;
        Premises = premises;
        Direction = direction;
    }

    // Draw the shape at the current location
    public void Draw()
    {
        for (int r = 0; r < Shape.GetLength(0); r++)
        {
            for (int c = 0; c < Shape.GetLength(1); c++)
            {
                int row = StartingPoint.x + r;
                int col = StartingPoint.y + c;

                if (row >= 0 && row < Console.WindowHeight - 1 &&
                    col >= 0 && col < Console.WindowWidth - 1)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(Shape[r, c]);
                }
            }
        }

        Console.SetCursorPosition(0, Math.Max(0,
            Console.WindowHeight - 2));
    }

    // Erase the shape from its current location
    public void Erase()
    {
        for (int r = 0; r < Shape.GetLength(0); r++)
        {
            for (int c = 0; c < Shape.GetLength(1); c++)
            {
                int row = StartingPoint.x + r;
                int col = StartingPoint.y + c;

                if (row >= 0 && row < Console.WindowHeight - 1 &&
                    col >= 0 && col < Console.WindowWidth - 1)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(' ');
                }
            }
        }

        Console.SetCursorPosition(0, Math.Max(0,
            Console.WindowHeight - 2));
    }

    // Move the object according to its direction
    public void Move()
    {
        int shapeWidth = Shape.GetLength(1);
        int shapeHeight = Shape.GetLength(0);

        int left = Premises.TopLeft.y;
        int right = Premises.TopRight.y - shapeWidth + 1;
        int top = Premises.TopLeft.x;
        int bottom = Premises.BottomLeft.x - shapeHeight + 1;

        switch (Direction)
        {
            case "LeftToRight":
                if (StartingPoint.y < right)
                    StartingPoint.y++;
                break;

            case "RightToLeft":
                if (StartingPoint.y > left)
                    StartingPoint.y--;
                break;

            case "Patrol":
                if (patrolMovingLeft)
                {
                    if (StartingPoint.y > left)
                        StartingPoint.y--;
                    else
                        patrolMovingLeft = false;
                }
                else
                {
                    if (StartingPoint.y < right)
                        StartingPoint.y++;
                    else
                        patrolMovingLeft = true;
                }
                break;

            case "Diagonal":
                if (StartingPoint.y < right &&
                    StartingPoint.x < bottom)
                {
                    StartingPoint.x++;
                    StartingPoint.y++;
                }
                break;

            case "Projectile":
                if (projectileFinished)
                    break;

                if (projectileStage == 0)
                {
                    // Five steps toward top-right
                    StartingPoint.x--;
                    StartingPoint.y++;
                    projectileSteps++;

                    if (projectileSteps == 5)
                    {
                        projectileStage = 1;
                        projectileSteps = 0;
                    }
                }
                else if (projectileStage == 1)
                {
                    // Two steps to the right
                    StartingPoint.y++;
                    projectileSteps++;

                    if (projectileSteps == 2)
                    {
                        projectileStage = 2;
                        projectileSteps = 0;
                    }
                }
                else if (projectileStage == 2)
                {
                    // Four steps toward bottom-right
                    StartingPoint.x++;
                    StartingPoint.y++;
                    projectileSteps++;

                    if (projectileSteps == 4)
                    {
                        projectileFinished = true;
                    }
                }
                break;
        }
    }
}

// ======================== DRIVER CODE ========================
class Program
{
    static void Main()
    {
        GameObject obj = new GameObject();

        Console.WriteLine("GameObject demo");
        Console.WriteLine("Press any key to move; Esc to exit.");

        while (true)
        {
            Console.Clear();
            obj.Draw();

            if (Console.KeyAvailable &&
                Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                break;
            }

            obj.Erase();
            obj.Move();
            obj.Draw();

            System.Threading.Thread.Sleep(100);
        }

        Console.Clear();
    }
}
