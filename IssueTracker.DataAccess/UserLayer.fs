namespace IssueTracker.DataAccess
module UserLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models
    open Connections
    
    [<Literal>]
    let private createQuery = "INSERT INTO Users (Username, Password, Initials) VALUES (@username, @password, @initials)"

    let createUser user =
        let cmd = new SqlCommandProvider<createQuery, connectionString> ()
        cmd.Execute(username=user.username, password=user.password, initials=user.initials)

    [<Literal>]
    let private readQuery = "SELECT * FROM Users where Id = @id"
    let readUser id =
        let cmd = new SqlCommandProvider<readQuery, connectionString>()
        match cmd.Execute(id=id) |> Seq.tryHead with
        | None -> None
        | Some record -> Some {id=record.Id; password=record.Password; username=record.Username; initials=record.Initials}

    [<Literal>]
    let private updateQuery = "Update Users SET Username=@username, Password=@password, Initials=@initials WHERE Id=@id"
    let updateUser user =
        let cmd = new SqlCommandProvider<updateQuery, connectionString>()
        cmd.Execute(username=user.username, password=user.password, initials=user.initials, id=user.id)

    [<Literal>]
    let private deleteQuery = "DELETE FROM Users WHERE Id=@id"
    let deleteUser id =
        let cmd = new SqlCommandProvider<deleteQuery, connectionString>()
        cmd.Execute(id=id)