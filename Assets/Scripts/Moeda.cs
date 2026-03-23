using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
   public enum TipoMoeda { Dourada, Like, Seguidor }
   
   public TipoMoeda tipo = TipoMoeda.Dourada;

   void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Jogador"))
        {
            GerenciadorMoedas.Instancia.AdicionarMoeda(tipo);

            if (tipo == TipoMoeda.Seguidor)
            {
                GerenciadorSeguidores.Instancia.AdicionarSeguidor();
            }

            Destroy(gameObject);
        }
    }
}
