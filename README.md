# Matrix Project

## Running the project:

To run the project, clone the repository and open **"EnterTheMatrixSolution.sln"** using Visual Studio.
You can run the project without choosing a specific file to start from.


## Project specifications:

You will create a GitHub repositories search page using their API.
1.The user will type the the repository he would like to search.

2.When searching(by pressing a button or using the Enter key) you will perform a request to:
[https://api.github.com/search/repositories?q=YOUR_SEARCH_KEYWORD]

3.Render the results as gallery items where each item will show to repository name, avater of the owner and a **bookmark button**.

4.When a user will bookmark a repository you will store the entire result to the user's session(Use [ASP.NET]session).

5.(Bonus) - Add a Bookmark screen that will show all the bookmarked repositories.

6.When you finish – upload your project to GitHub
Make sure you leave a Readme.md file so we can how to run the project.


## Classes:

`GithubResponse.cs`
 class holds the number of search results and the list of the repositories .

`GithubRepo.cs` 
class holds the information about the repository (id, name, owner)

`GithubUser.cs` 
class holds the relevant information about the user ( login, avatar_url, id, and a link to the profile)

`GithubServices.cs`
 class fetchs the information from api.github.com and returns the information as `GithubRepo`.
