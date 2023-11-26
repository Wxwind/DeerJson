using System;
using System.IO;

namespace DeerJson
{
    public class JsonGenerator : IDisposable
    {
        private readonly TextWriter       m_textWriter = new StringWriter();
        private          GeneratorContext m_genContext = new GeneratorContext();


        private void WriteRaw(string v)
        {
            m_textWriter.Write(v);
        }

        public void WriteObjectStart()
        {
            VerifyValueWrite("start object");
            m_genContext = m_genContext.NewObjectContext();
            WriteRaw("{");
        }

        // Write fields and auto props' name.
        public void WriteMemberName(string name)
        {
            if (!m_genContext.InObject())
            {
                throw new JsonException("can not write field name in non object context.");
            }

            var status = m_genContext.OnBeforeWriteMemberName();

            if (status == GeneratorContext.Status.EXPECT_VALUE)
            {
                throw new JsonException("can not write duplicate name");
            }

            InternalWriteMemberName(name, status == GeneratorContext.Status.OK_NEED_COMMA);
        }

        private void InternalWriteMemberName(string name, bool commaBefore)
        {
            if (commaBefore)
            {
                WriteRaw(",");
            }

            WriteRaw("\"");
            WriteRaw(name);
            WriteRaw("\"");
        }

        public void WriteObjectEnd()
        {
            if (!m_genContext.InObject())
            {
                throw new JsonException($"Current context is not in Object but in {m_genContext.CurContextType}");
            }

            WriteRaw("}");
            m_genContext = m_genContext.GetParentContext();
        }

        public void WriteArrayStart()
        {
            VerifyValueWrite("start array");
            m_genContext = m_genContext.NewArrayContext();
            WriteRaw("[");
        }

        public void WriteArrayEnd()
        {
            if (!m_genContext.InArray())
            {
                throw new JsonException($"Current context is not in Array but in {m_genContext.CurContextType}");
            }

            WriteRaw("]");
            m_genContext = m_genContext.GetParentContext();
        }

        public void WriteNumber(short value)
        {
            VerifyValueWrite("write short");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(ushort value)
        {
            VerifyValueWrite("write ushort");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(int value)
        {
            VerifyValueWrite("write int");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(uint value)
        {
            VerifyValueWrite("write uint");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(long value)
        {
            VerifyValueWrite("write long");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(ulong value)
        {
            VerifyValueWrite("write ulong");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(float value)
        {
            VerifyValueWrite("write double");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(double value)
        {
            VerifyValueWrite("write double");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(decimal value)
        {
            VerifyValueWrite("write decimal");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(byte value)
        {
            VerifyValueWrite("write byte");
            WriteRaw(value.ToString());
        }

        public void WriteNumber(sbyte value)
        {
            VerifyValueWrite("write sbyte");
            WriteRaw(value.ToString());
        }

        public void WriteString(char value)
        {
            VerifyValueWrite("write char");
            WriteRaw("\"");
            if (value == '\0')
            {
                WriteRaw("\\u0000");
            }
            else WriteRaw(value.ToString());
            WriteRaw("\"");
        }

        public void WriteString(string value)
        {
            VerifyValueWrite("write string");
            WriteRaw("\"");
            WriteRaw(value);
            WriteRaw("\"");
        }

        public void WriteBoolean(bool value)
        {
            VerifyValueWrite("write boolean");
            WriteRaw(value ? "true" : "false");
        }

        public void WriteNull()
        {
            VerifyValueWrite("write null");
            WriteRaw("null");
        }

        public string GetValueAsString()
        {
            return m_textWriter.ToString();
        }

        // Try auto complete the colon (object value) or comma (array value) before write any value.
        private void VerifyValueWrite(string msg)
        {
            var status = m_genContext.OnBeforeWriteValue();
            switch (status)
            {
                case GeneratorContext.Status.OK_NEED_COLON:
                    WriteRaw(":");
                    break;
                case GeneratorContext.Status.OK_NEED_COMMA:
                    WriteRaw(",");
                    break;
                case GeneratorContext.Status.EXPECT_NAME:
                    throw new JsonException(
                        $"can't write value while ${msg}, expect write member name in context {m_genContext.CurContextType}");
                default:
                    break;
            }
        }


        public void Dispose()
        {
            m_textWriter.Dispose();
        }
        
    }
}