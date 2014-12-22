## Project Description ##

Adds a fluent API over the existing Ektron Framework API for configuring the most common criteria objects.

```csharp
var criteria = new ContentCriteria()
  .FilteredBy(ContentProperty.Title).NotIn("Blah")
  .Recursive()
  .WithMetadata()
  .MaxItems()
```
