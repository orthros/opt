# opt
An example class to hold onto "global" options for a C# application

This uses C# attributes to mark properties as options to be added from a passed
dictionary.

Applications for this can be as simple as a console utility to a simulator that
needs a static set of options to influence the decision tree.

An example of each supported type (Bool, Enum and String) can be declared like so:
```
[BoolOption("BoolOption1Name")]
public bool BoolFeature1 { get; private set; }

[StringOption("StringOption1Name")]
public string StringFeature1 { get; private set; }

[EnumOption("EnumOption1Name",typeof(EnumType), (int)EnumType.DefaultValue)]
public EnumType EnumFeature1 { get; private set; }

```
Default values can be added to each type
```
///<summary>
/// If the option is not found, in the defining dictionary, BoolFeature1 will
/// be initialized to true
///</summary>
[BoolOption("BoolOption1Name", true)]
public bool BoolFeature1 { get; private set; }
```


To Do:
- [x] Add Simple Unit Tests
- [ ] Add more robust Unit Tests
- [ ] Add Comment documentation to the code
- [ ] Update Readme.md 
