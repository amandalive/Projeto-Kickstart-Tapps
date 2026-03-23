using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorSeguidores : MonoBehaviour
{
    
    public static GerenciadorSeguidores Instancia {  get; private set; }

    [Header("Referencias")]
    public Transform jogador;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por fase")]
    public Sprite spritePequeno; // até 10 seguidores
    public Sprite spriteMedio; // de 10 a 25 seguidores
    public Sprite spriteGrande; // mais que 25 seguidores

    [Header("Perseguicao")]
    public float distanciaInicial = 10f; //distancia inicial da horda
    public float velocidadeNormal = 4f; //velocidade normal da horda
    public float velocidadeImpulso = 8f; //velociade quando o jogador bater num obstáculo
    public float duracaoImpulso = 1.5f; //tempo que a velocidade extra funciona

    private int quantidadeSeguidores = 0;
    private float timerImpulso = 0f;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {

            Destroy(gameObject);
            return;
        }

        Instancia = this;

    }

    void Start()
    {
            transform.position = new Vector3(jogador.position.x - distanciaInicial, jogador.position.y, 0f);
            AtualizarSprite();
    }

    
    void Update()
    {
            float velocidade = velocidadeNormal;

            if (timerImpulso > 0f)
            {
                velocidade = velocidadeImpulso;
                timerImpulso -= Time.deltaTime;
            }

            transform.position = new Vector3(transform.position.x + velocidade * Time.deltaTime, jogador.position.y, 0f);

    }

    //Chamada de quando o jogador bate em um obstáculo
    public void AtivarImpulso()
    {
        timerImpulso = duracaoImpulso;
    }

    public void AdicionarSeguidor()
    {
        quantidadeSeguidores++;
        AtualizarSprite();
    }

    void AtualizarSprite()
    {
        if (quantidadeSeguidores <= 10) spriteRenderer.sprite = spritePequeno;

        else if (quantidadeSeguidores <= 25) spriteRenderer.sprite = spriteMedio;

        else spriteRenderer.sprite = spriteGrande;
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Jogador")) GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }
}
