using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDMoedas : MonoBehaviour
{
    public TMP_Text textoPontuacao;
    public TMP_Text textoLikes;
    public TMP_Text textoSeguidores;
    public TMP_Text textoRecord;

    void Update()
    {
        if (GerenciadorMoedas.Instancia == null) return;

        textoPontuacao.text = "Moedas: " + GerenciadorMoedas.Instancia.MoedasSessao;
        textoLikes.text = "Likes: " + GerenciadorMoedas.Instancia.LikesSessao;
        textoSeguidores.text = "Seguidores: " + GerenciadorMoedas.Instancia.SeguidoresTotal;
        textoRecord.text = "Record: " + GerenciadorMoedas.Instancia.MelhorPontuacao;
    }
}
