using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNode : MonoBehaviour
{
    public Fruit Fruit1;
    public Fruit Fruit2;
    public Fruit Fruit3;
    public Sprite fruitSprite;
    public Sprite flowerSprite;
    public Sprite waterSprite;
    public Sprite evilSprite;

    public GameObject notReadySprite;
    private SpriteRenderer notReadySp;

    public int type;
    private Color unripeColor_1;
    private Color unripeColor_2;
    private Color unripeColor_3;
    private Color unripeColor_4;
    private Color myColor;

    private Color transparentColor;

    public float minTimeBtwnSpawn, maxTimeBtwnSpawn;
    public float TimeBetweenSpawn;
    private float spawnTimer;

    public float growthRate;

    public TreeObj parentTree;
    private Fruit fruitToSpawn;

    public bool canBeHarvested;
    public bool fruitHasSpawned;
    public bool isHoldingFruit;

    SpriteRenderer sp;

    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
        parentTree = this.transform.parent.gameObject.GetComponent<TreeObj>();
        //notReadySp = notReadySprite.GetComponent<SpriteRenderer>();



        canBeHarvested = false;
        isHoldingFruit = false;
        fruitHasSpawned = false;

        unripeColor_1 = new Color(0.289f, 0.943f, 0.398f, 1);
        unripeColor_2 = new Color(0.302f, 0.489f, 0.877f, 1);
        unripeColor_3 = new Color(0.745f, 0.291f, 0.338f, 1);
        unripeColor_4 = new Color(0.448f, 0.745f, 0.291f, 1);
        transparentColor = new Color(1, 1, 1, 0);
        //notReadySp.color = transparentColor;


        if (parentTree.TreeType == 1)
        {
            // 1111111111

            minTimeBtwnSpawn = 10;
            maxTimeBtwnSpawn = 30;
            growthRate = 0.2f;

            sp.sprite = fruitSprite;
            myColor = unripeColor_1;

        }
        else if (parentTree.TreeType == 2)
        {
            // 222222222222222

            minTimeBtwnSpawn = 10;
            maxTimeBtwnSpawn = 30;
            growthRate = 0.2f;

            sp.sprite = flowerSprite;
            myColor = unripeColor_2;
        }
        else if (parentTree.TreeType == 3)
        {
            // 3333333333333

            minTimeBtwnSpawn = 10;
            maxTimeBtwnSpawn = 15;
            growthRate = 0.1f;

            sp.sprite = waterSprite;
            myColor = unripeColor_3;

        }
        else if (parentTree.TreeType == 4)
        {
            // 44444444444444

            minTimeBtwnSpawn = 10;
            maxTimeBtwnSpawn = 30;
            growthRate = 0.2f;

            sp.sprite = evilSprite;
            myColor = unripeColor_4;

        }


        type = parentTree.TreeType;
        sp.color = myColor;
        TimeBetweenSpawn = Random.Range(minTimeBtwnSpawn, maxTimeBtwnSpawn);
        spawnTimer = TimeBetweenSpawn;

    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.1f);

        if (fruitHasSpawned)
        {
            sp.enabled = true;
            if (this.transform.localScale.x < 0.395f)
            {
                this.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.4f, 0.4f, 1), growthRate * Time.deltaTime);
            }
            else
            {
                this.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                sp.color = Color.white;
                isHoldingFruit = true;
            }
        }
        else
        {
            this.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            sp.color = myColor;
            sp.enabled = false;
            isHoldingFruit = false;
            ManageSpawnFruit();
        }
    }

    void ManageSpawnFruit()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            if (!isHoldingFruit)
            {
                fruitHasSpawned = true;
            }
            spawnTimer = TimeBetweenSpawn;
        }
    }

    public void NotReady()
    {
        notReadySp.color = Color.white;
    }
}
