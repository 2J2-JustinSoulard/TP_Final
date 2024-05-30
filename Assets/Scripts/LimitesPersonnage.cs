using System.Collections.Generic;  // Removed System.Collections as it's not used
using UnityEngine;

public class LimitesPersonnage : MonoBehaviour
{
    public float limiteGauche1;
    public float limiteDroite1;
    public float limiteHaut;
    public float limiteBas;

    void Update()
    {
        Vector3 position = transform.position;  // Renamed variable to position for clarity

        // Check vertical limits
        if (position.y >= limiteHaut)
        {
            position.y = limiteHaut;
        }
        else if (position.y <= limiteBas)
        {
            position.y = limiteBas;
        }

        // Check horizontal limits
        if (position.x < limiteGauche1)
        {
            position.x = limiteGauche1;
        }
        else if (position.x > limiteDroite1)
        {
            position.x = limiteDroite1;
        }

        // Update the position of the GameObject
        transform.position = position;
    }
}
