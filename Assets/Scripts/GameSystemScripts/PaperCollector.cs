using System.Collections;
using UnityEngine;

public class PaperCollector : MonoBehaviour
{
    public int collectedPaperCount = 0;
    public int totalPaperCount = 2;
    [SerializeField] private GameObject Player;
    [SerializeField] private CanvasGroup BlackCanvas;
    Vector3 teleportDestination = new Vector3(-122.45f, 1f, 371.19f);
    [SerializeField] private CharacterController characterController;
    private bool characterControllerEnabled = true;
    bool isTeleported;

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
            Debug.Log("Collected Paper Count: " + collectedPaperCount);
            Destroy(other.gameObject);
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
