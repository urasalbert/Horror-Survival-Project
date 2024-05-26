using System.Collections;
using UnityEngine;

public class PaperCollector : MonoBehaviour
{
    public static PaperCollector Instance { get; set; }


    public int collectedPaperCount = 0;
    public int totalPaperCount = 2;
    [SerializeField] private GameObject Player;
    [SerializeField] private CanvasGroup BlackCanvas;
    Vector3 teleportDestination = new Vector3(-122.45f, 1f, 371.19f);
    [SerializeField] private CharacterController characterController;
    private bool characterControllerEnabled = true;
    bool isTeleported;

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
    private void Start()
    {
        BlackCanvas.alpha = 0;
        isTeleported = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            collectedPaperCount++;
            PaperCountText.Instance.UpdateScoreText();
            Debug.Log("Collected Paper Count: " + collectedPaperCount);
            Destroy(other.gameObject);
            PaperCollectionSound.Instance.PlayPaperSound();
        }
    }


    private void Update()
    {
        if (collectedPaperCount >= totalPaperCount && !isTeleported)
        {
            StartCoroutine(EndGameBlackScreenAndTeleportation());
        }
    }
    IEnumerator EndGameBlackScreenAndTeleportation()
    {
        isTeleported = true;
        Blink.Instance.ShowIU();
        yield return new WaitForSeconds(1f);
        Blink.Instance.HideUI();
        characterControllerEnabled = false;
        yield return new WaitForSeconds(0.01f);
        Player.transform.position = teleportDestination;
        yield return new WaitForSeconds(0.01f);
        characterControllerEnabled = true;
    }

    private void FixedUpdate()
    {
        characterController.enabled = characterControllerEnabled;
    }
}
