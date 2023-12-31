﻿namespace DeerJson
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
            NONE,
            OBJECT,
            ARRAY
        }

        private readonly GeneratorContext m_parent;

        private int  m_index          = 0;
        private bool m_hasWrittenName = false;

        public ContextType CurContextType { get; }

        public GeneratorContext()
        {
            CurContextType = ContextType.NONE;
        }

        public GeneratorContext(GeneratorContext parent, ContextType contextType)
        {
            m_parent = parent;
            CurContextType = contextType;
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

        public GeneratorContext NewArrayContext()
        {
            return new GeneratorContext(this, ContextType.ARRAY);
        }

        public GeneratorContext NewObjectContext()
        {
            return new GeneratorContext(this, ContextType.OBJECT);
        }

        public GeneratorContext GetParentContext()
        {
            return m_parent;
        }
    }
}