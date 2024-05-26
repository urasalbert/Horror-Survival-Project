using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; } // Diðer scriptlerden eriþim

    public GameObject inventoryScreenUI;
    public bool isOpen;
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupAlertText;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotsToEquip;

    public AudioSource BagOpenAndClose;
    public AudioClip BagClose, BagOpen;

    public float fadeDuration = 3.0f; // Fade süresi

    private void Awake()
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
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            BagOpenAndClose.clip = BagOpen;
            BagOpenAndClose.Play();
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            isOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            BagOpenAndClose.clip = BagClose;
            BagOpenAndClose.Play();
            inventoryScreenUI.SetActive(false);
            isOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddToInventory(string itemName)
    {
        if (CheckIfFull())
        {
            Debug.Log("Inventory is full!");
            return;
        }

        whatSlotsToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotsToEquip.transform.position, whatSlotsToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotsToEquip.transform);

        StartCoroutine(FadeOutText(itemName)); // FadeOutText fonksiyonunu çaðýr
        itemList.Add(itemName);
    }

    IEnumerator FadeOutText(string itemName)
    {
        pickupAlert.SetActive(true);
        pickupAlertText.text = itemName + " picked up!";

        float startAlpha = 1.0f;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        Color originalColor = pickupAlertText.color;
        while (progress < 1.0f)
        {
            Color tempColor = originalColor;
            tempColor.a = Mathf.Lerp(startAlpha, 0, progress);
            pickupAlertText.color = tempColor;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        pickupAlert.SetActive(false);
        pickupAlertText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);
    }

    public bool CheckIfFull()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return false; // Boþ slot varsa envanter dolu deðil
            }
        }
        return true; // Boþ slot yoksa envanter dolu
    }

    private GameObject FindNextEmptySlot()
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
