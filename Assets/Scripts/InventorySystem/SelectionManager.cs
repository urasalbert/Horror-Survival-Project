using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {get; set;}
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;
    public bool onTarget = false;
    public GameObject selectedObject;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if(Instance != null && Instance!=this) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>() && selectionTransform.GetComponent<InteractableObject>().playerInRange)
            {
                onTarget = true;
                selectedObject = selectionTransform.GetComponent<InteractableObject>().gameObject;
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
                onTarget = false;
            }

        }
        else
        {
            interaction_Info_UI.SetActive(false);
            onTarget = false;
        }

    }
}