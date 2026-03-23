using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoeda : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabDourada;
    public GameObject prefabLike;
    public GameObject prefabSeguidor;

    [Header("Quantidade")]
    public int minMoedas = 2;
    public int maxMoedas = 5;

    [Header("Probabilidade(O a 100)")]
    [Range(0, 100)] public int chanceDourada = 75;
    [Range(0, 100)] public int chanceLike = 25;
    [Range(0, 100)] public int chanceSeguidor = 5;

    [Header ("Area de Spawn")]
    public Vector2 areaMin = new Vector2 (0f, 1f);
    public Vector2 areaMax = new Vector2 (18f, 3f);

    void OnEnable()
    {
        //limpa as moedas do ciclo anterior
        foreach (Transform filho in transform)
        {
            if (filho.GetComponent<Moeda>() != null) Destroy(filho.gameObject);
        }

        SpawnarMoedas();
    }

    void SpawnarMoedas()
    {
        int total = Random.Range(minMoedas, maxMoedas + 1);

        for (int i = 0; i < total; i++)
        {

            GameObject prefab = EscolherPrefab();
            if (prefab == null) continue;

            Vector2 pos = new Vector2(
                    transform.position.x + Random.Range(areaMin.x, areaMax.x),
                    transform.position.y + Random.Range(areaMin.y, areaMax.y)
                );

            GameObject moeda = Instantiate(prefab, pos, Quaternion.identity);
            moeda.transform.SetParent(transform);
        }
    
    }

    GameObject EscolherPrefab()
    {
        //pra manter as chances somadas sempre 100
        int total = chanceDourada + chanceLike + chanceSeguidor;
        if (total == 0) return null;

        int sorteio = Random.Range(0, total);

        if (sorteio < chanceDourada) return prefabDourada;
        if (sorteio < chanceDourada + chanceLike) return prefabLike;
        return prefabSeguidor;
    }


    //facilita ajustes  da area de spawn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 centro = new Vector3(
                transform.position.x + (areaMin.x + areaMax.x) / 2f,
                transform.position.y + (areaMin.y + areaMax.y) / 2f, 
                0f
            );
        Vector3 tamanho = new Vector3(
                areaMax.x - areaMin.x,
                areaMax.y - areaMin.y,
                0f
            );

        Gizmos.DrawWireCube(centro, tamanho);
    }

}
