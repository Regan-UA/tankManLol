using UnityEngine;
public class Mover : MonoBehaviour
{
    protected Rigidbody rb;
    protected virtual void Awake() // Стандартний метод Unity
    {
        TryGetComponent(out rb);
    }
    public void MoveObjectRb(Vector3 direction, float speed)
    {
        rb.linearVelocity = speed * direction;
    }
    public void MoveObject(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    public void MoveObjectRb(Vector3 direction, float speed, float acceleration)
    {
        // 1. Визначаємо цільову швидкість у світових координатах
        Vector3 targetVelocity = direction * speed;

        // 2. Беремо поточну швидкість, але обнуляємо Y, щоб не впливати на гравітацію/стрибок
        Vector3 currentHorizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        // 3. Плавно наближаємо поточну швидкість до цільової
        Vector3 newHorizontalVelocity = Vector3.MoveTowards(
            currentHorizontalVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );

        // 4. Призначаємо нову швидкість, зберігаючи поточну вертикальну швидкість (Y)
        rb.linearVelocity = new Vector3(newHorizontalVelocity.x, rb.linearVelocity.y, newHorizontalVelocity.z);
    }
    public void RotateObject(Transform obj, Vector3 targetDir, float speed, float smoothAmount)
    {
        if (targetDir == Vector3.zero) return;

        targetDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        obj.rotation = Quaternion.Slerp(obj.rotation, targetRotation, speed * Time.deltaTime * smoothAmount);
    }
    public void RotateObject(Transform obj, Vector3 targetDir, float speed)
    {
        if (targetDir == Vector3.zero) return;

        targetDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        obj.rotation = Quaternion.RotateTowards(obj.rotation, targetRotation, speed * Time.deltaTime);
    }
}
