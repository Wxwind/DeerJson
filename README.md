# Overview
A .Net library support JSON serialization and deserialization.

## Feature

- Support .NET7.0, .NET Standard 2.0, 2.1, .NET framework 4.8, so can be used in Unity.
- Support primitive types, string, class, array, Ilist<>, IDictionary<,> (Only string, primitive types and enum can be the key of dictionary),  auto property, enum (name or underlying value).
- Source code are clear and light, without any dependencies.

## Quick Start

```c#
// Serialize
var res = JsonMapper.Default.ToJson(obj);
var res = obj.ToJson(); // Equals JsonMapper.Default.ToJson

// Deserialize
var obj = JsonMapper.Default.ParseJson<Student>(json);
var obj = json.ParseJson<Student>() // Equals JsonMapper.Default.ParseJson

// or create your JsonMapper
var m_jsonMapper = new JsonMapper();
```



## Features

### Configure

All settings are false by default, and can be configured by following apis.

```c#
JsonMapper.Enable(JsonFeature f);
JsonMapper.Disable(JsonFeature f);
JsonMapper.Configure(JsonFeature f, bool enabled)
```

### Introduce

- **SERIALIZE_ORDER_BY_NAME**

  members will be ordered by name in final JSON.(Used in unit test)

- **SERIALIZE_UNDERLYING_TYPE_FOR_ENUM**

  ```
   public enum Days
   {
      Sunday = 0,
      Monday,
      Tuesday,
   }
  
   jsonMapper.Enable(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM);
  
   var obj = new DayObject(Days.Tuesday);
   var json = jsonMapper.ToJson(obj); // {"day":2}
   
   jsonMapper.Disable(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM);
   
   var obj = new DayObject(Days.Tuesday);
   var json = jsonMapper.ToJson(obj); // {"day":Tuesday}
  ```

- **DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES**

  Ignore unknown JSON properties while deserializing. Helpful when JSON has redundant properties.

- **DESERIALIZE_FAIL_ON_TRAILING_TOKENS**

## Attributes

- **[JsonIgnore] **

  Ignore specified auto properties or fields when serializing and deserializing.

  ```
  public class Student
  {
      [JsonIgnore] private string name;
      private int age;
      
      // Need non-parametric constructor for deserialize
      public Student(){}
      public Student(string name, int age){
      	this.name = name;
      	this.age = age;
      }
  }
  
  var s = new Student("a",1);
  var res = s.ToJson(); // {"age":1}
  ```

  
