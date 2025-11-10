
### EF Figure out the relationship [1:* - One To Many] from Different Ways based from Entities Declaration:

1. Create List or ICollection form Children Entities under Parent Entity Only, There is no need any details under Child Entity

Note: Automatic EF define Foreign key called `AuthorId`

```csharp
namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new List<Book>();
    }
    
    // ...

    public List<Book> Books { get; set; }
}
```

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;
}
```

2. Create List or ICollection form Children Entities under Parent Entity Plus define Proptery reference to Parent Entity under Child Entity - Called **Navigation property**

```csharp
namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new List<Book>();
    }
    
    // ...

    public List<Book> Books { get; set; }
}
```

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;

    // ...

    public Author? Author { get; set; } = null;
}
```


3. Create List or ICollection form Children Entities under Parent Entity Plus define Proptery as Foreign Key with Parent Entity Name plus Id - called **Foreign Key Property** 

```csharp
namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new List<Book>();
    }
    
    // ...

    public List<Book> Books { get; set; }
}
```

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;

    public int AuthorId { get; set; } // Author + Id => Automatic define as FK
}
```


4. You can Combine the both Options from Points #2 and #3, this will be more flixable to define FK Foreign Key

```csharp
namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new List<Book>();
    }
    
    // ...

    public List<Book> Books { get; set; }
}
```

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;

    public Author? Author { get; set; } = null;

    public int AuthorId { get; set; } // Author + Id => Automatic define as FK
}
```


5. You Define The relation outside Entities Class Through Fluent API and Both Entities has no relattion between them - Called **Shadow Property**

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    // ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
                    .HasMany<Book>()
                    .WithOne(); // Has Many Book with One Author => 1:* One To Many relation
        // ...
    }
}
```

### Whats Different between Shadow property, Navigation property, Property Foreign

***AI Answer***

The core difference among **Shadow Properties**, **Navigation Properties**, and **Foreign Key (FK) Properties** in Entity Framework (EF) Core lies in their **purpose** and their **presence in the .NET entity class**.

Here is a breakdown of each one:

#### 👻 Shadow Property

* **Definition:** A property that is **not defined** in your .NET entity class but is part of the EF Core model and mapped to a database column.

* **Purpose:** To store data that is necessary for the database but you don't want to expose directly on the entity class for a cleaner domain model (e.g., audit fields like `CreatedDate` or, most commonly, the actual foreign key column).

* **Accessibility:** You **cannot** access a shadow property directly via the entity instance (e.g., `post.BlogId`). You must use the `ChangeTracker` API or the static `EF.Property` method in LINQ queries.

* **Example:** If your `Post` class only has a `Blog` navigation property, EF Core will **automatically create a shadow property** named `BlogId` to be the foreign key column in the database.

#### 🔗 Navigation Property

* **Definition:** A property defined on your .NET entity class that holds a **reference** to a related entity or a collection of related entities.

* **Purpose:** To provide an **object-oriented way** to navigate and manage relationships between entities in your code, abstracting away the underlying foreign key values.

* **Accessibility:** You access it directly like any other property (e.g., `post.Blog` or `blog.Posts`).

* **Mapping:** Behind the scenes, the navigation property is always backed by a **foreign key** in the database (which can be an explicit FK property or a shadow property).

* **Example:**
    * **Reference Navigation (the "one" side):** `public Blog Blog { get; set; }` on the `Post` entity.
    * **Collection Navigation (the "many" side):** `public ICollection<Post> Posts { get; set; }` on the `Blog` entity.

#### 🔑 Foreign Key Property (FK Property)

* **Definition:** A property explicitly defined on your .NET entity class that holds the **actual value** of the foreign key (the primary key of the related entity).

* **Purpose:** To directly represent the foreign key column from the database in your entity class. This is often used for:
    1.  **Direct manipulation** of the relationship by ID without loading the related entity.
    2.  **Explicitly defining** the foreign key name/type if conventions aren't sufficient.

* **Accessibility:** Accessed directly (e.g., `post.BlogId`).

* **Mapping:** Mapped directly to the foreign key column in the database.

* **Example:** `public int BlogId { get; set; }` on the `Post` entity.

#### Summary Table

| Feature | Shadow Property | Navigation Property | Foreign Key Property |
| :--- | :--- | :--- | :--- |
| **In .NET Class?** | ❌ **No** (Only in the EF Model) | ✅ **Yes** | ✅ **Yes** |
| **Data Type** | A primitive type (e.g., `int`, `string`, `DateTime`) | An entity type (`Blog`) or a collection (`ICollection<Post>`) | A primitive type (matching the principal key type) |
| **Core Role** | Stores relational or meta-data **values** invisibly. | Provides **object reference** for graph traversal. | Stores the relational **ID value** explicitly. |
| **Access in Code** | Via `ChangeTracker` or `EF.Property<T>()` | Direct property access (`entity.NavProp`) | Direct property access (`entity.FKId`) |
| **Typical Use** | Audit fields, or *hiding* the FK value from the entity. | Loading and working with related objects. | Direct assignment/querying by ID for performance. |

The key takeaway is that the **Foreign Key** is the **data mechanism** (the ID value) for the relationship, the **Navigation Property** is the **object mechanism** (the reference to the object) for the relationship, and a **Shadow Property** is a way to use a column (like an FK) **without exposing it** on your C# class.

