using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public BallData[] bolinhas;

    public TMP_Text textoP1;
    public TMP_Text textoP2;

    int indiceP1 = 0;
    int indiceP2 = 0;

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            indiceP1--;

            if (indiceP1 < 0)
                indiceP1 = bolinhas.Length - 1;

            AtualizarTexto();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            indiceP1++;

            if (indiceP1 >= bolinhas.Length)
                indiceP1 = 0;

            AtualizarTexto();
        }

       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            indiceP2--;

            if (indiceP2 < 0)
                indiceP2 = bolinhas.Length - 1;

            AtualizarTexto();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            indiceP2++;

            if (indiceP2 >= bolinhas.Length)
                indiceP2 = 0;

            AtualizarTexto();
        }
    }

    void AtualizarTexto()
    {
        textoP1.text = bolinhas[indiceP1].nome;
        textoP2.text = bolinhas[indiceP2].nome;
    }

    public void Jogar()
    {
        PlayerPrefs.SetInt("P1", indiceP1);
        PlayerPrefs.SetInt("P2", indiceP2);

        SceneManager.LoadScene("GameplayScene");
    }
}