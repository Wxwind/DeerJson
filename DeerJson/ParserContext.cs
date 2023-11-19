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

        private int  m_index          = 0;
        private bool m_hasWrittenName = false;

        public ContextType CurContextType { get; }

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
            if (m_hasWrittenName)
            {
                return Status.EXPECT_VALUE;
            }

            m_hasWrittenName = true;
            return m_index == 0 ? Status.OK : Status.OK_NEED_COMMA;
        }

        public Status OnBeforeGetValue()
        {
            if (CurContextType == ContextType.OBJECT)
            {
                if (!m_hasWrittenName)
                {
                    return Status.EXPECT_NAME;
                }

                m_hasWrittenName = false;
                m_index++;
                return Status.OK_NEED_COLON;
            }

            if (CurContextType == ContextType.ARRAY)
            {
                var i = m_index;
                m_index++;
                return i == 0 ? Status.OK : Status.OK_NEED_COMMA;
            }

            m_index++;
            return Status.OK;
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

        public ParserContext GetParentContext()
        {
            return m_parent;
        }
    }
}