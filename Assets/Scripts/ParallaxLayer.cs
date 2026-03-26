using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;

    private Transform[] sprites;
    private float larguraSprite;

    void Start()
    {
        //identifica os sprites filhos
        sprites = new Transform [transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sprites[i] = transform.GetChild(i);
        }

        //pega a largura do primeiro sprite
        if (sprites.Length > 0)
        {
            SpriteRenderer sr = sprites[0].GetComponent<SpriteRenderer>();
            if (sr != null) larguraSprite = sr.bounds.size.x - 0.06f;
        }
    }

    public void Move(float delta)
    {
        //move a layer inteira
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;

        //reposiciona sprites que sumiram na esquerda
        if (larguraSprite == 0) return;

        foreach(Transform sprite in sprites)
        {
            float limitEsquerda = Camera.main.transform.position.x - larguraSprite;

            if (sprite.position.x < limitEsquerda)
            {
                float maiorX = float.MinValue;
                foreach (Transform s in sprites)
                    if (s.position.x > maiorX) maiorX = s.position.x;

                Vector3 pos = sprite.localPosition;
                pos.x += larguraSprite * sprites.Length;
                sprite.localPosition = pos;
            }
        }
    }
}