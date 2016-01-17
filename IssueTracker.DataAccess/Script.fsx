// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "..\\IssueTracker.Core\\Models.fs"
#reference @".\bin\Debug\FSharp.Data.SqlClient.dll"
#load ".\\Connections.fs"
#load ".\\IssueLayer.fs"
#load ".\\UserLayer.fs"
open IssueTracker.Core.Models
open IssueTracker.DataAccess.IssueLayer
open IssueTracker.DataAccess.UserLayer

// Define your library scripting code here

