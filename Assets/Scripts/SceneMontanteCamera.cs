using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMontanteCamera : MonoBehaviour
{

    public float limiteBas;
    public float limiteHaut;

    public GameObject Ninja;

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = transform.position;


        if (Ninja.transform.position.y > camPosition.y)
        {
            camPosition.y = Ninja.transform.position.y;
        }
        if (Ninja.transform.position.y < camPosition.y)
        {
            camPosition.y = Ninja.transform.position.y;
        }

        if (camPosition.y < limiteBas)
        {
            camPosition.y = limiteBas;
        }
        if (camPosition.y > limiteHaut)
        {
            camPosition.y = limiteHaut;
        }


        transform.position = camPosition;
    }
}
