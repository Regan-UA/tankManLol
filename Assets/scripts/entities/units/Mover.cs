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
    public void MoveObjectRb(Vector3 worldDirection, float speed, float acceleration)
    {
        if (worldDirection.magnitude < 0.01f)
        {
            // Плавна зупинка, якщо вводу немає, зберігаючи лише вертикальну швидкість (гравітацію)
            Vector3 verticalVel = Vector3.Project(rb.linearVelocity, transform.up);
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, verticalVel, acceleration * Time.fixedDeltaTime);
            return;
        }

        // 1. ПРОЕКЦІЯ: Робимо ввід паралельним поверхні, на якій стоїть танк
        // transform.up — це нормаль, яку ти отримуєш у скрипті Sticking
        Vector3 moveDirOnPlane = Vector3.ProjectOnPlane(worldDirection, transform.up).normalized;

        // 2. Розраховуємо цільову швидкість
        Vector3 targetVelocity = moveDirOnPlane * speed;

        // 3. Отримуємо поточну швидкість і відокремлюємо "притискання"
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 verticalComponent = Vector3.Project(currentVelocity, transform.up);
        Vector3 horizontalComponent = currentVelocity - verticalComponent;

        // 4. Змінюємо тільки рух вздовж поверхні
        Vector3 newHorizontal = Vector3.MoveTowards(horizontalComponent, targetVelocity, acceleration * Time.fixedDeltaTime);

        // 5. Фінальний вектор: рух по поверхні + існуюча сила притягання
        rb.linearVelocity = newHorizontal + verticalComponent;
    }
    public void RotateObject(Transform obj, Vector3 worldDir, float speed, float smoothAmount)
    {
        if (worldDir.sqrMagnitude < 0.01f) return;

        // Проектуємо напрямок погляду на поверхню
        Vector3 projectedDir = Vector3.ProjectOnPlane(worldDir, obj.up);

        if (projectedDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(projectedDir, obj.up);
            obj.rotation = Quaternion.Slerp(obj.rotation, targetRotation, speed * Time.deltaTime * smoothAmount);
        }
    }
    public void RotateObject(Transform obj, Vector3 localDir, float speed)
    {
        if (localDir.sqrMagnitude < 0.01f) return;

        // Створюємо вектор напрямку в світових координатах на основі локального вводу
        Vector3 worldDir = obj.TransformDirection(localDir);

        // Проектуємо цей напрямок на площину поверхні (використовуючи поточний UP танка)
        Vector3 projectedDir = Vector3.ProjectOnPlane(worldDir, obj.up);

        if (projectedDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(projectedDir, obj.up);
            obj.rotation = Quaternion.RotateTowards(obj.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}
