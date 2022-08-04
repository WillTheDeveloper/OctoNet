﻿using Octokit;

var token = "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"; // ADD YOUR PERSONAL ACCESS TOKEN HERE

var client = new GitHubClient(new ProductHeaderValue("OctoNet")); // Required header when accessing API

if (token != "") // Some actions require tokens and auth
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Found a token, using it");
    Console.ResetColor();
    var tokenAuth = new Credentials(token);
    client.Credentials = tokenAuth;
    var user = await client.User.Current();
    Authenticated(user);
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("No token found, using anonymous");
    Console.ResetColor();
    Support();
}

void Support() // Give any assistance to users using the app
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("Would you like help setting up authentication?");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Yes / No");
    Console.ResetColor();
    var a = Console.ReadLine();

    if (a == "Yes")
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Do you have a GitHub account?");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Yes / No");
        Console.ResetColor();
        var b = Console.ReadLine();
        if (b == "Yes")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Follow these steps to authenticate yourself:");
            Console.WriteLine("1. Open Github in your browser.");
            Console.WriteLine("2. Login if you have not already done so.");
        }
        else if (b == "No")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Create an account and then run this again.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Your input selector was not recognised.");
            Console.ResetColor();
            Support();
        }
    }
    else if (a == "No")
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Okay. Please note that without auth, API requests will be throttled a lot.");
        Console.ResetColor();
        Guest();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Your input selector was not recognised.");
        Console.ResetColor();
        Support();
    }
}

void ComingSoon()
{
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("Coming soon!");
    Console.ResetColor();
}

void Authenticated(User user) // Main menu
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Authenticated as " + user.Login + " (" + user.Id + ")");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("Please select an category:");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("1. Issues");
    Console.WriteLine("2. Labels");
    Console.WriteLine("3. Milestones");
    Console.WriteLine("4. Releases");
    Console.WriteLine("5. Repositories");
    Console.WriteLine("6. Users");
    
    Console.ResetColor();
    
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
        case "5":
            Console.Clear();
            Repositories();
            break;
        case "6":
            Console.Clear();
            Users(user);
            break;
        case "api": // Hidden but might add to main options later
            Console.Clear();
            apiStatus();
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            Authenticated(user);
            break;
    }
}

void Users(User authenticatedUser)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("What action would you like to do?");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("1. Get a user");
    Console.WriteLine("2. Get authenticated user");
    Console.WriteLine("3. Update authenticated user");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            GetUser();
            break;
        case "2":
            Console.Clear();
            GetAuthenticatedUser(authenticatedUser);
            break;
        case "3":
            Console.Clear();
            UpdateAuthenticatedUser(authenticatedUser);
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            Users(authenticatedUser);
            break;
    }
}

void GetUser()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("Enter the username of the user you want to get");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    var username = Console.ReadLine();
    var user = client.User.Get(username).Result;
    Console.WriteLine(user.Login);
}

void GetAuthenticatedUser(User authenticatedUser)
{
    Console.WriteLine(authenticatedUser.Login);
}

void DestructableAction(User user)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Are you sure you want to do this action?");
    Console.WriteLine("Yes / No");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "Yes":
            Console.WriteLine("As a secondary safety measure, please answer the following question:");
            if (user.Name != "")
            {
                Console.WriteLine("Please enter your name (Found on your profile)");
                var input = Console.ReadLine();

                while (input != user.Name)
                {
                    Console.Clear();
                    Console.WriteLine("Please try again.");
                    input = Console.ReadLine();
                }
            }
            Console.ResetColor();
            break;
        case "No":
            Console.ResetColor();
            return;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            DestructableAction(user);
            break;
    }
}

void UpdateAuthenticatedUser(User authenticatedUser)
{
    Console.WriteLine("What would you like to update?");
    Console.WriteLine("Clear. Clear a field");
    Console.WriteLine("1. Name");
    Console.WriteLine("2. Email");
    Console.WriteLine("3. Blog");
    Console.WriteLine("4. Company");
    Console.WriteLine("5. Location");
    Console.WriteLine("6. Hire-able");
    Console.WriteLine("7. Bio");
    
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "Clear" or "clear":
            Console.Clear();
            ClearUserField();
            break;
            
        case "1":
            Console.Clear();
            DestructableAction(authenticatedUser);
            var old1 = authenticatedUser.Name;
            var name = UpdateName();
            var user1 = client.User.Update(name).Result;
            Console.WriteLine("Changed name from " + old1 + " to " + user1.Name);
            break;
        case "2":
            Console.Clear();
            DestructableAction(authenticatedUser);
            var old2 = authenticatedUser.Email;
            var email = UpdateEmail();
            var user2 = client.User.Update(email).Result;
            Console.WriteLine("Changed email from " + old2 + " to " + user2.Email);
            break;
        case "3":
            Console.Clear();
            var old3 = authenticatedUser.Blog;
            var blog = UpdateBlog();
            var user3 = client.User.Update(blog).Result;
            Console.WriteLine("Changed blog from " + old3 + " to " + user3.Blog);
            break;
        case "4":
            Console.Clear();
            var old4 = authenticatedUser.Company;
            var company = UpdateCompany();
            var user4 = client.User.Update(company).Result;
            Console.WriteLine("Changed company from " + old4 + " to " + user4.Company);
            break;
        case "5":
            Console.Clear();
            var old5 = authenticatedUser.Location;
            var location = UpdateLocation();
            var user5 = client.User.Update(location).Result;
            Console.WriteLine("Changed location from " + old5 + " to " + user5.Location);
            break;
        case "6":
            Console.Clear();
            var old6 = authenticatedUser.Hireable;
            var hireable = UpdateHireable();
            var user6 = client.User.Update(hireable).Result;
            Console.WriteLine("Changed hireable from " + old6 + " to " + user6.Hireable);
            break;
        case "7":
            Console.Clear();
            var old7 = authenticatedUser.Bio;
            var bio = UpdateBio();
            var user7 = client.User.Update(bio).Result;
            Console.WriteLine("Bio has been updated from:");
            Console.WriteLine(old7);
            Console.WriteLine("And updated to:");
            Console.WriteLine(user7);
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            UpdateAuthenticatedUser(authenticatedUser);
            break;
    }
}

void ClearUserField()
{
    Console.WriteLine("Which field would you like to clear?");
    Console.WriteLine("1. Name");
    Console.WriteLine("2. Email");
    Console.WriteLine("3. Blog");
    Console.WriteLine("4. Company");
    Console.WriteLine("5. Location");
    Console.WriteLine("6. Hire-able");
    Console.WriteLine("7. Bio");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var a = new UserUpdate
            {
                Name = string.Empty
            };
            var aa = client.User.Update(a).Result;
            Console.WriteLine("Name has been cleared");
            break;
        case "2":
            var b = new UserUpdate
            {
                Email = string.Empty
            };
            var bb = client.User.Update(b).Result;
            Console.WriteLine("Email has been cleared");
            break;
        case "3":
            var c = new UserUpdate
            {
                Blog = string.Empty
            };
            var cc = client.User.Update(c).Result;
            Console.WriteLine("Blog has been cleared");
            break;
        case "4":
            var d = new UserUpdate
            {
                Company = string.Empty
            };
            var dd = client.User.Update(d).Result;
            Console.WriteLine("Company has been cleared");
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            ClearUserField();
            break;
    }
}

UserUpdate UpdateCompany()
{
    Console.WriteLine("Enter a new company");
    var c = Console.ReadLine();

    var company = new UserUpdate
    {
        Company = c
    };

    return company;
}

UserUpdate UpdateBio()
{
    Console.WriteLine("Enter a new bio");
    var b = Console.ReadLine();

    var bio = new UserUpdate
    {
        Bio = b
    };

    return bio;
}

UserUpdate UpdateBlog()
{
    Console.WriteLine("Enter a new blog");
    var b = Console.ReadLine();

    var blog = new UserUpdate
    {
        Blog = b
    };

    return blog;
}

UserUpdate UpdateName()
{
    Console.WriteLine("Enter the new name");
    var n = Console.ReadLine();

    var name = new UserUpdate
    {
        Name = n
    };
    
    return name;
}

UserUpdate UpdateLocation()
{
    Console.WriteLine("Enter a new location");
    var l = Console.ReadLine();

    var location = new UserUpdate
    {
        Location = l
    };

    return location;
}

UserUpdate UpdateEmail()
{
    Console.WriteLine("Enter a new email");
    var e = Console.ReadLine();

    var email = new UserUpdate
    {
        Email = e
    };

    return email;
}

UserUpdate UpdateHireable()
{
    Console.WriteLine("Enter a new hireable (true or false)");
    var h = Console.ReadLine();

    var hire = false;
    
    if (h is "true" or "True")
    {
        hire = true;
    }
    else if (h is "false" or "False")
    {
        hire = false;
    }
    else
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("That option is not recognised.");
        Console.ResetColor();
        UpdateHireable();
    }

    var hireable = new UserUpdate
    {
        Hireable = hire
    };

    return hireable;
}

void Repositories()
{
    Console.WriteLine("Select a repository:");
    var repositories = client!.Repository.GetAllForCurrent().Result;
    int i = 0;
    foreach (var repository in repositories)
    {
        i++;
        Console.WriteLine(i + ": " + repository.Name);
    }
    
    //Select a repository from the list
    Console.WriteLine("Select a repository by its associated number:");
    var repositoryChoice = Console.ReadLine();
    Console.Clear();
    var information = repositories[int.Parse(repositoryChoice!) - 1];
    
    //Get information about the selected repository
    Console.WriteLine("Selected " + information.Name);
    
    Console.WriteLine("What would you like to do with this repository?");
    
    Console.WriteLine("1. List commits");
    Console.WriteLine("2. More details");
    Console.WriteLine("3. ");
    Console.WriteLine("4. Statistics");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            ListCommits(information);
            break;
        case "2":
            Console.Clear();
            RepositoryDetails(information);
            break;
        case "4":
            Console.Clear();
            RepositoryStatistics(information);
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            Repositories();
            break;
    }
}

void RepositoryStatistics(Repository repository)
{
    ComingSoon();
    // var stats = client.Repository.Statistics.GetCommitActivity(repository.Owner.Login, repository.FullName).Result;
    // Console.WriteLine("Commit activity for " + repository.Name);
    // Console.WriteLine("Total commits: " + stats.Activity.Sum(x => x.Total));
    // Console.WriteLine("Commits per day:");
    // foreach (var day in stats.Activity)
    // {
    //     Console.WriteLine(day.Total + " on " + day.Days);
    // }
}

void RepositoryDetails(Repository repository)
{
    Console.WriteLine(repository.Id);
    Console.WriteLine("Name: " + repository.Name);
    Console.WriteLine("Description: " + repository.Description);
    Console.WriteLine("Created: " + repository.CreatedAt);
}

void ListCommits(Repository repository)
{
    Console.WriteLine("Commits for " + repository.Name);

    ComingSoon();
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
        case "3":
            Console.Clear();
            CreateRelease();
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            Releases();
            break;
    }
}

void GetAllReleases()
{
    Console.WriteLine("Enter the repository owner's name:");
    var owner = Console.ReadLine();
    Console.WriteLine("Enter the repository name:");
    var repo = Console.ReadLine();
    var releases = client.Repository.Release.GetAll(owner, repo).Result;
    var latest = releases.First();
    Console.WriteLine("Latest tag is " + latest.TagName);
    Console.WriteLine("Latest release is " + latest.Name);
}

void CreateRelease()
{
    Console.WriteLine("Enter the owner name of the repository:");
    var owner = Console.ReadLine();
    Console.WriteLine("Enter the name of the repository:");
    var repo = Console.ReadLine();
    Console.Clear();
    Console.WriteLine("Enter the tag name of the release:");
    var tag = Console.ReadLine();
    Console.WriteLine("Enter the name of the release:");
    var name = Console.ReadLine();
    Console.WriteLine("Enter the body of the release:");
    var body = Console.ReadLine();
    Console.Clear();
    Console.WriteLine("Enter the draft status of the release (true or false):");
    var draft = Console.ReadLine();
    Console.WriteLine("Enter the prerelease status of the release (true or false):");
    var prerelease = Console.ReadLine();
    
    bool isDraft = false;
    bool isPrerelease = false;

    switch (draft)
    {
        case "true":
            isDraft = true;
            break;
        case "false":
            isDraft = false;
            break;
        default:
            Console.WriteLine("Invalid draft status");
            Console.Clear();
            CreateRelease();
            break;
    }

    switch (prerelease)
    {
        case "true":
            isPrerelease = true;
            break;
        case "false":
            isPrerelease = false;
            break;
        default:
            Console.WriteLine("Invalid prerelease status");
            Console.Clear();
            CreateRelease();
            break;
    }

    var newRelease = new NewRelease(tag);
    newRelease.Name = name;
    newRelease.Body = body;
    newRelease.Draft = isDraft;
    newRelease.Prerelease = isPrerelease;
    var release = client.Repository.Release.Create(owner, repo, newRelease).Result;
    
    Console.WriteLine("Created released id " + release.Id);
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
    Console.WriteLine("8. Lock an issue");
    Console.WriteLine("9. Unlock an issue");
    
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
        case "8":
            Console.Clear();
            LockIssue();
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That option is not recognised.");
            Console.ResetColor();
            Issues();
            break;
    }
}

void LockIssue()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("This command will only work if you have push access on the repository!");

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("What is the name of the owner for the repository?");
    Console.ForegroundColor = ConsoleColor.White;
    var owner = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("What is the name of the repository?");
    Console.ForegroundColor = ConsoleColor.White;
    var repo = Console.ReadLine();
    
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("What is the issue number?");
    var identifier = Console.ReadLine();

    //Parse identifier into an integer
    int issueNumber = 0;
    if (!int.TryParse(identifier, out issueNumber))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid issue number");
        Console.ResetColor();
        Console.Clear();
        LockIssue();
    }
    

    var issue = client!.Issue.Lock(owner, repo, issueNumber);
}

void ListAllIssues()
{
    var issues = client.Issue.GetAllForCurrent().Result;
    Console.WriteLine("Issues:");
    foreach (var issue in issues)
    {
        Console.WriteLine(issue.Title);
    }
}

void ListAllIssuesForRepository()
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
    
    var issues = client.Issue.GetAllForRepository(owner, name).Result;
    
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

void apiStatus()
{
    var miscellaneousRateLimit = client!.Miscellaneous.GetRateLimits().Result;

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

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Core API rate limit:");
    Console.WriteLine("Rate limit: " + howManyCoreRequestsCanIMakePerHour + " requests per hour");
    Console.WriteLine("Rate limit: " + howManyCoreRequestsDoIHaveLeft + " requests left");
    Console.WriteLine("Rate limit: " + whenDoesTheCoreLimitReset + " UTC time");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Search API rate limit:");
    Console.WriteLine("Rate limit: " + howManySearchRequestsCanIMakePerMinute + " requests per minute");
    Console.WriteLine("Rate limit: " + howManySearchRequestsDoIHaveLeft + " requests left");
    Console.WriteLine("Rate limit: " + whenDoesTheSearchLimitReset + " UTC time");
}