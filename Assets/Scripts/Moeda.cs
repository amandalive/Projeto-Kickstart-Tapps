using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
   public enum TipoMoeda { Dourada, Like, Seguidor }
   
   public TipoMoeda tipo = TipoMoeda.Dourada;


   void Start()
   {
       Vector3 posicaoNova = transform.position;

       posicaoNova.z = 0;

       transform.position = posicaoNova;
   }

   void OnTriggerEnter2D(Collider2D outro)
   {
       if (outro.CompareTag("Jogador"))
       {
           GerenciadorMoedas.ObterInstancia().AdicionarMoeda(tipo);

           if (tipo == TipoMoeda.Seguidor && GerenciadorSeguidores.Instancia != null)
               GerenciadorSeguidores.Instancia.AdicionarSeguidor();

           Destroy(gameObject);
       }
   }
}
