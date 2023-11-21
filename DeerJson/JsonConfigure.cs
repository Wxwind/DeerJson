namespace DeerJson
{
    public class JsonConfigure
    {
        private int m_mask;

        public JsonConfigure()
        {
        }

        private JsonConfigure(int mask)
        {
            m_mask = mask;
        }

        public bool IsEnabled(JsonFeature f)
        {
            return (m_mask & f.GetMask()) != 0;
        }

        public void Configure(JsonFeature f, bool enabled)
        {
            if (enabled)
            {
                Enable(f);
            }
            else
            {
                Disable(f);
            }
        }

        public void Enable(JsonFeature f)
        {
            m_mask |= f.GetMask();
        }

        public void Disable(JsonFeature f)
        {
            m_mask &= ~f.GetMask();
        }
    }
}