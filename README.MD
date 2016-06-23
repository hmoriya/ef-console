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

compatible error message
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

add tools section for "dotnet ef command"

```
"tools": {
    "Microsoft.EntityFrameworkCore.Tools": {
      "version": "1.0.0-preview1-*",
      "imports": [
        "dnxcore50",
        "portable-net45+win8"
      ]
    }
  }
```
finally [project.json]
```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0-rc2-*"
    },
    "Microsoft.EntityFrameworkCore.Sqlite": "1.0.0-rc2-*",
    "Microsoft.EntityFrameworkCore.Tools": {
      "version": "1.0.0-preview1-*",
      "type": "build"
    }
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": [
        "dnxcore50",
        "portable-net45+win8"
      ]
    }
  },
  "tools": {
    "Microsoft.EntityFrameworkCore.Tools": {
      "version": "1.0.0-preview1-*",
      "imports": [
        "dnxcore50",
        "portable-net45+win8"
      ]
    }
  }
}
```
## Create Model class

```
public class TodoDbContext : DbContext{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=./Todo.db");
    }
    public DbSet<ToDo> Todos { get; set; }
}
[Table("Todos")]
public class ToDo {
    public int Id { get; set; }
    public bool IsDone { get; set; }
    public string Text { get; set; }
}

```


## Dotnet EF command

```
dotnet ef migrations add "first schema"
dotnet ef database update
```

## Add Hello EF

```
public static void Main(string[] args)
{
    Console.WriteLine("Hello World!");

    using(var db = new TodoDbContext())
    {
        
        db.Todos.Add(new ToDo(){ IsDone = false,Text = "First Commit"});
        
        db.SaveChanges();

        foreach (var item in db.Todos)
        {
            Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
        }

    }

}
```

## Run dotnet

```
dotnet run
```

results

```
Project console-ef (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
Hello World!
[1] First Commit : False
```


## Run Unite Test

add dependency for unit test
```
"NUnit": "3.2.1",
"dotnet-test-nunit": "3.4.0-alpha-2"
```
add runner setting
```
  "testRunner": "nunit",
```

unit test run

```
dotnet test
```
