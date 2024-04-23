using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacePlayer : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bile�eni

    void Update()
    {
        // E�er oyuncu tan�mlanm��sa
        if (player != null)
        {
            // Oyuncunun pozisyonuna do�ru bak
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Yaln�zca yatay ekseni d�nd�r (iste�e ba�l�)
            rotation.x = 0;
            rotation.z = 0;

            // D�nme i�lemi
            transform.rotation = rotation;
        }
    }
}
