using System.Collections.Generic;
using UnityEngine;

public class RandomPaperPlacer : MonoBehaviour
{
    [Header("Possible Locations")]
    public List<GameObject> possibleLocations = new List<GameObject>();

    [Header("Paper Prefab")]
    public GameObject paperPrefab;

    [Header("Number of Papers to Place")]
    public int numberOfPapersToPlace = 8;

    [Header("Placement Offsets")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private void Start()
    {
        PlaceRandomPapers();
    }

    private void PlaceRandomPapers()
    {
        if (possibleLocations.Count < numberOfPapersToPlace)
        {
            Debug.LogError("Not enough locations to place the papers.");
            return;
        }

        // Shuffle the list of possible locations
        List<GameObject> shuffledLocations = new List<GameObject>(possibleLocations);
        ShuffleList(shuffledLocations);

        // Place papers at the first 'numberOfPapersToPlace' locations
        for (int i = 0; i < numberOfPapersToPlace; i++)
        {
            PlacePaper(shuffledLocations[i]);
        }
    }

    private void PlacePaper(GameObject location)
    {
        Vector3 adjustedPosition = location.transform.position + location.transform.TransformDirection(positionOffset);
        Quaternion adjustedRotation = location.transform.rotation * Quaternion.Euler(rotationOffset);

        Instantiate(paperPrefab, adjustedPosition, adjustedRotation);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
