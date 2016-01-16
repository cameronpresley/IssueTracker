// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "..\\IssueTracker.Core\\Models.fs"
#reference @".\bin\Debug\FSharp.Data.SqlClient.dll"
#load ".\\IssueLayer.fs"
open IssueTracker.Core.Models
open IssueTracker.DataAccess.IssueLayer

// Define your library scripting code here

