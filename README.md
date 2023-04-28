# Tournament Results

## Intro
This project was created as a Blazor Server App instead of a Blazor Web Assembly because the structure of a Web Assembly mirrors how my other hobby project is composed: https://github.com/Articunatu/Social-Media


## Features
You can view every tournament a player has participated in, and the result - in shape of a placing or points - for each attendance.
Similarly you can view each player that particpated in a specific tournament, and each result.

The main feature is the full ranking table, where you can see each tournamnet for a selected period of time as the columns, and every player - that participated in at least one of those tournaments - as the rows. The final column will contain a sum of all the points a player gathered through these tournaments. <br/>
With this function you can filter be years to see which competitors were ranked in the top for that one year. <br/>
Or you could see what the rankings are right now, checking all the tournaments that have ran from one year ago up until now. 


## Technical Overview

* Framework & Language - Blazor Server App / .NET 6 / C#
* Database - Microsoft SQL Server
* Cloud - Azure (both app and database will be published there)
* Micro-ORM - Dapper, used to call queries and procedures from database
* Object Mapping - Automapper
* Automation Tests - MS Test
* Style Utility - Bootstrap
* Component Library - "SyncFusion"


## Technical Details

### Database Diagram
There is an API for a specific eSports on Start.gg, which indentifies its tables with GUIDs as primary keys.

### Displaying the ATP Ranking
The view model for displaying the data for the ATP Ranking needs to make 3 separate database calls.
As much as possible of the data filtering and searching should be done in the backend (DataAccessors and Controllers) while the code in the razor files should solely be for changing the UI and making initial data calls.
This view was also made into a component to keep resusibility for multiple rankings.
