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

    private int pontosP1 = 0;
    private int pontosP2 = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AtualizarInterface();
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
            Debug.Log("Jogador 1 venceu a partida!");
            return;
        }

        if (pontosP2 >= 2)
        {
            Debug.Log("Jogador 2 venceu a partida!");
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
}