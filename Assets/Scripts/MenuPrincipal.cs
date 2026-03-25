using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
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
}
