using UnityEditor;

namespace OGT.Editor
{
    internal class SavedBool
    {
        private bool m_value;
        private readonly string m_name;

        public bool Value
        {
            get => m_value;
            set
            {
                if (m_value == value)
                    return;

                m_value = value;
                EditorPrefs.SetBool(m_name, value);
            }
        }

        public SavedBool(string name, bool value)
        {
            m_name = name;
            m_value = EditorPrefs.GetBool(name, value);
        }
    }
}