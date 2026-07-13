using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player1;
    public PlayerController player2;

    public Transform spawnPlayer1;
    public Transform spawnPlayer2;

    [Header("Interface")]
    public TMP_Text textoP1;
    public TMP_Text textoP2;

    [Header("Tela de Vitória")]
    public GameObject painelVitoria;
    public TMP_Text textoVitoria;

    private int pontosP1 = 0;
    private int pontosP2 = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AtualizarInterface();

        if (painelVitoria != null)
            painelVitoria.SetActive(false);
    }

    public void PlayerPerdeu(PlayerController player)
    {
        if (player == player1)
            pontosP2++;
        else
            pontosP1++;

        AtualizarInterface();

        if (pontosP1 >= 2)
        {
            MostrarVitoria("Jogador 1 venceu!");
            return;
        }

        if (pontosP2 >= 2)
        {
            MostrarVitoria("Jogador 2 venceu!");
            return;
        }

        ReiniciarRound();
    }

    void AtualizarInterface()
    {
        textoP1.text = "P1: " + pontosP1;
        textoP2.text = "P2: " + pontosP2;
    }

    void ReiniciarRound()
    {
        player1.transform.position = spawnPlayer1.position;
        player2.transform.position = spawnPlayer2.position;

        Rigidbody rb1 = player1.GetComponent<Rigidbody>();
        Rigidbody rb2 = player2.GetComponent<Rigidbody>();

        rb1.linearVelocity = Vector3.zero;
        rb2.linearVelocity = Vector3.zero;
    }

    void MostrarVitoria(string vencedor)
    {
        painelVitoria.SetActive(true);
        textoVitoria.text = vencedor;

        Time.timeScale = 0f;
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        pontosP1 = 0;
        pontosP2 = 0;

        AtualizarInterface();

        painelVitoria.SetActive(false);

        ReiniciarRound();
    }
}