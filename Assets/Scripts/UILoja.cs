using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UILoja : MonoBehaviour
{
    [Header("Skins disponiveis")]
    public SkinItem[] skins;

    [Header("UI")]
    public TMP_Text textoMoedas;

    void Start()
    {
        AtualizarUI();
    }

    void AtualizarUI()
    {
        textoMoedas.text = "Moedas: " + GerenciadorMoedas.ObterInstancia().MoedasTotal;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].Atualizar(i);
        }
    }

    public void TentarComprar(int id)
    {
        SkinItem item = skins[id];

        if (GerenciadorLoja.ObterInstancia().SkinComprada(id))
        {
            GerenciadorLoja.ObterInstancia().SelecionarSkin(id);
        }
        else
        {
            bool sucesso = GerenciadorLoja.ObterInstancia().ComprarSkin(id, item.preco);
            if (!sucesso)
            {
                Debug.Log("Moedas insuficientes!");
                return;
            }

            GerenciadorLoja.ObterInstancia().SelecionarSkin(id);
        }

        AtualizarUI();
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}

[System.Serializable]
public class SkinItem
{
    public Sprite sprite;
    public string nome;
    public int preco;

    [Header("Referencias UI do card")]
    public Image imagemSkin;
    public TMP_Text textoNome;
    public TMP_Text textoPreco;
    public Button botao;
    public TMP_Text textoBotao;  
    public Image bordaSelecao;

    public void Atualizar(int id)
    {
        imagemSkin.sprite = sprite;
        textoNome.text = nome;

        bool comprada = GerenciadorLoja.ObterInstancia().SkinComprada(id);
        bool selecionada = GerenciadorLoja.ObterInstancia().SkinSelecionada() == id;

        if (id == 0)
        {
            textoBotao.text = selecionada ? "Equipada" : "Equipar";
            textoPreco.text = "Gratis";
        } else if (comprada)
        {
            textoBotao.text = selecionada ? "Equipada" : "Equipar";
            textoPreco.text = "Comprada";
        }
        else
        {
            textoBotao.text = preco + " Moedas";
            textoPreco.text = preco + " Moedas";
        }
        if (bordaSelecao != null) bordaSelecao.gameObject.SetActive(selecionada);

    }
}
