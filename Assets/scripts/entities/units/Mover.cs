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
    public void RotateObject(Transform obj, Vector3 targetDir, float speed)
    {
        if (targetDir == Vector3.zero) return;

        targetDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        obj.rotation = Quaternion.RotateTowards(obj.rotation, targetRotation, speed * Time.deltaTime);
    }
    public void MoveObjectRb(Vector3 direction, float speed, float acceleration)
    {
        // 1. Обчислюємо цільову швидкість тільки для горизонтальних осей (X, Z)
        Vector3 targetHorizontalVelocity = direction * speed;

        // 2. Беремо поточну швидкість Rigidbody
        Vector3 currentVelocity = rb.linearVelocity;

        // 3. Плавно змінюємо тільки горизонтальну складову
        Vector3 newHorizontalVelocity = Vector3.MoveTowards(
            new Vector3(currentVelocity.x, 0, currentVelocity.z),
            targetHorizontalVelocity,
            acceleration * Time.fixedDeltaTime
        );

        // 4. Повертаємо оригінальний Y (гравітацію) назад у вектор
        rb.linearVelocity = new Vector3(newHorizontalVelocity.x, currentVelocity.y, newHorizontalVelocity.z);
    }
    public void RotateObject(Transform obj, Vector3 targetDir, float speed, float smoothAmount)
    {
        if (targetDir == Vector3.zero) return;

        targetDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        obj.rotation = Quaternion.Slerp(obj.rotation, targetRotation, speed * Time.deltaTime * smoothAmount);
    }
}
