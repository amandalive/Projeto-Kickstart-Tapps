using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform;
    private float ultimaPosX;
    private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
       
        ultimaPosX = cameraTransform.position.x;
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }
    void LateUpdate()
    {
        if (cameraTransform == null) return;

        float delta = cameraTransform.position.x - ultimaPosX;
        ultimaPosX = cameraTransform.position.x;

        

        foreach (ParallaxLayer layer in parallaxLayers)
            layer.Move(delta);
    }
}