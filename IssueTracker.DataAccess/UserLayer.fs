namespace IssueTracker.DataAccess
module UserLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models
    open Connections
    
    type UserDataAccess () =
        [<Literal>]
        let createQuery = "INSERT INTO Users (Username, Password, Initials) VALUES (@username, @password, @initials)"
        [<Literal>]
        let readQuery = "SELECT * FROM Users where Id = @id"
        [<Literal>]
        let updateQuery = "Update Users SET Username=@username, Password=@password, Initials=@initials WHERE Id=@id"
        [<Literal>]
        let deleteQuery = "DELETE FROM Users WHERE Id=@id"
        [<Literal>]
        let readAllQuery = "SELECT * FROM Users"

        member x.createUser user =
            let cmd = new SqlCommandProvider<createQuery, connectionString> ()
            cmd.Execute(username=user.username, password=user.password, initials=user.initials)

        member x.readUser id =
            let cmd = new SqlCommandProvider<readQuery, connectionString>()
            match cmd.Execute(id=id) |> Seq.tryHead with
            | None -> None
            | Some record -> Some {id=record.Id; password=record.Password; username=record.Username; initials=record.Initials}

        member x.updateUser user =
            let cmd = new SqlCommandProvider<updateQuery, connectionString>()
            cmd.Execute(username=user.username, password=user.password, initials=user.initials, id=user.id)

        member x.deleteUser id =
            let cmd = new SqlCommandProvider<deleteQuery, connectionString>()
            cmd.Execute(id=id)

        member x.readAllUsers () =
            let cmd = new SqlCommandProvider<readAllQuery, connectionString>()
            match cmd.Execute () |> Seq.toList with
            | [] -> []
            | x -> x |> List.map(fun y -> {username=y.Username; password=y.Password; initials=y.Initials; id=y.Id})