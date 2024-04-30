using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperCollector : MonoBehaviour
{
    public int collectedPaperCount = 0;
    public int totalPaperCount = 3;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper"))
        {
            collectedPaperCount += 1;
            Debug.Log(collectedPaperCount);
            Destroy(other.gameObject);
        }   
    }

    private void Update()
    {
        if (collectedPaperCount >= totalPaperCount)
        {
            //game finisher here
        }
    }
}
