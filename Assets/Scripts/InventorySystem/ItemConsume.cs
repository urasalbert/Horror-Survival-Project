using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemConsume : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrashable;

    public string thisName, thisDescription, thisFunctionality;

    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;

    public void OnPointerDown(PointerEventData eventData)
    {
        //rmb item use
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PlayerState.Instance.currentHealth == PlayerState.Instance.maxHealth)
            {
                Debug.Log("You are already at full hp!");
            }
            else
            {
                if (isConsumable)
                {
                    HealEffect.Instance.PlayHealSound();
                    itemPendingConsumption = gameObject;
                    consumingFunction(healthEffect);
                }
            }

        }
    }
    //after rmb item destroy
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {

                if (isConsumable && itemPendingConsumption ==gameObject)
                {
                        DestroyImmediate(gameObject);//fast destroy
                }
            

        }
    }

    private void consumingFunction(float healthEffect)
    {
        healthEffectCalculation(healthEffect);
    }


    private static void healthEffectCalculation(float healthEffect)
    {

        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if (healthEffect != 0)
        { 
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }

        }
    }
}

