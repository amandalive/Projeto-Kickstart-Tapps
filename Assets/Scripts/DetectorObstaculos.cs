using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorObstaculos : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Obstaculo")){
            GerenciadorSeguidores.Instancia.AtivarImpulso();
        }
    }
}
