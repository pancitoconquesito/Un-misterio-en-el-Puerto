using UnityEngine;
public class movOffsetMat : MonoBehaviour
{
    [SerializeField] private float speed_X, speed_Y;
    private Renderer rend;
    void Start()
    {
        //GetComponent<MeshRenderer>().materials[0].GetTextureOffset("mat_b");
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset_X = Time.time * speed_X;
        float offset_Y = Time.time * speed_Y;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset_X, offset_Y));
    }
}
