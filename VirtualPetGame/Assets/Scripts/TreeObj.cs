using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObj : MonoBehaviour
{
    public string NameOfTree;

    public int TreeType;

    public int numFruitNodes;

    public Fruit fruitType;

    public float fruitSpawn_minX, fruitSpawn_maxX, fruitSpawn_minY, fruitSpawn_maxY;

    public List<GameObject> fruitNodes;

    public bool isInspected;

    public bool canSpawnChao;
    public GameObject ChaoPrefab;
    public float timer;
    public GameObject NOTICE;



    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        NOTICE.SetActive(false);

        FindFruitNodes(); // Create list of all fruitnodes in children, and deactivate them all
    }

    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - 1.5f));

        if (transform.position.y > 0.5f)
        {
            transform.localScale = new Vector3(3.5f,3.5f,1);
        }
        else if (transform.position.y > -1)
        {
            transform.localScale = new Vector3(3.75f, 3.75f, 1);
        }
        else if (transform.position.y > -2f)
        {
            transform.localScale = new Vector3(4f, 4f, 1);
        }
        else if (transform.position.y > -3f)
        {
            transform.localScale = new Vector3(4.25f, 4.25f, 1);
        }
        else if (transform.position.y > -4f)
        {
            transform.localScale = new Vector3(4.5f, 4.5f, 1);
        }
        else if (transform.position.y > -5)
        {
            transform.localScale = new Vector3(5f, 5f, 1);
        }





        if (timer < 100)
        {
            timer += Time.deltaTime * 1f;
            canSpawnChao = false;
            NOTICE.SetActive(false);
        }
        else
        {
            timer = 100;
            canSpawnChao = true;
            NOTICE.SetActive(true);
        }
    }

    public void SpawnChao()
    {
        if (canSpawnChao)
        {
            Instantiate(ChaoPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 1.5f, this.transform.position.z), Quaternion.identity);
            timer = 0;
        }
    }

    void FindFruitNodes()
    {
        for (int i = 0; i<this.transform.childCount; i++)
         {
             Transform child = this.transform.GetChild(i);
             if (child.tag == "FruitNode")
             {
                fruitNodes.Add(child.gameObject);
                numFruitNodes++;
             }
             /*if (child.childCount > 0)
             {
                 GetChildObject(child, _tag);
             }*/
         }
    }
}
