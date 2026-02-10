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
        // 1. Переводимо поточну світову швидкість у локальну (відносно гравця)
        // Тепер Vector3.up — це завжди "голова" гравця, а не небо
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

        // 2. Обчислюємо цільову швидкість (direction має бути локальним, наприклад, Vector2 від Input)
        Vector3 targetLocalHorizontalVelocity = direction * speed;

        // 3. Плавно змінюємо локальні X та Z (це площина, по якій ми ходимо)
        Vector3 currentLocalHorizontal = new Vector3(localVelocity.x, 0, localVelocity.z);

        Vector3 newLocalHorizontal = Vector3.MoveTowards(
            currentLocalHorizontal,
            targetLocalHorizontalVelocity,
            acceleration * Time.fixedDeltaTime
        );

        // 4. Збираємо локальний вектор назад:
        // Нові X та Z + старий локальний Y (сила, що притискає до стіни або стрибок)
        Vector3 newLocalVelocity = new Vector3(newLocalHorizontal.x, localVelocity.y, newLocalHorizontal.z);

        // 5. Переводимо результат назад у світові координати для Rigidbody
        rb.linearVelocity = transform.TransformDirection(newLocalVelocity);
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
