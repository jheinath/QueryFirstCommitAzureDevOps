# Query First Commit Azure DevOps

## What is this project for?
This projects helps you find the first commits of a user on a
Azure DevOps instance - searching through all repositories of all projects of all configured project collections.

All you have to do is follow the `How to use?` and provide the user email and the amount of first commits you want to query.

It is working with an on-premise Azure DevOps v6.0 Api.

## How to use? - As usual user
1. Click download and extract zip:
[Download](https://github.com/jheinath/QueryFirstCommitAzureDevOps/actions/runs/10972388692/artifacts/1961565100)
2. Generate a personal access token (PAT) in Azure DevOps (read on all repositories is enough) -
   If you  want to query multiple organizations select "all accessible organizations" in the PAT creation dialog. (Help: https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops&tabs=Windows#create-a-pat)
3. Enter PAT in `QueryFirstCommitAzureDevOps\Configuration\appsettings.json`
4. Also enter Azure DevOps Url and the project collections you want to query in the `appsettings.json`
5. execute `QueryFirstCommitAzureDevOps.exe` and follow the instructions.

## How to use? - For devs
1. Clone repository to your local machine
 ```bash
  git clone https://github.com/jheinath/QueryFirstCommitAzureDevOps
````
2. Generate a personal access token (PAT) in Azure DevOps (read on all repositories is enough) - 
If you  want to query multiple organizations select "all accessible organizations" in the PAT creation dialog. (Help: https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops&tabs=Windows#create-a-pat)
3. Enter PAT in `QueryFirstCommitAzureDevOps\Configuration\appsettings.json`
4. Also enter Azure DevOps Url and the project collections you want to query in the `appsettings.json`
5. execute `dotnet run` and follow the instructions.