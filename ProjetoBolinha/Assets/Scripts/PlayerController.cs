using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Cooldown")]
    public float tempoRecarga = 2f;
    private float proximoEmpurrao = 0f;

    [Header("Bolinhas")]
    public BallData[] bolinhas;
    public bool jogador1;

    private BallData dadosBolinha;

    public float speed = 6f;
    public string actionMap;
    public float pushForce = 15f;
    public float pushRange = 5f;

    public PlayerController opponent;

    [Header("Interface")]
    public Slider cooldownBar;

    private PlayerControls controls;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Carrega a bolinha escolhida na tela de sele��o
        int indice = jogador1 ? PlayerPrefs.GetInt("P1", 0) : PlayerPrefs.GetInt("P2", 0);

        if (bolinhas != null && bolinhas.Length > indice)
        {
            dadosBolinha = bolinhas[indice];

            speed = dadosBolinha.velocidade;
            pushForce = dadosBolinha.forcaEmpurrao;
            rb.mass = dadosBolinha.massa;

            transform.localScale = Vector3.one * dadosBolinha.tamanho;
            GetComponent<Renderer>().material.color = dadosBolinha.cor;
        }

        controls = new PlayerControls();

        var map = controls.asset.FindActionMap(actionMap, true);

        map.Enable();

        map.FindAction("Move").performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        map.FindAction("Move").canceled += ctx => moveInput = Vector2.zero;
        map.FindAction("Push").performed += Push;
    }

    private void Update()
    {
        if (cooldownBar == null)
            return;

        float progresso = Mathf.Clamp01(
            (tempoRecarga - (proximoEmpurrao - Time.time)) / tempoRecarga
        );

        cooldownBar.value = progresso;
    }

    private void FixedUpdate()
    {
        Vector3 movimento = new Vector3(moveInput.x, 0, moveInput.y);

        rb.AddForce(new Vector3(
            movimento.x * speed,
            rb.linearVelocity.y,
            movimento.z * speed
        ));
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

        Vector3 direction = (opponent.transform.position - transform.position).normalized;

        float force = pushForce * (1f - (distance / pushRange));

        Rigidbody enemyRb = opponent.GetComponent<Rigidbody>();

        enemyRb.AddForce(direction * force, ForceMode.Impulse);
    }
}