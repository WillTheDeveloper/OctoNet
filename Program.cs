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

void ComingSoon()
{
    Console.WriteLine("Coming soon!");
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
            Console.Clear();
            Issues();
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
            Console.Clear();
            ListAllIssues();
            break;
        case "2":
            Console.Clear();
            ListAllIssuesForRepository();
            break;
        case "3":
            Console.Clear();
            GetIssue();
            break;
        case "4":
            Console.Clear();
            CreateIssue();
            break;
        case "5":
            Console.Clear();
            UpdateIssue();
            break;
        case "6":
            Console.Clear();
            DeleteIssue();
            break;
        case "7":
            Console.Clear();
            ListAllCommentsForIssue();
            break;
    }
}

async void ListAllIssues()
{
    var issues = await client.Issue.GetAllForCurrent();
    Console.WriteLine("Issues:");
    foreach (var issue in issues)
    {
        Console.WriteLine(issue.Title);
    }
}

async void ListAllIssuesForRepository()
{
    Console.WriteLine("Enter owner of repository (Leave blank if its your own):");
    var owner = Console.ReadLine();

    if (owner == "")
    {
        owner = client.User.Current().Result.Login;
        Console.WriteLine(owner);
    }
    
    Console.WriteLine("Enter the name of the repository:");
    var name = Console.ReadLine();
    
    var issues = await client.Issue.GetAllForRepository(owner, name);
    
    Console.WriteLine("Found " + issues.Count + " issues:");
}

async void GetIssue()
{
    
}

async void CreateIssue()
{
    Console.WriteLine("Enter the owner of the repository:");
    var owner = Console.ReadLine();
    Console.WriteLine("Enter the name of the repository:");
    var repository = Console.ReadLine();
    Console.WriteLine("Enter the title of the issue:");
    var title = Console.ReadLine();
    
    
    var createIssue = new NewIssue(title);
    
    Console.WriteLine("Add a label (leave blank if none):");
    var label = Console.ReadLine();
    if (label != "")
    {
        AddLabel(createIssue, label!);
    }

    await client.Issue.Create(owner, repository, createIssue);
}

void AddLabel(NewIssue issue, string label)
{
    issue.Labels.Add(label);
}

async void UpdateIssue()
{
    
}

async void DeleteIssue()
{
    
}

async void ListAllCommentsForIssue()
{
    
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
