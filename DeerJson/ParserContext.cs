namespace DeerJson
{
    public class ParserContext
    {
        public enum Status : byte
        {
            OK,
            OK_NEED_COMMA,
            OK_NEED_COLON,
            EXPECT_VALUE,
            EXPECT_NAME
        }

        public enum ContextType : byte
        {
            NONE,
            OBJECT,
            ARRAY
        }

        private readonly ParserContext m_parent;

        private int  m_index         = 0;
        private bool m_hasParsedName = false;

        public ContextType CurContextType { get; }
        public bool HasParsedName => m_hasParsedName;

        public ParserContext()
        {
            CurContextType = ContextType.NONE;
        }

        public ParserContext(ParserContext parent, ContextType contextType)
        {
            m_parent = parent;
            CurContextType = contextType;
        }

        public Status OnBeforeGetMemberName()
        {
            if (m_hasParsedName)
            {
                return Status.EXPECT_VALUE;
            }

            m_hasParsedName = true;
            return m_index == 0 ? Status.OK : Status.OK_NEED_COMMA;
        }

        public Status OnBeforeGetValue()
        {
            if (CurContextType == ContextType.OBJECT)
            {
                if (!m_hasParsedName)
                {
                    return Status.EXPECT_NAME;
                }

                m_hasParsedName = false;
                m_index++;
                return Status.OK_NEED_COLON;
            }

            if (CurContextType == ContextType.ARRAY)
            {
                var i = m_index;
                m_index++;
                return Status.OK;
            }

            m_index++;
            return Status.OK;
        }

        public bool ExpectComma()
        {
            return CurContextType != ContextType.NONE && m_index > 0;
        }

        public bool InArray()
        {
            return CurContextType == ContextType.ARRAY;
        }

        public bool InObject()
        {
            return CurContextType == ContextType.OBJECT;
        }

        public ParserContext NewArrayContext()
        {
            return new ParserContext(this, ContextType.ARRAY);
        }

        public ParserContext NewObjectContext()
        {
            return new ParserContext(this, ContextType.OBJECT);
        }

        // 0 - Object, 1 - Array
        public ParserContext GetParentContext(int mode)
        {
            if (CurContextType == ContextType.OBJECT && mode == 0)
            {
                return m_parent;
            }

            if (CurContextType == ContextType.ARRAY && mode == 1)
            {
                return m_parent;
            }

            throw new JsonException("accident close of scope");
        }
    }
}