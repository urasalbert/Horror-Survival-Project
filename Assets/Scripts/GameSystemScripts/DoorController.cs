using UnityEngine;

public class DoorController : MonoBehaviour
{
    //singleton access
    public static DoorController Instance { get; private set; }

    public float rotationSpeed = 2f; 
    public Vector3 closedRotationEulerAngles; 

    private Quaternion closedRotation; 
    private bool isRotating = false; 

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
        //calculate with euler
        closedRotation = Quaternion.Euler(closedRotationEulerAngles);
    }

    private void Update()
    {

        if (isRotating)
        {
            //rotate door slowly
            transform.rotation = Quaternion.RotateTowards(transform.rotation, closedRotation, rotationSpeed * Time.deltaTime);

            // check door closed
            if (transform.rotation == closedRotation)
            {
                isRotating = false;
            }
        }
    }

    // method for closing door
    public void CloseDoor()
    {
        isRotating = true; 
    }
}
