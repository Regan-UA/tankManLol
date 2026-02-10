using UnityEngine;

public class Sticking : MonoBehaviour
{
    public LayerMask groundLayer;
    public float scanRadius;
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
        ColliderFinderRayCast();

        rb.AddForce(gravityVector * gravityForce * rb.mass, ForceMode.Force);
    }
    private void ColliderFinderRayCast()
    {
        Vector3[] directions = {
        transform.right,
        -transform.right,
        transform.forward,
        -transform.forward,
        -transform.up
        };

        foreach (Vector3 dir in directions)
        {
            RaycastHit tempHit;

            if (Physics.Raycast(transform.position, dir, out tempHit, rayDistance, groundLayer))
            {
                SetGravity(tempHit);
            }
        }
    }
    private void SetGravity(RaycastHit hit)
    {
        gravityVector = -hit.normal;
        AlightSurface(hit.normal);

        if (debugMode)
        {
            Debug.DrawRay(transform.position, -transform.up * rayDistance, Color.red);
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
