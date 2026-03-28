using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    void OnEnable()
    {
        // Busca o texto pelo nome toda vez que a cena ativa
        TMP_Text campoMoedas = GameObject.Find("Texto_Moedas")
                                         ?.GetComponent<TMP_Text>();

        if (campoMoedas != null)
            campoMoedas.text = " " + PlayerPrefs.GetInt("moedas_total", 0);
    }

    //botão jogar
    public void botaoJogar()
    {
        SceneManager.LoadScene("Jogo1");
    }

    public void botaoConfig()
    {
        SceneManager.LoadScene("Configuracoes");
    }

    public void botaoLoja()
    {
        SceneManager.LoadScene("Loja");
    }

    public void botaoSair()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
