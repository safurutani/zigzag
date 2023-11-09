using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefabCenter;
    public GameObject roadPrefabRight;
    public GameObject roadPrefabLeft;
    public float offset = 0.7071068f;
    public Vector3 lastPosition;
    public int roadRotation = 45;
    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.5f);
    }
    public  void CreateNewRoadPart()
    {
        Vector3 spawnPos = Vector3.zero;
        float chance = Random.Range(0, 100);
        GameObject newRoad;
        if (chance < 50 )
        {
            spawnPos = new Vector3(lastPosition.x + offset, lastPosition.y, lastPosition.z + offset);
            roadRotation = 45;
        }
        else
        {
            spawnPos = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z + offset);
            roadRotation = -45;
        }
        if(chance < 33)
        {
            newRoad = roadPrefabCenter;
        }
        else if (chance > 66)
        {
            newRoad = roadPrefabLeft;
        }
        else
        {
            newRoad = roadPrefabRight;
        }

        GameObject road = Instantiate(newRoad, spawnPos, Quaternion.Euler(0, roadRotation, 0));
        lastPosition = road.transform.position;

        if(chance % 3 == 0)
        {
            road.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
