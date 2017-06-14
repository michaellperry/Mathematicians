## Domain

Begin with the domain layer, which contains immutable business object. The first one to look at is `Mathematician`. Notice that its only properties are those that identify it:

```csharp
public class Mathematician
{
    public int MathematicianId { get; private set; }
    public Guid Unique { get; private set; }
}
```

To change a property of a `Mathematician` -- such as its name -- you must create a child object. See for example `MathematicianName`:

```csharp
public class MathematicianName
{
    public int MathematicianNameId { get; private set; }

    public int MathematicianId { get; private set; }
    public virtual Mathematician Mathematician { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}
```

This object records one version of the mathematician's name over time. You can get all versions of this property from the `Mathematician`:

```csharp
public class Mathematician
{
    public virtual ICollection<MathematicianName> Names { get; } =
        new List<MathematicianName>();
}
```

More likely, however, you are interested only in the current name. Each name keeps track of the prior versions that it replaces. On the opposite end, a name keeps track of the versions that replace it.

```csharp
public class MathematicianName
{
    public virtual ICollection<MathematicianName> Prior { get; } =
        new List<MathematicianName>();
    public virtual ICollection<MathematicianName> Next { get; } =
        new List<MathematicianName>();
}
```

The current names, therefore, are the names that do not yet have a next version.

```csharp
public class Mathematician
{
    public IEnumerable<MathematicianName> CurrentNames =>
        Names.Where(x => !x.Next.Any());
}
```

As you study these classes, pay attention to how to set a mathematician's name. How does it prevent collecting meaningless versions? How does it guarnatee idempotency? How does it recognize conflicts? How does it resolve them?