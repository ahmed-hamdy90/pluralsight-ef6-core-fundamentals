### For Reverse Engineering the exists Database

When We have Database already includes details about Table and Migrations files, we need to create all available Entity Classes which represent Exists Tables

- We need use EF CLI Command, So will auto-create DbContext and Entities classes

```SHELL
dotnet ef dbcontext scaffold "{Connection-string}"
```
