﻿using Octokit;

var token = "";

var client = new GitHubClient(new ProductHeaderValue("OctoNet"));

if (token != "")
{
    Console.Clear();
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
        case "4":
            Console.Clear();
            Releases();
            break;
        case "api":
            Console.Clear();
            apiStatus();
            break;
    }
}

void Releases()
{
    Console.WriteLine("Please select an action:");
    Console.WriteLine("1. Get all releases");
    Console.WriteLine("2. Get a release");
    Console.WriteLine("3. Create a release");
    Console.WriteLine("4. Update a release");
    Console.WriteLine("5. Delete a release");
    Console.WriteLine("6. Get a release's assets");
    Console.WriteLine("7. Get a release's assets by ID");
    Console.WriteLine("8. Upload a release asset");
    Console.WriteLine("9. Delete a release asset");
    Console.WriteLine("10. Get a release's assets by ID");
    
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            GetAllReleases();
            break;
    }
}

async void GetAllReleases()
{
    Console.WriteLine("Enter the repository owner's name:");
    var owner = Console.ReadLine();
    Console.WriteLine("Enter the repository name:");
    var repo = Console.ReadLine();
    var releases = await client.Repository.Release.GetAll(owner, repo);
    var latest = releases.First();
    Console.WriteLine("Latest tag is " + latest.TagName);
    Console.WriteLine("Latest release is " + latest.Name);
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

async void apiStatus()
{
    var miscellaneousRateLimit = await client!.Miscellaneous.GetRateLimits();

//  The "core" object provides your rate limit status except for the Search API.
    var coreRateLimit = miscellaneousRateLimit.Resources.Core;

    var howManyCoreRequestsCanIMakePerHour = coreRateLimit.Limit;
    var howManyCoreRequestsDoIHaveLeft = coreRateLimit.Remaining;
    var whenDoesTheCoreLimitReset = coreRateLimit.Reset; // UTC time

// the "search" object provides your rate limit status for the Search API.
    var searchRateLimit = miscellaneousRateLimit.Resources.Search;

    var howManySearchRequestsCanIMakePerMinute = searchRateLimit.Limit;
    var howManySearchRequestsDoIHaveLeft = searchRateLimit.Remaining;
    var whenDoesTheSearchLimitReset = searchRateLimit.Reset; // UTC time
    
    Console.WriteLine("Rate limit: " + howManyCoreRequestsCanIMakePerHour + " requests per hour");
    Console.WriteLine("Rate limit: " + howManyCoreRequestsDoIHaveLeft + " requests left");
    Console.WriteLine("Rate limit: " + whenDoesTheCoreLimitReset + " UTC time");
    
    Console.WriteLine("Rate limit: " + howManySearchRequestsCanIMakePerMinute + " requests per minute");
    Console.WriteLine("Rate limit: " + howManySearchRequestsDoIHaveLeft + " requests left");
    Console.WriteLine("Rate limit: " + whenDoesTheSearchLimitReset + " UTC time");
}