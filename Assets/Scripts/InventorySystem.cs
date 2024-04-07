using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }//diðer scriplerden eriþim

    public GameObject inventoryScreenUI;
    public bool isOpen;
    //public bool isFull;
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupAlertText;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotsToEquip;

    private void Awake() //diðer scriplerden eriþim
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
    void TriggerPopUp(string itemName)
    {
        pickupAlert.SetActive(true);
        pickupAlertText.text = itemName + " picked up!";
    }

    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)// inv slotlarýnýn üzerinde gezip liste eklemek için
        {
            if (child.CompareTag("Slot"))//tag ile diger objeleri es geçiyoruz
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

        TriggerPopUp(itemName);
        itemList.Add(itemName);
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
        if (counter == 4) // envanter slot sayýsý dolana kadar döndür
        {
            return true;//18 slot doluysa true dondür
        }
        else
        {
            return false;//boþsa false döndür
        }
    }

    private GameObject FindNextEmptySlot()//listede slot aranýr slot child yoksa empty demek degeri dondurur
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