using DeerJson.Deserializer;
using DeerJson.Deserializer.std;
using DeerJson.Tests.Type;

namespace DeerJson.Tests;

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
                        while (!p.HasToken(TokenType.RBRACKET))
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