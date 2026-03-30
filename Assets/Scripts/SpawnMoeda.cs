using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoeda : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabDourada;
    public GameObject prefabLike;
    public GameObject prefabSeguidor;

    [Header("Trilhas de Moedas")]
    public Trilha[] trilhas;

    [Header("Probabilidade de spawnar cada trilha (O a 100)")]
    [Range(0, 100)] public int chanceTrilha = 80;

    void OnEnable()
    {
        LimparMoedas();
        SpawnarTrilhas();
    }

    void LimparMoedas()
    {
        foreach(Transform filho in transform)
            if(filho.GetComponent<Moeda>() != null) Destroy(filho.gameObject);
    }

    void SpawnarTrilhas()
    {
        foreach(Trilha trilha in trilhas)
        {
            if (Random.Range(0, 100) > chanceTrilha) continue;

            foreach (PontoMoeda ponto in trilha.pontos)
            {
                GameObject prefab = EscolherPrefab(ponto.tipo);
                if (prefab == null) continue;

                Vector3 pos = new Vector3(
                    transform.position.x + ponto.posicaoX, ponto.posicaoY, 0f);

                GameObject moeda = Instantiate (prefab, pos, Quaternion.identity);
                moeda.transform.SetParent(transform);
            }
        }
    }

    GameObject EscolherPrefab(Moeda.TipoMoeda tipo)
    {
        switch (tipo)
        {
            case Moeda.TipoMoeda.Dourada: return prefabDourada;
            case Moeda.TipoMoeda.Like : return prefabLike;
            case Moeda.TipoMoeda.Seguidor: return prefabSeguidor;
            default: return prefabDourada;
        }
    }


    //facilita ajustes  da area de spawn
    void OnDrawGizmosSelected()
    {
        if (trilhas == null) return;

        Color[] cores = { Color.yellow, Color.cyan, Color.green };

        for (int t = 0; t < trilhas.Length; t++)
        {
            Gizmos.color = cores[t % cores.Length];

            foreach(PontoMoeda ponto in trilhas[t].pontos)
            {
                Vector3 pos = new Vector3(
                    transform.position.x + ponto.posicaoX, ponto.posicaoY, 0f);

                Gizmos.DrawSphere(pos, 0.3f);
            }

            for (int i = 0; i < trilhas[t].pontos.Length - 1; i++)
            {
                Vector3 a = new Vector3(
                    transform.position.x + trilhas[t].pontos[i].posicaoX,
                    trilhas[t].pontos[i].posicaoY, 0f);
                Vector3 b = new Vector3(
                    transform.position.x + trilhas[t].pontos[i + 1].posicaoX,
                    trilhas[t].pontos[i + 1].posicaoY, 0f);
                Gizmos.DrawLine(a, b);
            }
        }
    }
}

[System.Serializable]
public class Trilha
{
    public string nome;
    public PontoMoeda[] pontos;
}

[System.Serializable]
public class PontoMoeda
{
    public float posicaoX;
    public float posicaoY;
    public Moeda.TipoMoeda tipo = Moeda.TipoMoeda.Dourada;
}