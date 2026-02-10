using UnityEngine;

public class Sticking : MonoBehaviour
{
    public LayerMask groundLayer;
    public float rayDistance;

    private Rigidbody rb;

    private float gravityForce = 9.81f;
    private Vector3 gravityVector;

    public bool debugMode;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //raycast for collider search
        ColliderFinderRayCast();

        //gravity
        rb.AddForce(gravityVector * gravityForce * 10 * rb.mass, ForceMode.Force);
    }
    private void ColliderFinderRayCast()
    {
        Vector3[] directions = {
        -transform.up,
        -transform.forward,
        transform.right,
        -transform.right,
        transform.forward
        };

        foreach (Vector3 dir in directions)
        {
            RaycastHit tempHit;

            if (Physics.Raycast(transform.position, dir, out tempHit, rayDistance, groundLayer))
            {
                //calling for new gravity vector when found a surface
                SetGravity(tempHit);

                if (debugMode)
                {
                    Debug.DrawRay(transform.position, dir * rayDistance, Color.red);
                    Debug.Log("gravityVector: " + gravityVector);
                }
            }
        }
    }
    private void SetGravity(RaycastHit hit)
    {
        gravityVector = -hit.normal; //gravity vector
        AlightSurface(hit.normal); //attemp at changing player's alignment to surface

        if (debugMode)
        {
            Debug.Log("gravityVector: " + gravityVector);
        }
    }
    private void AlightSurface(Vector3 normal)
    {
        Vector3 newForfard = Vector3.ProjectOnPlane(transform.forward, normal);

        if (newForfard.sqrMagnitude < 0.001f)
        {
            newForfard = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(newForfard, normal);

        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
    }
}
