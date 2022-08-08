using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cannonBalls;
    public GameObject coconuts;
    Quaternion playerPos;
    public int maxCoconuts = 30;
    bool isMaxCoconuts;
    static int numOfCoconuts = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCannonBalls());
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfCoconuts < maxCoconuts){
            int x = Random.Range(-19, 19);
            int y = Random.Range(-19, 19);
            Instantiate(coconuts, new Vector3(x,y,0), cannonBalls.transform.localRotation);
            numOfCoconuts++;
        }
    }


    IEnumerator SpawnCannonBalls(){
        while(1 == 1){
            yield return new WaitForSeconds(1);

            //Vector3 playerPos = player.transform.position;
            Instantiate(cannonBalls, new Vector3(0,0,0), cannonBalls.transform.localRotation);
        }
    }

}
