using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    public Transform posJogador;
    public Vector3 offset;
    public float suavizacaoMov = 8f;

    public void LateUpdate()
    {
       float targetX = posJogador.position.x + offset.x;

        float novoX = Mathf.Lerp(transform.position.x, targetX, 1f - Mathf.Exp(-suavizacaoMov * Time.deltaTime));

        transform.position = new Vector3(novoX, transform.position.y, offset.z);
    }
}
