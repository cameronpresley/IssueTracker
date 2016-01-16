namespace IssueTracker.Core
module Models =
    type User = {username:string; password:string; initials:string }

    type Priority = ShowStopper | Critical | Annoying | Trivial
    type Issue = {id:int; title:string; description:string; priority:Priority}

