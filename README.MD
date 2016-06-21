# Hello EF Core

## Create dotnet core
```
dotnet new
dotnet restore
dotnet run
```

## Change Dependecy [ project.json ]

```
"dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0-rc2-*"
    },
    "Microsoft.EntityFrameworkCore.Sqlite": "1.0.0-rc2-*",
    "Microsoft.EntityFrameworkCore.Tools": {
    "version": "1.0.0-preview1-*",
    "type":"build"
    }
  }
```

ompatible error message
```
Errors in /Users/hmoriya/Develop/github/hmoriya/console-ef/project.json
    Package Ix-Async 1.2.5 is not compatible with netcoreapp1.0 (.NETCoreApp,Version=v1.0). Package Ix-Async 1.2.5 supports:
      - net40 (.NETFramework,Version=v4.0)
      - net45 (.NETFramework,Version=v4.5)
      - portable-net45+win8+wp8 (.NETPortable,Version=v0.0,Profile=Profile78)
    Package Remotion.Linq 2.0.2 is not compatible with netcoreapp1.0 (.NETCoreApp,Version=v1.0). Package Remotion.Linq 2.0.2 supports:
      - net35 (.NETFramework,Version=v3.5)
      - net40 (.NETFramework,Version=v4.0)
      - net45 (.NETFramework,Version=v4.5)
      - portable-net45+win8+wp8+wpa81 (.NETPortable,Version=v0.0,Profile=Profile259)
    One or more packages are incompatible with .NETCoreApp,Version=v1.0.
```

update frmework section

```
"frameworks": {
    "netcoreapp1.0": {
      "imports": [
        "dnxcore50",
        "portable-net45+win8"
      ]
    }
  }
```

