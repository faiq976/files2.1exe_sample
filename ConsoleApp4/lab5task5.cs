
using System;
using System.Collections.Generic;

// ======================================================
// BUSINESS LOGIC: USER
// ======================================================

public class User
{
    private string user;
    private string password;
    private List<string> messages;

    public User(string user, string pass)
    {
        this.user = user;
        password = pass;
        messages = new List<string>();
    }

    public string GetUsername()
    {
        return user;
    }

    public string EncryptPassword(string str)
    {
        // Add # until the string length is divisible by 3.
        while (str.Length % 3 != 0)
        {
            str += "#";
        }

        int partLength = str.Length / 3;

        string firstPart = str.Substring(0, partLength);
        string secondPart = str.Substring(partLength, partLength);
        string thirdPart = str.Substring(partLength * 2, partLength);

        // Integer division gives the required middle position.
        int middle = password.Length / 2;

        return firstPart
             + password.Substring(0, middle)
             + secondPart
             + password.Substring(middle)
             + thirdPart;
    }

    public bool CheckPassword(string candidate)
    {
        return password == candidate;
    }

    public string GetPassword()
    {
        return new string('*', password.Length);
    }

    public void ChangePassword(string newPassword)
    {
        password = newPassword;
    }

    public void AddMessage(string message)
    {
        messages.Add(message);
    }

    public string GetMessage(int index)
    {
        if (index < 0 || index >= messages.Count)
        {
            return "invalid";
        }

        return messages[index];
    }

    public void DeleteMessage(int index)
    {
        if (index >= 0 && index < messages.Count)
        {
            messages.RemoveAt(index);
        }
    }

    public void DeleteAllMessages()
    {
        messages.Clear();
    }

    public List<string> GetAllMessages()
    {
        return new List<string>(messages);
    }

    public int GetMessageCount()
    {
        return messages.Count;
    }
}

// ======================================================
// BUSINESS LOGIC: PLATFORM-SPECIFIC USERS
// ======================================================

public class TwitterUser : User
{
    public TwitterUser(string user, string pass)
        : base(user, pass)
    {
    }
}

public class FacebookUser : User
{
    public FacebookUser(string user, string pass)
        : base(user, pass)
    {
    }
}

// ======================================================
// DATA LAYER: ALL USERS
// ======================================================

public static class UserDL
{
    private static List<User> users = new List<User>();

    public static void AddUser(User newUser)
    {
        users.Add(newUser);
    }

    public static User GetUserByUsername(string username)
    {
        foreach (User user in users)
        {
            if (user.GetUsername() == username)
            {
                return user;
            }
        }

        return null;
    }

    public static bool UsernameExists(string username)
    {
        return GetUserByUsername(username) != null;
    }
}

// ======================================================
// BUSINESS LOGIC: TWITTER
// ======================================================

public class Twitter
{
    private List<TwitterUser> users;

    public Twitter()
    {
        users = new List<TwitterUser>();
    }

    public void AddTwitterUser(TwitterUser user)
    {
        users.Add(user);
    }

    public TwitterUser GetTwitterUserByUsername(string name)
    {
        foreach (TwitterUser user in users)
        {
            if (user.GetUsername() == name)
            {
                return user;
            }
        }

        return null;
    }

    public List<string> GetTweets(TwitterUser user)
    {
        if (user == null ||
            GetTwitterUserByUsername(user.GetUsername()) == null)
        {
            return null;
        }

        return user.GetAllMessages();
    }
}

// ======================================================
// BUSINESS LOGIC: FACEBOOK
// ======================================================

public class Facebook
{
    private List<FacebookUser> users;

    public Facebook()
    {
        users = new List<FacebookUser>();
    }

    public void AddFacebookUser(FacebookUser user)
    {
        users.Add(user);
    }

    public FacebookUser GetFacebookUserByUsername(string name)
    {
        foreach (FacebookUser user in users)
        {
            if (user.GetUsername() == name)
            {
                return user;
            }
        }

        return null;
    }

    public List<string> GetPosts(FacebookUser user)
    {
        if (user == null ||
            GetFacebookUserByUsername(user.GetUsername()) == null)
        {
            return null;
        }

        return user.GetAllMessages();
    }
}

// ======================================================
// USER INTERFACE: USER OPERATIONS
// ======================================================

public class UserUI
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nUser Operations");
            Console.WriteLine("1. Create a new user");
            Console.WriteLine("2. Log in (existing user)");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateUser();
            }
            else if (choice == "2")
            {
                Login();
            }
            else if (choice == "3")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }

    private void CreateUser()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        if (UserDL.UsernameExists(username))
        {
            Console.WriteLine("Username already exists.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        User newUser = new User(username, password);
        UserDL.AddUser(newUser);

        Console.WriteLine("User created successfully.");
    }

    private void Login()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        User user = UserDL.GetUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.Write("Enter password: ");
        string candidate = Console.ReadLine();

        if (!user.CheckPassword(candidate))
        {
            Console.WriteLine("Incorrect password.");
            return;
        }

        Console.WriteLine("Valid User!");
        ShowUserMenu(user);
    }

    private void ShowUserMenu(User user)
    {
        while (true)
        {
            Console.WriteLine("\nValid User");
            Console.WriteLine("1. View Profile");
            Console.WriteLine("2. Encrypt Password");
            Console.WriteLine("3. Check Password");
            Console.WriteLine("4. Change Password");
            Console.WriteLine("5. Get Password");
            Console.WriteLine("6. Log Out");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Username: " + user.GetUsername());
                    Console.WriteLine("Password: " + user.GetPassword());
                    Console.WriteLine("Messages: " + user.GetMessageCount());
                    break;

                case "2":
                    Console.Write("Enter string to encrypt with password: ");
                    string str = Console.ReadLine();
                    Console.WriteLine("Encrypted password: "
                                      + user.EncryptPassword(str));
                    break;

                case "3":
                    Console.Write("Enter candidate password: ");
                    string candidate = Console.ReadLine();
                    Console.WriteLine(
                        user.CheckPassword(candidate)
                        ? "Password matches."
                        : "Password does not match.");
                    break;

                case "4":
                    Console.Write("Enter new password: ");
                    string newPassword = Console.ReadLine();
                    user.ChangePassword(newPassword);
                    Console.WriteLine("Password changed successfully.");
                    break;

                case "5":
                    Console.WriteLine("Password: " + user.GetPassword());
                    break;

                case "6":
                    Console.WriteLine("Logged out.");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

// ======================================================
// USER INTERFACE: TWITTER OPERATIONS
// ======================================================

public class TwitterUI
{
    private Twitter twitter;

    public TwitterUI(Twitter twitter)
    {
        this.twitter = twitter;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nTwitter Operations");
            Console.WriteLine("1. Add TwitterUser");
            Console.WriteLine("2. View Tweets for a Specific User");
            Console.WriteLine("3. Post Tweet for a Specific User");
            Console.WriteLine("4. Back to main menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser();
                    break;

                case "2":
                    ViewTweets();
                    break;

                case "3":
                    PostTweet();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void AddUser()
    {
        Console.Write("Enter Twitter username: ");
        string username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) ||
            UserDL.UsernameExists(username))
        {
            Console.WriteLine("Username is empty or already exists.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        TwitterUser user = new TwitterUser(username, password);

        twitter.AddTwitterUser(user);
        UserDL.AddUser(user);

        Console.WriteLine("Twitter user added successfully.");
    }

    private void ViewTweets()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        TwitterUser user = twitter.GetTwitterUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("Twitter user not found.");
            return;
        }

        List<string> tweets = twitter.GetTweets(user);

        if (tweets.Count == 0)
        {
            Console.WriteLine("No tweets available.");
            return;
        }

        for (int i = 0; i < tweets.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + tweets[i]);
        }
    }

    private void PostTweet()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        TwitterUser user = twitter.GetTwitterUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("Twitter user not found.");
            return;
        }

        Console.Write("Enter tweet: ");
        string tweet = Console.ReadLine();

        user.AddMessage(tweet);
        Console.WriteLine("Tweet posted successfully.");
    }
}

// ======================================================
// USER INTERFACE: FACEBOOK OPERATIONS
// ======================================================

public class FacebookUI
{
    private Facebook facebook;

    public FacebookUI(Facebook facebook)
    {
        this.facebook = facebook;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nFacebook Operations");
            Console.WriteLine("1. Add FacebookUser");
            Console.WriteLine("2. View Posts for a Specific User");
            Console.WriteLine("3. Add a Post for a Specific User");
            Console.WriteLine("4. Back to main menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser();
                    break;

                case "2":
                    ViewPosts();
                    break;

                case "3":
                    AddPost();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void AddUser()
    {
        Console.Write("Enter Facebook username: ");
        string username = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) ||
            UserDL.UsernameExists(username))
        {
            Console.WriteLine("Username is empty or already exists.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        FacebookUser user = new FacebookUser(username, password);

        facebook.AddFacebookUser(user);
        UserDL.AddUser(user);

        Console.WriteLine("Facebook user added successfully.");
    }

    private void ViewPosts()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        FacebookUser user =
            facebook.GetFacebookUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("Facebook user not found.");
            return;
        }

        List<string> posts = facebook.GetPosts(user);

        if (posts.Count == 0)
        {
            Console.WriteLine("No posts available.");
            return;
        }

        for (int i = 0; i < posts.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + posts[i]);
        }
    }

    private void AddPost()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        FacebookUser user =
            facebook.GetFacebookUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("Facebook user not found.");
            return;
        }

        Console.Write("Enter post: ");
        string post = Console.ReadLine();

        user.AddMessage(post);
        Console.WriteLine("Post added successfully.");
    }
}

// ======================================================
// DRIVER PROGRAM
// ======================================================

public class Program
{
    public static void Main()
    {
        // Required: create both platform objects at startup.
        Twitter twitter = new Twitter();
        Facebook facebook = new Facebook();

        UserUI userUI = new UserUI();
        TwitterUI twitterUI = new TwitterUI(twitter);
        FacebookUI facebookUI = new FacebookUI(facebook);

        while (true)
        {
            Console.WriteLine("\nWelcome to Social Media Simulator!");
            Console.WriteLine("1. User Operations");
            Console.WriteLine("2. Twitter Operations");
            Console.WriteLine("3. Facebook Operations");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    userUI.Run();
                    break;

                case "2":
                    twitterUI.Run();
                    break;

                case "3":
                    facebookUI.Run();
                    break;

                case "4":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
