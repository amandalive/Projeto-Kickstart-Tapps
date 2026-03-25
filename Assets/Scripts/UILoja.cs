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
        textoMoedas.text = "Moedas: " + GerenciadorMoedas.Instancia.MoedasTotal;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].Atualizar(i);
        }
    }

    public void TentarComprar(int id)
    {
        SkinItem item = skins[id];

        if (GerenciadorLoja.Instancia.SkinComprada(id))
        {
            GerenciadorLoja.Instancia.SelecionarSkin(id);
        }
        else
        {
            bool sucesso = GerenciadorLoja.Instancia.ComprarSkin(id, item.preco);
            if (!sucesso)
            {
                Debug.Log("Moedas insuficientes!");
                return;
            }

            GerenciadorLoja.Instancia.SelecionarSkin(id);
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

        bool comprada = GerenciadorLoja.Instancia.SkinComprada(id);
        bool selecionada = GerenciadorLoja.Instancia.SkinSelecionada() == id;

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
            textoBotao.text = preco + "moedas";
            textoPreco.text = preco + "moedas";
        }
        if (bordaSelecao != null) bordaSelecao.gameObject.SetActive(selecionada);

    }
}
