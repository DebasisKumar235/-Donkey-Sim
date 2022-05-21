using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TreeShadowRemover : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject dirLight;
    private Light lig;

    void Start()
    {
        dirLight= GameObject.Find ("Directional Light");
        lig =dirLight.GetComponent<Light>();

    }

    public void setNoShadows(){
       lig.shadows=LightShadows.None;

    }
    public void setShadows(){
       lig.shadows=LightShadows.Soft;

    }

}
