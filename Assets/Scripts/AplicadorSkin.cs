using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplicadorSkin : MonoBehaviour
{
    public RuntimeAnimatorController[] animatorControllers;

    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        AplicarSkin();
    }

    void AplicarSkin()
    {
        int id = GerenciadorLoja.ObterInstancia().SkinSelecionada();

        if (id < animatorControllers.Length && animatorControllers[id] != null)
                animator.runtimeAnimatorController = animatorControllers[id];
    }
}
