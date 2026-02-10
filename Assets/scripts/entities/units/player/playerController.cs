using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : Tank
{
    public InputActionReference move;
    public InputActionReference attack;
    private void Update()
    {
        Vector2 input = move.action.ReadValue<Vector2>();

        // 1. Отримуємо базові світові вектори руху (наприклад, відносно камери або просто світу)
        // Якщо у тебе є камера, краще замінити Vector3.forward на camera.transform.forward
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        // 2. Формуємо світовий вектор бажаного напрямку
        Vector3 desiredMoveDirection = (forward * input.y + right * input.x).normalized;

        // 3. Передаємо цей вектор у Ride
        // Навіть якщо desiredMoveDirection дивиться "в землю", Ride його виправить
        Ride(desiredMoveDirection);

        RotateTurret(AimTurret());
    }
    private Vector3 AimTurret()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            // 2. Отримуємо напрямок до точки зіткнення
            Vector3 lookDir = hit.point - Turret.transform.position;
            lookDir.y = 0; // Щоб башта не нахилялася в землю
            if (lookDir != Vector3.zero)
            {
                return lookDir;
            }
        }
        return Vector3.zero; // Якщо не влучили по землі, не обертати башту
    }
    private void OnSpace(InputAction.CallbackContext obj)
    {
        Shoot();
    }
    private void OnEnable()
    {
        attack.action.Enable();
        move.action.Enable();
        attack.action.performed += OnSpace;
    }
    private void OnDisable()
    {
        attack.action.Disable();
        move.action.Disable();
        attack.action.performed -= OnSpace;
    }
}
