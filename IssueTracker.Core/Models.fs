namespace IssueTracker.Core
module Models =
    type User (id:int, username:string, password:string, initials:string) =
        member x.Id = id
        member x.Username = username
        member x.Password = password
        member x.Initials = initials

    type Status = ToDo | InProgress | Done

    type Issue (id:int, title:string, description:string, status:Status) =
        member x.Id = id
        member x.Title = title
        member x.Description = description
        member x.Status = status