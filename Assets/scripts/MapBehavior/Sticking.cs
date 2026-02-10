using UnityEngine;

public class Sticking : MonoBehaviour
{
    public LayerMask groundLayer;
    public float scanRadius;

    private Rigidbody rb;

    private float gravityForce = 9.81f;
    private Vector3 gravityVector;
    private bool groundDetected;
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
        rb.AddForce(gravityVector * gravityForce, ForceMode.Force);

        if (!groundDetected)
        {
            StickingSys();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gravityVector = -collision.GetContact(0).normal.normalized;
            groundDetected = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundDetected = false;
        }
    }
    private void StickingSys()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRadius, groundLayer);

        if (hits.Length > 0)
        {
            Collider groundLevel = hits[0];
            if (groundLevel != null)
            {
                gravityVector = (groundLevel.ClosestPoint(transform.position) - (Vector3)transform.position).normalized;
            }
        }
        
    }
}
