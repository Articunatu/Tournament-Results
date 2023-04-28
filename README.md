# Tournament Results

## Intro
This project was created as a Blazor Server App instead of a Blazor Web Assembly because the structure of a Web Assembly mirrors how my other hobby project is composed: https://github.com/Articunatu/Social-Media

## Technical Overview

### Server
* Framework & Language - .NET 6 / C#
* Database - Microsoft SQL Server
* Micro-ORM - Dapper, used to call queries and procedures from database
* Object Mapping - Automapper
* Automation Tests - MS Test
### Client
* Framework - Blazor Server App
* Preprocessor - 
* Style Utility - Bootstrap
* Component Library - "SyncFusion"

## Technical Details

### Database Diagram
### Displaying the ATP Ranking
The view model for displaying the data for the ATP Ranking needs to make 3 separate database calls.
As much as possible of the data filtering and searching should be done in the backend (DataAccessors and Controllers) while the code in the razor files should solely be for changing the UI and making initial data calls.
This view was also made into a component to keep resusibility for multiple rankings.
