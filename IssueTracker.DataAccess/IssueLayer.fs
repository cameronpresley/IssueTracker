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

    let private convertToPriority input =
        match input with
        | "ShowStopper" -> Some ShowStopper
        | "Critical" -> Some Critical
        | "Annoying" -> Some Annoying
        | "Trivial" -> Some Trivial
        | _ -> None

    [<Literal>]
    let private readQuery = "SELECT * FROM Issues where Id = @id"
    let readIssue id =
        let cmd = new SqlCommandProvider<readQuery, connectionString>()
        match cmd.Execute(id=id) |> Seq.tryHead with
        | None -> None
        | Some record ->
            let newPriority = convertToPriority record.Priority
            match newPriority with
            | None -> None
            | Some priority -> Some {id=record.Id; title=record.Title; description=record.Description; priority=priority}

    [<Literal>]
    let private updateQuery = "Update Issues SET Title=@title, Description=@description, Priority=@priority WHERE Id=@id"
    let updateIssue issue =
        let cmd = new SqlCommandProvider<updateQuery, connectionString>()
        cmd.Execute(title=issue.title, description=issue.description, priority=(issue.priority |> sprintf("%A")), id=issue.id)