using UnityEngine;
public class setPositionUpdate : MonoBehaviour
{
    [SerializeField] private Transform transformTarget;
    [SerializeField] private Vector3 offsetTarget;
    void Update()
    {
       // transform.position = new Vector3(transformTarget.position.x, transformTarget.position.y + transformTarget.position.y, 0);


        transform.position = transformTarget.localPosition + offsetTarget;



    }
}
