using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorMoedas : MonoBehaviour
{
    public static GerenciadorMoedas Instancia {  get; private set; }

    //contagem da sessão atual
    public int MoedasSessao { get; private set; }
    public int LikesSessao { get; private set; }
    public int SeguidoresSessao { get; private set; }

    //contagem de moedas para uso na loja
    public int MoedasTotal {  get; private set; }
   

    //progressão de seguidores
    public int SeguidoresTotal { get; private set; }

    //recorde
    public int MelhorPontuacao { get; private set; }

    //define o valor de cada moeda
    private const int VALOR_VIEW = 1;
    private const int VALOR_LIKE = 5;
    private const int VALOR_SEGUIDOR = 10;


    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
        CarregarDados();
    }

    // Garante que o GerenciadorMoedas sempre existe, em qualquer cena
    public static GerenciadorMoedas ObterInstancia()
    {
        if (Instancia == null)
        {
            GameObject go = new GameObject("GerenciadorMoedas");
            Instancia = go.AddComponent<GerenciadorMoedas>();
        }
        return Instancia;
    }

    public void AdicionarMoeda (Moeda.TipoMoeda tipo)
    {
        switch (tipo)
        {
            case Moeda.TipoMoeda.Dourada:
                MoedasSessao += VALOR_VIEW;
                MoedasTotal += VALOR_VIEW;
                break;

            case Moeda.TipoMoeda.Like:
                LikesSessao += 1;
                MoedasSessao += VALOR_LIKE;
                MoedasTotal += VALOR_LIKE;
                break;

            case Moeda.TipoMoeda.Seguidor:
                SeguidoresSessao += 1;
                SeguidoresTotal += 1;
                MoedasSessao += VALOR_SEGUIDOR;
                MoedasTotal += VALOR_SEGUIDOR;
                break;
        }
        
        if (MoedasSessao > MelhorPontuacao) MelhorPontuacao = MoedasSessao;
        
        SalvarDados();
    }

    public bool GastarMoedas (int quantidade)
    {
        if (MoedasTotal < quantidade) return false;
        MoedasTotal -= quantidade;
        SalvarDados();
        return true;
    }

    public void ResetarSessao()
    {
        MoedasSessao = 0;
        LikesSessao = 0;
        SeguidoresSessao = 0;
    }

    void SalvarDados()
    {
        PlayerPrefs.SetInt("moedas_total", MoedasTotal);
        PlayerPrefs.SetInt("seguidores_total", SeguidoresTotal);
        PlayerPrefs.SetInt("melhor_pontuacao", MelhorPontuacao);
        PlayerPrefs.Save();
    }

    void CarregarDados()
    {
        MoedasTotal = PlayerPrefs.GetInt("moedas_total", 0);
        SeguidoresTotal = PlayerPrefs.GetInt("seguidores_total", 0);
        MelhorPontuacao = PlayerPrefs.GetInt("melhor_pontuacao", 0);
    }
}
