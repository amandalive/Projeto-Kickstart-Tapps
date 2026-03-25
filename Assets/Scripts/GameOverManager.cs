using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instancia {  get; private set; }

    [Header("Painel Game Over")]
    public GameObject painelGameOver;

    [Header("Textos")]
    public TMP_Text textoPontuacao;
    public TMP_Text textoMelhorPontuacao;
    public TMP_Text textoSeguidores;

    private bool gameOverAtivado = false;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;

        painelGameOver.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if(gameOverAtivado) return;
        gameOverAtivado = true;

        Time.timeScale = 0f;

        textoPontuacao.text = "Moedas: " + GerenciadorMoedas.Instancia.MoedasSessao;
        textoMelhorPontuacao.text = "Recorde: " + GerenciadorMoedas.Instancia.MelhorPontuacao;
        textoSeguidores.text = "Seguidores: " + GerenciadorMoedas.Instancia.SeguidoresSessao;

        GerenciadorMoedas.Instancia.ResetarSessao();

        painelGameOver.SetActive(true);
    }

    public void BotaoReiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotaoMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
