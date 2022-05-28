using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime=0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
