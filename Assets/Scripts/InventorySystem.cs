using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }//di�er scriplerden eri�im

    public GameObject inventoryScreenUI;
    public bool isOpen;
    //public bool isFull;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotsToEquip;

    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    private void Awake() //di�er scriplerden eri�im
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)// inv slotlar�n�n �zerinde gezip liste eklemek i�in
        {
            if (child.CompareTag("Slot"))//tag ile diger objeleri es ge�iyoruz
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            isOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotsToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotsToEquip.transform.position, whatSlotsToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotsToEquip.transform);

        itemList.Add(itemName);
        TriggerPickUpPopUp(itemName);
    }


    void TriggerPickUpPopUp(string itemName)
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName + " picked up!";
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        if (counter == 10) // envanter slot say�s� dolana kadar d�nd�r
        {
            return true;//4 slot doluysa true dond�r
        }
        else
        {
            return false;//bo�sa false d�nd�r
        }
    }

    private GameObject FindNextEmptySlot()//listede slot aran�r slot child yoksa empty demek degeri dondurur
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
}