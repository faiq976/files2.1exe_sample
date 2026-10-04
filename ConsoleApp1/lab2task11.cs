using System;

class Movie
{
    int Movieid;
    string Title;
    int Duration;

    public Movie(int Movieid, string Title, int Duration)
    {
        this.Movieid = Movieid;
        this.Title = Title;
        this.Duration = Duration;
    }

    public void Display()
    {
        Console.WriteLine("Movie ID: " + Movieid);
        Console.WriteLine("Title: " + Title);
        Console.WriteLine("Duration: " + Duration + " minutes");
        Console.WriteLine();
    }

    public int GetMovieid()
    {
        return Movieid;
    }

    static void Main(string[] args)
    {
        List<Movie> movies = new List<Movie>();

        Console.Write("Enter Movie ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Movie Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Movie Duration: ");
        int duration = Convert.ToInt32(Console.ReadLine());

        movies.Add(new Movie(Movieid, Title, Duration));

        Console.WriteLine("\nAll Movies:");

        foreach (Movie movie in movies)
        {
            movie.Display();
        }

        Console.Write("Enter Movie ID to search: ");
        int searchId = Convert.ToInt(Console.ReadLine());

        bool found = false;

        foreach (Movie movie in movies)
        {
            if (movie.GetMovieid() == searchId)
            {
                movie.Display();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Movie not found!");
        }
    }
}