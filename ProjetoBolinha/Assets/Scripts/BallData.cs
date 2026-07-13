using UnityEngine;

[CreateAssetMenu(fileName = "NovaBolinha", menuName = "Bolinhas/Ball Data")]
public class BallData : ScriptableObject
{
    public string nome;

    public float velocidade;

    public float forcaEmpurrao;

    public float massa;

    public Color cor;

    public float tamanho;
}