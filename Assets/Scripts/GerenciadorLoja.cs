using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorLoja : MonoBehaviour
{
    public static GerenciadorLoja Instancia {  get; private set; }

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);

    }

    public bool SkinComprada(int id)
    {
        return PlayerPrefs.GetInt("skin_comprada_" + id, 0) == 1;
    }

    public int SkinSelecionada()
    {
        return PlayerPrefs.GetInt("skin_selecionada", 0);
    }

    public bool ComprarSkin(int id, int preco)
    {
        if (SkinComprada(id)) return false;
        if (!GerenciadorMoedas.Instancia.GastarMoedas(preco)) return false;

        PlayerPrefs.SetInt("skin_comprada_" + id, 1);
        PlayerPrefs.Save();
        return true;
    }

    public bool SelecionarSkin(int id)
    {
        if (!SkinComprada(id)) return false;

        PlayerPrefs.SetInt("skin_selecionada", id);
        PlayerPrefs.Save();
        return true;
    }
}
