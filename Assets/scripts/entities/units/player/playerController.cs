using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : Tank
{
    public InputActionReference move;
    public InputActionReference attack;
    public Vector3 movementOffset = Vector3.zero;
    private void Update()
    {
        Vector2 dir = move.action.ReadValue<Vector2>();

        // Конвертуємо локальний напрямок (відносно гравця) у світовий
        Vector3 worldDirection = transform.TransformDirection(new Vector3(dir.x * movementOffset.x, movementOffset.y, dir.y * movementOffset.z));

        // Тепер передаємо цей світовий вектор у метод
        Ride(worldDirection);

        RotateTurret(AimTurret());

        //if ()
        //{
        //    Shoot();
        //}
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
    //BRUH
}
