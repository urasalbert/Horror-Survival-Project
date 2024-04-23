using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacePlayer : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni

    void Update()
    {
        // Eðer oyuncu tanýmlanmýþsa
        if (player != null)
        {
            // Oyuncunun pozisyonuna doðru bak
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Yalnýzca yatay ekseni döndür (isteðe baðlý)
            rotation.x = 0;
            rotation.z = 0;

            // Dönme iþlemi
            transform.rotation = rotation;
        }
    }
}
