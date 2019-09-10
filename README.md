# Design Patterns in C# code
It will contain common designs Patterns that I've implemented or used.

# Setup
## Database
This project is using SQLite and liquibase.
Install liquibase-3.5.5 in `C:\opt\liquibase` and add it to the Path environment variable. [LINK](https://download.liquibase.org/download-community/)
Then Run:
```
> cd Design.Patterns.WebApi/sqlite-database/
> liquibase update
```

# Useful commands
`dotnet new webapi -n Design.Patterns.WebApi`

