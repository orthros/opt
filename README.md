opt
======

# About
An example module to hold onto "global" options for a C# application

This uses C# attributes and reflection to mark properties as options to be
added from a passed dictionary.

Applications for this can be as simple as a console utility to a simulator that
needs a static set of options to influence the decision tree.

Supported types are Bool, Enum and String.
Each Option has a "Name" field, which will be used in the dictionary lookup

#Examples

### Supported Types
Each type can be declared like so:
```csharp
[BoolOption("BoolOption1Name")]
public bool BoolFeature1 { get; private set; }

[StringOption("StringOption1Name")]
public string StringFeature1 { get; private set; }

[EnumOption("EnumOption1Name", typeof(EnumType), (int)EnumType.DefaultValue)]
public EnumType EnumFeature1 { get; private set; }

```
### Default Values
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
### Code Example
Consider the following class:
```csharp
class MyOptionsClass : OptionSet
{
    enum FillType
    {
        Spiral,
        BottomUp,
        TopDown
    }
    [StringOption("DelimiterValue", "|")]
    public string Delimiter { get; private set; }

    [BoolOption("TurnOnLights", true)]
    public bool LightsOn { get; private set; }

    [EnumOption("FillStyle", typeof(FillType), (int)FillType.TopDown)]
    public EnumType FillMethod { get; private set; }
}
```
And consider the following Dictionary:

| Key            | Value     |
| -------------- | --------- |
| DelimiterValue | "&"       |
| FillStyle      | "TopDown" |

When passed to the MyOptionsClass constructor, its properties will be

* Delimiter  => "&"
* LightsOn   => true
* FillMethod => TopDown

Because the dictionary did not contain the TurnOnLights key, LightsOn assumes
its default value of true, and the class will log that it did not find a match
for the TurnOnLights property.

If the dictionary has an unknown key as in the following example:

| Key       | Value     |
| --------- | --------- |
| **Deli**  | "&"       |
| FillStyle | "TopDown" |

The constructor will log that there was an unrecognized option "Deli" and
the Delimiter property of MyOptionsClass will be initialized to "|"

# Contributing
Pull requests are welcome!
Please make sure they are decently formatted with some documentation.

# Future Plans
To Do:
- [x] Add Simple Unit Tests
- [ ] Add more robust Unit Tests
- [x] Add Comment documentation to the code
- [x] Update Readme.md
- [ ] Make the Class isomorphic (i.e. can create a Dictionary<string,string> of the options from the OptionSet object
