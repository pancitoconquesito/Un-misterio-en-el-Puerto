using UnityEngine;
public class PosIniFallingVisualization : MonoBehaviour
{
    float aaa= 1.05f, bbb= -4.19f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 _endPosition = transform.position;
        Vector3 _horizontal = transform.position;
        _horizontal.x = transform.position.x - bbb;
        _horizontal.y = transform.position.y+ aaa;
        _endPosition.y = transform.position.y+ aaa;
        Gizmos.DrawLine(transform.position, _endPosition);
        Gizmos.DrawLine(_horizontal, new Vector3(_horizontal.x+ bbb*2, _horizontal.y, _horizontal.z));
    }
}
