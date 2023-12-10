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
var jsonMapper = new JsonMapper();
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

  Members will be ordered by name in final JSON.(Used in unit test)

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

- **DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES**

## Attributes

- **[JsonIgnore]**

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

## Custom serializer and deserializer

- **Serializer**

  1. Make your own Serializer class extends from JsonSerializer and implement Serialize(). Here is an example.

     ```csharp
     public class SimpleNestedObjectSerializer : JsonSerializer<SimpleNestedObject>
     {
         public override void Serialize(SimpleNestedObject value, JsonGenerator gen, SerializeContext ctx)
         {
             gen.WriteObjectStart();
     
             gen.WriteMemberName("str2");
             gen.WriteString(value.str);
     
             gen.WriteMemberName("arr2");
             gen.WriteArrayStart();
             gen.WriteNumber(1);
             gen.WriteNumber(2);
             gen.WriteNumber(3);
             gen.WriteArrayEnd();
     
             gen.WriteObjectEnd();
         }
     }
     ```

  2. Register in jsonMapper.

     ```csharp
     m_jsonMapper.AddSerializer(new SimpleNestedObjectSerializer());
     ```

- **Deserializer**
  1. Make your own Deserializer class extends from JsonDeserializer and implement Deserialize(). Here is an example.

     ```csharp
     public class SimpleNestedObjectDeserializer : JsonDeserializer<SimpleNestedObject>
     {
         public override SimpleNestedObject GetNullValue(DeserializeContext ctx)
         {
             return null;
         }
     
         public override SimpleNestedObject Deserialize(JsonParser p, DeserializeContext ctx)
         {
             p.GetObjectStart();
             var o = new SimpleNestedObject();
             
             string name;
     
             while ((name = p.GetMemberName()) != null)
             {
                 switch (name)
                 {
                     case "str":
                         o.str = "hi";
                         p.SkipMemberValue();
                         break;
                     case "numArr":
                         if (p.HasToken(TokenType.NULL))
                         {
                             p.GetNull();
                             o.subObj.subNum = 0;
                         }
                         else
                         {
                             var array = new List<int>();
                             p.GetArrayStart();
                             while (p.GetNextToken() != TokenType.RBRACKET)
                             {
                                 var el = Convert.ToInt32(p.GetNumber());
                                 array.Add(el);
                             }
     
                             p.GetArrayEnd();
                             o.numArr = array;
                         }
                         break;
                     case "subObj":
                         p.GetObjectStart();
                         while ((name = p.GetMemberName()) != null)
                         {
                             switch (name)
                             {
                                 case "subNum":
                                     if (p.HasToken(TokenType.NULL))
                                     {
                                         p.GetNull();
                                         o.subObj.subNum = 0;
                                     }
                                     else o.subObj.subNum = Convert.ToInt32(p.GetNumber());
                                     break;
                                 case "isObj":
                                     o.subObj.isObj = p.GetBool();
                                     break;
                             }
                         }
     
                         p.GetObjectEnd();
                         break;
                 }
             }
     
             p.GetObjectEnd();
             return o;
         }
     }
     ```
  
  2. Register in jsonMapper.
  
     ```csharp
     m_jsonMapper.AddDeserializer(new SimpleNestedObjectDeserializer());
     ```