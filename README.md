# opt
An example class to hold onto "global" options for a C# application

This uses C# attributes and reflection to mark properties as options to be
added from a passed dictionary.

Applications for this can be as simple as a console utility to a simulator that
needs a static set of options to influence the decision tree.

Supported types are Bool, Enum and String
Each Option has a "Name" field, which will be used in the dictionary lookup
Each type can be declared like so:
```csharp
[BoolOption("BoolOption1Name")]
public bool BoolFeature1 { get; private set; }

[StringOption("StringOption1Name")]
public string StringFeature1 { get; private set; }

[EnumOption("EnumOption1Name", typeof(EnumType), (int)EnumType.DefaultValue)]
public EnumType EnumFeature1 { get; private set; }

```
Default values can be added to each type
```csharp
///<summary>
/// If the option is not found in the defining dictionary, BoolFeature1 will
/// be initialized to true
///</summary>
[BoolOption("BoolOption1Name", true)]
public bool BoolFeature1 { get; private set; }

///<summary>
/// A simple Enumeration Option
///</summary>
enum EnumType
{
    Value1,
    Value2,
    Value3
}

///<summary>
/// If the option is not found in the defining dictionary, EnumFeature1 will be
/// initialized to EnumType.Value2
///</summary>
[EnumOption("EnumOption1Name", typeof(EnumType), (int)EnumType.Value2)]
public EnumType EnumFeature1 { get; private set; }

///<summary>
/// If the option is not found in the defining dictionary, StringFeature1 will
/// be initialized to "MyDefaultString"
///</summary>
[StringOption("StringFeature1Name", "MyDefaultString")]
public string StringFeature1 { get; private set; }

```


To Do:
- [x] Add Simple Unit Tests
- [ ] Add more robust Unit Tests
- [ ] Add Comment documentation to the code
- [ ] Update Readme.md
