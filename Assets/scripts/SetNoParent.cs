using UnityEngine;

public class SetNoParent : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
    }
}
