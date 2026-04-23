using System;
using ArtStudioApp;

var path = @"C:\Users\COLLEGE\RiderProjects\ArtStudio\database.db";
var db = new DbContext($"Data Source={path};");

db.DisplayAllProjectsInfo();