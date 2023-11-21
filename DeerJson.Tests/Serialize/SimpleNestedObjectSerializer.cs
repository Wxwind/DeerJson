using DeerJson.Serializer;
using DeerJson.Tests.Type;

namespace DeerJson.Tests;

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