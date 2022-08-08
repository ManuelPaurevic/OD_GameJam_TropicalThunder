using UnityEngine;

public class CoconutSpawnController : MonoBehaviour
{
    [SerializeField]
    private ItemObject coconutItemObject;

    [SerializeField]
    private float spawnTimer = 0.25f;

    private float currentTimer = 0f;

    void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer) {
            currentTimer = 0f;
            
            // TODO: Need to randomly generate these in the play field;
            SpawnCoconut(Vector2.zero);
        }
    }

    private void SpawnCoconut(Vector2 position) {
        GameObject go = Instantiate(coconutItemObject.ItemPrefab, position, Quaternion.identity);
        go.GetComponent<ItemController>().Initialize(coconutItemObject);
    }
}
