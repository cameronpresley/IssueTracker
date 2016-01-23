namespace IssueTracker.DataAccess
module UserLayer = 
    open FSharp.Data
    open IssueTracker.Core.Models

    type IUserDataAccess =
        abstract member CreateUser : User -> unit
        abstract member ReadUser : int -> User option
        abstract member UpdateUser : User -> unit
        abstract member DeleteUser : int -> unit
        abstract member ReadAllUsers : unit -> User list

    type UserDataAccess (connectionString:string) =

        [<Literal>]
        let createQuery = "INSERT INTO Users (Username, Password, Initials) VALUES (@username, @password, @initials)"
        let createUser (user:User) =
            let cmd = new SqlCommandProvider<createQuery, Connections.connectionString> (connectionString)
            cmd.Execute(username=user.Username, password=user.Password, initials=user.Initials) |> ignore

        [<Literal>]
        let readQuery = "SELECT * FROM Users where Id = @id"
        let readUser id =
            let cmd = new SqlCommandProvider<readQuery, Connections.connectionString>(connectionString)
            match cmd.Execute(id=id) |> Seq.tryHead with
            | None -> None
            | Some record -> new User(record.Id, record.Username, record.Password, record.Initials) |> Option.Some

        [<Literal>]
        let updateQuery = "Update Users SET Username=@username, Password=@password, Initials=@initials WHERE Id=@id"
        let updateUser (user:User) =
            let cmd = new SqlCommandProvider<updateQuery, Connections.connectionString>(connectionString)
            cmd.Execute(username=user.Username, password=user.Password, initials=user.Initials, id=user.Id) |> ignore

        [<Literal>]
        let deleteQuery = "DELETE FROM Users WHERE Id=@id"
        let deleteUser id =
            let cmd = new SqlCommandProvider<deleteQuery, Connections.connectionString>(connectionString)
            cmd.Execute(id=id) |> ignore

        [<Literal>]
        let readAllQuery = "SELECT * FROM Users"
        let readAllUsers () =
            let cmd = new SqlCommandProvider<readAllQuery, Connections.connectionString>(connectionString)
            match cmd.Execute () |> Seq.toList with
            | [] -> []
            | x -> x |> List.map(fun y -> new User(y.Id, y.Username, y.Password, y.Initials))
        
        member x.connectionString = connectionString
        new () = UserDataAccess(Connections.connectionString)

        interface IUserDataAccess with
            member x.CreateUser user = createUser user
            member x.ReadUser id = readUser id
            member x.UpdateUser user = updateUser user
            member x.DeleteUser id = deleteUser id
            member x.ReadAllUsers () = readAllUsers ()
        
        