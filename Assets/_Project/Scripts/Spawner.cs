using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public int numberOfPickups = 0;

    private float minX = -10.8f;
    private float maxX = 5.0f;
    private float minZ = -3.8f;
    private float maxZ = 14.7f;

    private Color[] colors = { Color.red, Color.green, Color.blue, new Color(0.5f, 0f, 0.5f) };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 22; i++)
        {
            SpawnPickup(new Color(0.5f, 0f, 0.5f));
        }

        for (int i = 22; i < numberOfPickups; i++)
        {
            SpawnPickup(colors[Random.Range(0, colors.Length)]);
        }
    }

    void SpawnPickup(Color color)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, 0.2f, randomZ);

        GameObject pickup = Instantiate(pickupPrefab, randomPosition, Quaternion.identity);

        Renderer renderer = pickup.GetComponent<Renderer>();
        renderer.material.color = colors[Random.Range(0, colors.Length)];
    }
}
