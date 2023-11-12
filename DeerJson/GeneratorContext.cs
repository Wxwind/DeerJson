namespace DeerJson
{
    public class GeneratorContext
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
            None,
            Object,
            Array
        }

        private readonly GeneratorContext m_parent;

        private int  m_index          = 0;
        private bool m_hasWrittenName = false;

        private Status      m_status = Status.OK;
        private ContextType m_curContextType;

        public ContextType CurContextType => m_curContextType;

        public GeneratorContext()
        {
            m_curContextType = ContextType.None;
        }

        public GeneratorContext(GeneratorContext parent, ContextType contextType)
        {
            m_parent = parent;
            m_curContextType = contextType;
        }

        public Status OnBeforeWriteMemberName()
        {
            if (m_hasWrittenName)
            {
                return Status.EXPECT_VALUE;
            }

            m_hasWrittenName = true;
            return m_index == 0 ? Status.OK : Status.OK_NEED_COMMA;
        }

        public Status OnBeforeWriteValue()
        {
            if (m_curContextType == ContextType.Object)
            {
                if (!m_hasWrittenName)
                {
                    return Status.EXPECT_NAME;
                }

                m_hasWrittenName = false;
                m_index++;
                return Status.OK_NEED_COLON;
            }

            if (m_curContextType == ContextType.Array)
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
            return m_curContextType == ContextType.Array;
        }

        public bool InObject()
        {
            return m_curContextType == ContextType.Object;
        }

        public GeneratorContext NewArrayContext()
        {
            return new GeneratorContext(this, ContextType.Array);
        }

        public GeneratorContext NewObjectContext()
        {
            return new GeneratorContext(this, ContextType.Object);
        }

        public GeneratorContext GetParentContext()
        {
            return m_parent;
        }
    }
}