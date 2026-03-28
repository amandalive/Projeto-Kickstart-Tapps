using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovJogador : MonoBehaviour
{

    public Rigidbody2D jogadorRB;
    public float velocidade;
    public float input;
    public float forcaPulo;

    public LayerMask camadaChao;
    private bool noChao;
    public Transform posicaoDosPes;
    public float checagemDePosicao;
    public float tempoPulo = 0.35f;
    public float contadorDeTempoDoPulo;
    private bool pulando;

    [SerializeField] private Animator animacao;

    private bool deveIniciarPulo = false;

    // Update is called once per frame
    void Update()
    {

        noChao = Physics2D.OverlapCircle(posicaoDosPes.position, checagemDePosicao, camadaChao);  //para garantir que o jogador esteja no chão quando pular.
     
        if (noChao == true && Input.GetButtonDown("Jump"))
        {
            deveIniciarPulo = true; //inicia o pulo
            pulando = true;  //impede que o jogador dê pulos duplos, triplos, etc.
            contadorDeTempoDoPulo = tempoPulo;  // seta o cronometro do tempo máximo de subida para que segurar o botão de pulo não faça o jogador subir infinitamente
            animacao.SetBool("pulando", true);
        }

        if (Input.GetButtonUp("Jump"))
        {

            pulando = false;
            animacao.SetBool("pulando", false);
        }

    }

    void FixedUpdate()
    {
        if (deveIniciarPulo == true)
        {

            jogadorRB.velocity = new Vector2(velocidade, forcaPulo);
            deveIniciarPulo = false; //limpa a flag
        }
        else if (pulando && contadorDeTempoDoPulo > 0)
        {
            jogadorRB.velocity = new Vector2(velocidade, forcaPulo);
            contadorDeTempoDoPulo -= Time.fixedDeltaTime;
        }
        else
        {
            jogadorRB.velocity = new Vector2(velocidade, jogadorRB.velocity.y);
        }
    }
}