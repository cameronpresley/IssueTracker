namespace IssueTracker.DataAccess
module UserLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models
    open Connections
    type IUserDataAccess =
        abstract member CreateUser : User -> unit
        abstract member ReadUser : int -> User option
        abstract member UpdateUser : User -> unit
        abstract member DeleteUser : int -> unit
        abstract member ReadAllUsers : unit -> User list

    type UserDataAccess () =
        
        [<Literal>]
        let createQuery = "INSERT INTO Users (Username, Password, Initials) VALUES (@username, @password, @initials)"
        let createUser user =
            let cmd = new SqlCommandProvider<createQuery, connectionString> ()
            cmd.Execute(username=user.username, password=user.password, initials=user.initials) |> ignore

        [<Literal>]
        let readQuery = "SELECT * FROM Users where Id = @id"
        let readUser id =
            let cmd = new SqlCommandProvider<readQuery, connectionString>()
            match cmd.Execute(id=id) |> Seq.tryHead with
            | None -> None
            | Some record -> Some {id=record.Id; password=record.Password; username=record.Username; initials=record.Initials}

        [<Literal>]
        let updateQuery = "Update Users SET Username=@username, Password=@password, Initials=@initials WHERE Id=@id"
        let updateUser user =
            let cmd = new SqlCommandProvider<updateQuery, connectionString>()
            cmd.Execute(username=user.username, password=user.password, initials=user.initials, id=user.id) |> ignore

        [<Literal>]
        let deleteQuery = "DELETE FROM Users WHERE Id=@id"
        let deleteUser id =
            let cmd = new SqlCommandProvider<deleteQuery, connectionString>()
            cmd.Execute(id=id) |> ignore

        [<Literal>]
        let readAllQuery = "SELECT * FROM Users"
        let readAllUsers () =
            let cmd = new SqlCommandProvider<readAllQuery, connectionString>()
            match cmd.Execute () |> Seq.toList with
            | [] -> []
            | x -> x |> List.map(fun y -> {username=y.Username; password=y.Password; initials=y.Initials; id=y.Id})
        
        interface IUserDataAccess with
            member x.CreateUser user = createUser user
            member x.ReadUser id = readUser id
            member x.UpdateUser user = updateUser user
            member x.DeleteUser id = deleteUser id
            member x.ReadAllUsers () = readAllUsers ()
        
        