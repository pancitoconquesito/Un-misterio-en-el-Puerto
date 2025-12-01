using UnityEngine;
using UnityEngine.UI;
public class SetRectMaskPaddingOnStart : MonoBehaviour
{
    [SerializeField] RectMask2D m_RectMask2D;
    [System.Serializable]
    public struct Padding
    {
        public float Top;
        public float Left;
        public float Right;
        public float Down;
    }
    [SerializeField] Padding m_Padding;
    private void Awake()
    {
        Vector4 padding = new Vector4(m_Padding.Left, m_Padding.Right, m_Padding.Top, m_Padding.Down);
        m_RectMask2D.padding = padding;
    }
}
