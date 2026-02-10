using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform TargetPosition;
    private void Update()
    {
        transform.position = TargetPosition.position;
    }
}
