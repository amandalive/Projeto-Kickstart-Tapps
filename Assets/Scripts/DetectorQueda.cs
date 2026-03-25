using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorQueda : MonoBehaviour
{
    public float altMinima = -10f;


    void Update()
    {
        if (transform.position.y < altMinima)
        {
            GameOverManager.Instancia.TriggerGameOver();
        }
    }
}
