using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private List<ItemObject> ItemSpawnPool;

    public GameObject player;
    public GameObject cannonBalls;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    Quaternion playerPos;
    public int maxItems = 30;
    bool isMaxItems;
    static int numOfItems = 0;
    Camera cam;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        SpawnRocks(15);
        StartCoroutine(SpawnCannonBalls());
    }

    // Update is called once per frame
    void Update() {
        if (numOfItems < maxItems) {
            int x = Random.Range(-19, 19);
            int y = Random.Range(-19, 19);
            SpawnItem(new Vector2(x, y));
            numOfItems++;
        }
    }

    private void SpawnRocks(int count) {
        for (int i = 0; i < count; i++) {
            int x = Random.Range(-19, 19);
            int y = Random.Range(-19, 19);
            int rockType = Random.Range(0, 3);
            GameObject rock = rockType == 0 ? rock1 : rockType == 1 ? rock2 : rock3;
            Instantiate(rock, new Vector3(x, y, 0), rock.transform.localRotation);
        }
    }


    IEnumerator SpawnCannonBalls() {
        while (1 == 1) {

            yield return new WaitForSeconds(5);

            int y = Random.Range(-19, 19);
            int x = Random.Range(-19, 19);
            SpawnCannonBall(new Vector3(22, y, 0));
            SpawnCannonBall(new Vector3(-22, y, 0));
            SpawnCannonBall(new Vector3(x, 22, 0));
            SpawnCannonBall(new Vector3(x, -22, 0));
        }
    }

    private void SpawnCannonBall(Vector2 direction) {
        GameObject go = Instantiate(cannonBalls, direction, cannonBalls.transform.localRotation);
        go.GetComponent<CannonStats>();
    }

    private void SpawnItem(Vector2 position) {
        int itemPoolIndex =  Random.Range(0, ItemSpawnPool.Count);
        ItemObject item = ItemSpawnPool[itemPoolIndex];
        GameObject go = Instantiate(item.ItemPrefab, position, Quaternion.identity);
        go.GetComponent<ItemController>().Initialize(item);
    }

}
