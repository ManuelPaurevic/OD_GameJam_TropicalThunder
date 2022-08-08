using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private ItemObject coconutItemObject;

    [SerializeField]
    private PlayerStats playerStats;

    public GameObject player;
    public GameObject cannonBalls;
    Quaternion playerPos;
    public int maxCoconuts = 30;
    bool isMaxCoconuts;
    static int numOfCoconuts = 0;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(SpawnCannonBalls());
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfCoconuts < maxCoconuts){
            int x = Random.Range(-19, 19);
            int y = Random.Range(-19, 19);
            SpawnCoconut(new Vector2(x,y));
            numOfCoconuts++;
        }
    }


    IEnumerator SpawnCannonBalls(){
        while(1 == 1){
             
            yield return new WaitForSeconds(5);

            int y = Random.Range(-19, 19);
            int x = Random.Range(-19, 19);
            SpawnCannonBall(new Vector3(22,y,0));
            SpawnCannonBall(new Vector3(-22,y,0));
            SpawnCannonBall(new Vector3(x,22,0));
            SpawnCannonBall(new Vector3(x,-22,0));
        }
    }

    private void SpawnCannonBall(Vector2 direction) {
        GameObject go = Instantiate(cannonBalls, direction, cannonBalls.transform.localRotation);
        go.GetComponent<CannonStats>().Initialize(playerStats);
    }

    private void SpawnCoconut(Vector2 position) {
        GameObject go = Instantiate(coconutItemObject.ItemPrefab, position, Quaternion.identity);
        go.GetComponent<ItemController>().Initialize(coconutItemObject);
    }

}
