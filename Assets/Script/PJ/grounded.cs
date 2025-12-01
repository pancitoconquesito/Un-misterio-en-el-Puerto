using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class grounded : MonoBehaviour
{
    private enum modeColision
    {
        capsule, boxCollider
    }

    [Header("-- Grounded --")]
    [SerializeField] [Layer] int m_layer;
    [SerializeField] private modeColision mode;
    [SerializeField] private float largeRay;
    [SerializeField] [Tag] private string[] tagCompare;
    [SerializeField] float timeDelay = 0.05f;

    private CapsuleCollider2D capuslePJ;
    private BoxCollider2D boxCollider;
    private float offset_X;
    [SerializeField] private float offset_Y;
    private bool moveablePlatform;
    Rigidbody2D rb_pj;
    private bool privateIsGrounded = false;
    float curr_timeDelay = 0.05f;

    private void Awake()
    {
        capuslePJ = this.GetComponent<CapsuleCollider2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        rb_pj = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (mode == modeColision.capsule)
        {
            boxCollider.enabled = false;
            offset_X = (capuslePJ.bounds.size.x / 2) * 0.7f;
            offset_Y = (capuslePJ.bounds.size.y / 2);
        }
        else
        {
            capuslePJ.enabled = false;
            //offset_X = (boxCollider.bounds.size.x / 2) * 0.7f;
            offset_X = (boxCollider.bounds.size.x / 2);
            //offset_Y = (boxCollider.bounds.size.y / 2)+0.0001f;
            //offset_Y = boxCollider.bounds.size.y;
        }
    }

    public bool isGrounded()
    {
        privateIsGrounded =
            rb_pj.velocity.y < 1 &&
        (
            lanzarRayo(0, offset_Y)
            || lanzarRayo(offset_X, offset_Y)
            || lanzarRayo(-offset_X, offset_Y)
        );

        //if (privateIsGrounded)
        //{
        //    curr_timeDelay = timeDelay;
        //}
        //else
        //{
        //    curr_timeDelay -= Time.deltaTime;
        //}
        return privateIsGrounded;// || curr_timeDelay > 0f;
    }

    public bool isMoveablePlatform()
    {
        moveablePlatform = lanzarRayoMoveablePlatform(0, offset_Y)
        || lanzarRayoMoveablePlatform(offset_X, offset_Y)
        || lanzarRayoMoveablePlatform(-offset_X, offset_Y);
        return moveablePlatform;
    }
    [ShowNonSerializedField] bool esSueloSolido = false;
    PlatformEffector2D curr_Effector2D =null;


    private bool lanzarRayo(float offetX, float offsetY)
    {
        Vector2 newPosition = new Vector2(transform.position.x + offetX, transform.position.y - offsetY);

        // Crear un filtro que ignore triggers
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false; // 👈 Ignora colliders con IsTrigger = true
        filter.SetLayerMask(1 << m_layer);

        // Almacenar el resultado del raycast
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = Physics2D.Raycast(newPosition, Vector2.down, filter, hits, largeRay);

        if (hitCount > 0 && System.Array.Exists(tagCompare, t => hits[0].collider.CompareTag(t)))
        {
            Debug.DrawRay(newPosition, Vector2.down * largeRay, Color.yellow);
            PlatformEffector2D effector = hits[0].collider.GetComponent<PlatformEffector2D>();
            esSueloSolido = effector == null;
            curr_Effector2D = effector;
            return true;
        }
        else
        {
            esSueloSolido = false;
            Debug.DrawRay(newPosition, Vector2.down * largeRay, Color.red);
        }

        return false;
        //Vector2 newPosition = new Vector2(transform.position.x + offetX, transform.position.y - offsetY);
        //RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.down, largeRay, 1 << m_layer);
        //if (hit.collider != null && System.Array.Exists(tagCompare, t => hit.collider.CompareTag(t)))
        //{
        //    Debug.DrawRay(newPosition, Vector2.down * largeRay, Color.yellow);
        //    PlatformEffector2D effector = hit.collider.GetComponent<PlatformEffector2D>();
        //    esSueloSolido = effector == null;
        //    curr_Effector2D = effector;
        //    return true;
        //}
        //else
        //{
        //    esSueloSolido = false;
        //    Debug.DrawRay(newPosition, Vector2.down * largeRay, Color.red);

        //}
        //return false;
    }

    private bool lanzarRayoMoveablePlatform(float offetX, float offsetY)
    {
        Vector2 newPosition = new Vector2(transform.position.x + offetX, transform.position.y - offsetY);
        RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.down, largeRay);
        if (hit.collider != null)
        {
            Debug.DrawRay(newPosition, Vector2.down * largeRay, Color.yellow);
            if (hit.collider.GetComponent<MoveablePlatform>() != null)
            {
                m_currentMoveablePlatform = hit.collider.GetComponent<MoveablePlatform>();//FIXED
                currentPlatform = hit.collider.transform;
                return true;
            }
            else return false;
        }
        return false;
    }

    Transform currentPlatform;
    MoveablePlatform m_currentMoveablePlatform;
    float XoffesetPosition;

    public PlatformEffector2D Curr_Effector2D { get => curr_Effector2D; set => curr_Effector2D = value; }
    public bool EsSueloSolido { get => esSueloSolido;}

    public Vector2 CalculatePositionDelta(Vector2 positionPlayer)
    {
        return positionPlayer - (Vector2)currentPlatform.transform.position;
    }

    public Transform GetPlatformMoveable() => currentPlatform;
    public float GetYPosition() => currentPlatform.position.y + m_currentMoveablePlatform.GetHeightOffset();
}
