using Octokit;

var token = "";

var client = new GitHubClient(new ProductHeaderValue("OctoNet"));

if (token != "")
{
    Console.WriteLine("Found a token, using it");
    var tokenAuth = new Credentials(token);
    client.Credentials = tokenAuth;
    var user = await client.User.Current();
    Authenticated(user);
}
else
{
    Console.WriteLine("No token found, using anonymous");
    Guest();
}

void Authenticated(User user)
{
    Console.WriteLine("Authenticated as " + user.Login);
    Console.WriteLine("Please select an category:");
    Console.WriteLine("1. Issues");
    Console.WriteLine("2. Labels");
    Console.WriteLine("3. Milestones");
    Console.WriteLine("4. Releases");
    Console.WriteLine("5. Repositories");
    
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            
            break;
    }
}

void Issues()
{
    Console.WriteLine("Please select an issue:");
    Console.WriteLine("1. List all issues");
    Console.WriteLine("2. List all issues for a repository");
    Console.WriteLine("3. Get an issue");
    Console.WriteLine("4. Create an issue");
    Console.WriteLine("5. Update an issue");
    Console.WriteLine("6. Delete an issue");
    Console.WriteLine("7. List all comments for an issue");
    
    var choice = Console.ReadLine();
    
    switch (choice)
    {
        case "1":
            ListAllIssues();
            break;
        case "2":
            // ListAllIssuesForRepository();
            break;
        case "3":
            // GetIssue();
            break;
        case "4":
            // CreateIssue();
            break;
        case "5":
            // UpdateIssue();
            break;
        case "6":
            // DeleteIssue();
            break;
        case "7":
            // ListAllCommentsForIssue();
            break;
    }
}

async void ListAllIssues()
{
    var issues = await client!.Issue.GetAllForCurrent();
    Console.WriteLine("Issues:");
    foreach (var issue in issues)
    {
        Console.WriteLine(issue.Title);
    }
}

void Labels()
{
    Console.WriteLine("Please select a label:");
    Console.WriteLine("1. List all labels");
    Console.WriteLine("2. List all labels for a repository");
    Console.WriteLine("3. Get a label");
    Console.WriteLine("4. Create a label");
    Console.WriteLine("5. Update a label");
    Console.WriteLine("6. Delete a label");
}

void Guest()
{
    Console.WriteLine("Accessing as guest");
}
