using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Configuracoes : MonoBehaviour
{

    public Slider sliderMusica;
    public Slider sliderEfeitos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("vol_musica", 1f);
        sliderEfeitos.value = PlayerPrefs.GetFloat("vol_efeitos", 1f);
    }

    public void AlterarMusica(float valor)
    {
        PlayerPrefs.SetFloat("vol_musica",  valor);
    }


    public void AlterarEfeitos(float valor)
    {
        PlayerPrefs.SetFloat("vol_efeitos", valor);
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
