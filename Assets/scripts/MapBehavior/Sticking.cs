using UnityEngine;

public class Sticking : MonoBehaviour
{
    public LayerMask groundLayer;
    public float rayDistance;

    private Rigidbody rb;

    private float gravityForce = 9.81f;
    public Vector3 gravityVector;

    public bool debugMode;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //raycast for collider search
        ColliderFinderRayCast();

        //gravity
        rb.AddForce(gravityVector * gravityForce * 5 * rb.mass, ForceMode.Force);
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

            if (Physics.SphereCast(transform.position + (transform.up * 0.5f), 0.5f, dir, out tempHit, rayDistance, groundLayer))
            {
                //calling for new gravity vector when found a surface
                SetGravity(tempHit);

                if (debugMode)
                {
                    Debug.DrawRay(transform.position, dir * rayDistance, Color.red);
                }
            }
        }
    }
    private void SetGravity(RaycastHit hit)
    {
        Vector3 rounded = new Vector3(Mathf.Round(hit.normal.x), Mathf.Round(hit.normal.y), Mathf.Round(hit.normal.z)); //rounding the normal vector to avoid small changes in gravity vector

        if (rounded == Vector3.zero)
        {
            rounded = hit.normal;
        }

        if (gravityVector == -rounded) return; //if the same surface is already set as gravity, do nothing

        gravityVector = -rounded; //gravity vector
        AlightSurface(rounded); //attemp at changing player's alignment to surface

        if (debugMode)
        {
            Debug.Log("gravityVector: " + gravityVector);
        }
    }
    private void AlightSurface(Vector3 normal)
    {
        //making forward parralel to the surface
        Vector3 newForfard = Vector3.ProjectOnPlane(transform.forward, normal);

        //rotating the object to the surface
        Quaternion targetRotation = Quaternion.LookRotation(newForfard, normal);

        //applying rotation
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
    }
}
