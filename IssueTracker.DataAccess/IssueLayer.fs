namespace IssueTracker.DataAccess
module IssueLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models

    [<Literal>]
    let connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=IssueTracker.Database;Integrated Security=True"

    [<Literal>]
    let private createQuery = "INSERT INTO Issues (Title, Description, Priority) VALUES (@Title, @Description, @Priority)"

    let createIssue issue =
        let cmd = new SqlCommandProvider<createQuery, connectionString> ()
        cmd.Execute(Title=issue.title, Description=issue.description, Priority=(issue.priority |> sprintf("%A")))
