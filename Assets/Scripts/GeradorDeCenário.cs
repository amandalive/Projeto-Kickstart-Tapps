using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeCenário : MonoBehaviour
{
    [Header("Referencia")]
    public Transform jogador;

    [Header("Conjunto Inicial")]
    public GameObject[] conjuntoInicial;

    [Header("Conjuntos aleatorios por tempo")]
    public FaixaTempo[] faixas;

    [Header("Configuracoes")]
    public int conjuntosVisiveis = 4;    //quantos conjuntos ficam ativos por vez (refinar para otimização)
    public float distanciaCiclagem = 20f;  //distancia que o conjunto chega antes de ser retirado

    private List<Conjuntos> conjuntosAtivos = new List<Conjuntos>();
    private Queue<GameObject> pool = new Queue<GameObject>();
    private float proxPosX = 0f;
    private int indiceConjuntoInicial = 0;
    private float tempoDeJogo = 0f;    
    private int ultimoConjuntoIndex = -1;


    void Start()
    {
        
        for (int i = 0; i < conjuntosVisiveis; i++) {

            SpawnarProximoConjunto();        
        }
    }


    void Update()
    {
        tempoDeJogo += Time.deltaTime;

        // Se o jogador está chegando perto do último conjunto, spawna mais
        if (conjuntosAtivos.Count > 0) { 

            Conjuntos ultimo = conjuntosAtivos[conjuntosAtivos.Count - 1];
            float bordaDireita = ultimo.transform.position.x + ultimo.largura;

            if (jogador.position.x + distanciaCiclagem > bordaDireita) {

                SpawnarProximoConjunto();
            }
        }

        // cicla conjuntos que ficaram muito para trás
        if (conjuntosAtivos.Count > 0)
        {
            Conjuntos primeiro = conjuntosAtivos[0];
            float bordaDireita = primeiro.transform.position.x + primeiro.largura;

            if(bordaDireita < jogador.position.x - distanciaCiclagem)
            {
                CiclagemConjunto(primeiro);
            }
        }
    }


    void SpawnarProximoConjunto()
    {
        GameObject prefab = EscolherPrefab();
        if (prefab == null) return;

        GameObject obj = ObterDaPool(prefab);
        obj.transform.position = new Vector3(proxPosX, 0f, 0f);
        obj.SetActive(true);

        Conjuntos conjunto = obj.GetComponent<Conjuntos>();
        proxPosX += conjunto.largura;
        conjuntosAtivos.Add(conjunto);
    }
    
    GameObject EscolherPrefab()
    {
        if (indiceConjuntoInicial < conjuntoInicial.Length)
            return conjuntoInicial[indiceConjuntoInicial++];

        FaixaTempo faixaAtual = null;

        for(int i = faixas.Length -1; i >=0; i--)
        {
            if (tempoDeJogo >= faixas[i].tempoInicio)
            {
                faixaAtual = faixas[i];
                break;
            }
        }

        if (faixaAtual == null || faixaAtual.conjuntos.Length == 0) return null;

        int index;
        do
        {
            index = Random.Range(0, faixaAtual.conjuntos.Length);
        } while (faixaAtual.conjuntos.Length > 1 && index == ultimoConjuntoIndex);

        ultimoConjuntoIndex = index;
        return faixaAtual.conjuntos[index];             
    }

    void CiclagemConjunto(Conjuntos conjunto)
    {
        conjuntosAtivos.RemoveAt(0);
        conjunto.gameObject.SetActive(false);
        pool.Enqueue(conjunto.gameObject);
    }

    GameObject ObterDaPool(GameObject prefab)
    {
        //reutiliza objeto da pool se estiver disponível
        while (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            if (obj != null) return obj;
        }

        //senão, cria outro
        return Instantiate(prefab);
    }
}

[System.Serializable]
public class FaixaTempo
{
    public string nome;
    public float tempoInicio;
    public GameObject[] conjuntos;
}