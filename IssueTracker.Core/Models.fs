namespace IssueTracker.Core
module Models =
    type User (id:int, username:string, password:string, initials:string) =
        member x.Id = id
        member x.Username = username
        member x.Password = password
        member x.Initials = initials
