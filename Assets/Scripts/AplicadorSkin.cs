using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplicadorSkin : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer sr;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int id = GerenciadorLoja.Instancia.SkinSelecionada();

        if (id < sprites.Length) sr.sprite = sprites[id];
    }
}
