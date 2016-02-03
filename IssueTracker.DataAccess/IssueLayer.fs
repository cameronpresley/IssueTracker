namespace IssueTracker.DataAccess
module IssueLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models

    type IIssueDataAccess =
        abstract member CreateIssue : Issue -> unit
        abstract member ReadIssue : int -> Issue option
        abstract member UpdateIssue : Issue -> unit
        abstract member DeleteIssue : int -> unit
        abstract member ReadAllIssues : unit -> Issue list

    type IssueDataAccess (connectionString:string) =

        let convertStatusToString (status:Status) = sprintf "%A" status
        let convertStringToStatus input =
            match input with
            | "ToDo" -> Some ToDo
            | "InProgress" -> Some InProgress
            | "Done" -> Some Done
            | _ -> None

        [<Literal>]
        let createQuery = "INSERT INTO Issues (Title, Description, Status) VALUES (@title, @description, @status)"
        let createIssue(issue:Issue) =
            let cmd = new SqlCommandProvider<createQuery, Connections.connectionString> (connectionString)
            cmd.Execute(title=issue.Title, description=issue.Description, status=convertStatusToString issue.Status) |> ignore

        [<Literal>]
        let readQuery = "SELECT * FROM Issues where Id = @id"
        let readIssue id =
            let cmd = new SqlCommandProvider<readQuery, Connections.connectionString>(connectionString)
            match cmd.Execute(id=id) |> Seq.tryHead with
            | None -> None
            | Some record ->
                let status = convertStringToStatus record.Status
                match status with
                | Some status -> new Issue(record.Id, record.Title, record.Description, status) |> Option.Some
                | _ -> None

        [<Literal>]
        let updateQuery = "Update Issues SET Title=@title, Description=@description, Status=@status WHERE Id=@id"
        let updateIssue (issue:Issue) =
            let cmd = new SqlCommandProvider<updateQuery, Connections.connectionString>(connectionString)
            cmd.Execute(title=issue.Title, description=issue.Description, status=convertStatusToString issue.Status, id=issue.Id) |> ignore

        [<Literal>]
        let deleteQuery = "DELETE FROM Issues WHERE Id=@id"
        let deleteIssue id =
            let cmd = new SqlCommandProvider<deleteQuery, Connections.connectionString>(connectionString)
            cmd.Execute(id=id) |> ignore

        [<Literal>]
        let readAllQuery = "SELECT * FROM Issues"
        let readAllIssues () =
            let cmd = new SqlCommandProvider<readAllQuery, Connections.connectionString>(connectionString)
            match cmd.Execute () |> Seq.toList with
                | [] -> []
                | x -> x |> List.map(fun y -> new Issue(y.Id, y.Title, y.Description, (convertStringToStatus y.Status).Value))
        
        member x.connectionString = connectionString
        new () = IssueDataAccess(Connections.connectionString)

        interface IIssueDataAccess with
            member x.CreateIssue issue = createIssue issue
            member x.ReadIssue id = readIssue id
            member x.UpdateIssue issue = updateIssue issue
            member x.DeleteIssue id = deleteIssue id
            member x.ReadAllIssues () = readAllIssues ()
        

