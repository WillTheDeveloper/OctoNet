using Octokit;

var token = "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";

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
    Console.WriteLine("6. Users");
    
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
        case "api":
            Console.Clear();
            apiStatus();
            break;
    }
}

void Users(User authenticatedUser)
{
    Console.WriteLine("What action would you like to do?");
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
    }
}

void GetUser()
{
    Console.WriteLine("Enter the username of the user you want to get");
    var username = Console.ReadLine();
    var user = client.User.Get(username).Result;
    Console.WriteLine(user.Login);
}

void GetAuthenticatedUser(User authenticatedUser)
{
    Console.WriteLine(authenticatedUser.Login);
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
            var old1 = authenticatedUser.Name;
            var name = UpdateName();
            var user1 = client.User.Update(name).Result;
            Console.WriteLine("Changed name from " + old1 + " to " + user1.Name);
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
    }
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