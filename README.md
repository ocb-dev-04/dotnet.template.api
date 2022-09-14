### Migrations

```cs
dotnet ef migrations add v1.0.0_Migration --context MainDbContext --startup-project API --project Data -o Migrations
```

### Update database

```cs
dotnet ef database update --context MainDbContext --startup-project API --project Data
```