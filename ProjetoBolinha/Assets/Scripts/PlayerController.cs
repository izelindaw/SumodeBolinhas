using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Cooldown")]
public float tempoRecarga = 2f;
private float proximoEmpurrao = 0f;
    public float speed = 6f;
    public string actionMap;
    public float pushForce = 15f;
    public float pushRange = 5f;

    public PlayerController opponent;

    private PlayerControls controls;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        controls = new PlayerControls();

        var map = controls.asset.FindActionMap(actionMap, true);

        map.Enable();

        map.FindAction("Move").performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        map.FindAction("Move").canceled += ctx => moveInput = Vector2.zero;
        map.FindAction("Push").performed += Push;
    }

    private void FixedUpdate()
    {
        Vector3 movimento = new Vector3(moveInput.x, 0, moveInput.y);

        rb.linearVelocity = new Vector3(
            movimento.x * speed,
            rb.linearVelocity.y,
            movimento.z * speed
        );
    }
    private void Push(InputAction.CallbackContext ctx)
{
    if (Time.time < proximoEmpurrao)
        return;

    proximoEmpurrao = Time.time + tempoRecarga;

    if (opponent == null)
        return;

    float distance = Vector3.Distance(transform.position, opponent.transform.position);

    if (distance > pushRange)
        return;

    Vector3 direction =
        (opponent.transform.position - transform.position).normalized;

    float force = pushForce * (1f - (distance / pushRange));

    Rigidbody enemyRb = opponent.GetComponent<Rigidbody>();

    enemyRb.AddForce(direction * force, ForceMode.Impulse);
}
}

