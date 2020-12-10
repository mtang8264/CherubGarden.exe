using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("Chao")]
    public Text nameText;
    public Image gauge1, gauge2, gauge3, gauge4;
    public Text levelText;

    [Header("Tree")]
    public Text treeNameText;
    public Text fruitNodeText;
    public Text treeInfo;

    [Header("World")]
    public Text fruitNumWorld;
    public Text flowerNumWorld;
    public Text waterNumWorld;
    public Text evilFruitNumWorld;
    public Text fruitNumUI;
    public Text flowerNumUI;
    public Text waterNumUI;
    public Text evilFruitNumUI;
    public Image fruitDisabled;
    public Image flowerDisabled;
    public Image waterDisabled;
    public Image evilDisabled;
    public Text ChaoCounter;
    public Text TreeCounter;
    private Chao[] allChao;
    private TreeObj[] allTrees;

    [Header("Settings")]
    public float waterRechargeRate;
    private float waterTimer;

    [Header("Attributes to Change")]
    public string myName;
    public int level;
    public float val1, val2, val3, val4;
    public float GaugeImageLerpSpeed;
    public int fruitCount;
    public int flowerCount;
    public int waterCount;
    public int maxWater;
    public int evilFruitCount;
    public string treeName;
    public int fruitNodeCount;


    void Start()
    {
        fruitCount = 0;
        flowerCount = 0;
        waterCount = 4;
        evilFruitCount = 0;

        waterTimer = waterRechargeRate;
    }

    void Update()
    {
        allTrees = FindObjectsOfType<TreeObj>();
        allChao = FindObjectsOfType<Chao>();
        ChaoCounter.text = "x"+allChao.Length;
        TreeCounter.text = "x" + allTrees.Length;



        ManageUI();
        UpdateStats();
        nameText.text = myName;
        gauge1.fillAmount = Mathf.Lerp(gauge1.fillAmount, (val1 / 10), Time.deltaTime * GaugeImageLerpSpeed);
        gauge2.fillAmount = Mathf.Lerp(gauge2.fillAmount, (val2 / 10), Time.deltaTime * GaugeImageLerpSpeed);
        gauge3.fillAmount = Mathf.Lerp(gauge3.fillAmount, (val3 / 10), Time.deltaTime * GaugeImageLerpSpeed);
        gauge4.fillAmount = Mathf.Lerp(gauge4.fillAmount, (val4 / 10), Time.deltaTime * GaugeImageLerpSpeed);

        treeNameText.text = treeName;
        fruitNodeText.text = "" + fruitNodeCount;

        fruitNumWorld.text = "x " + fruitCount;
        flowerNumWorld.text = "x " + flowerCount;
        waterNumWorld.text = "(" + waterCount + "/" + maxWater + ")";
        evilFruitNumWorld.text = "x " + evilFruitCount;

        fruitNumUI.text = "x" + fruitCount;
        flowerNumUI.text = "x" + flowerCount;
        waterNumUI.text = "(" + waterCount + "/" + maxWater + ")";
        evilFruitNumUI.text = "x" + evilFruitCount;


        if (fruitCount < 1)
            fruitDisabled.enabled = true;
        else
            fruitDisabled.enabled = false;

        if (flowerCount < 1)
            flowerDisabled.enabled = true;
        else
            flowerDisabled.enabled = false;

        if (waterCount < 1)
            waterDisabled.enabled = true;
        else
            waterDisabled.enabled = false;

        if (evilFruitCount < 1)
            evilDisabled.enabled = true;
        else
            evilDisabled.enabled = false;
    }

    void ManageUI()
    {

    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1:
                levelText.text = "Type: Fruit";
                break;
            case 2:
                levelText.text = "Type: Flower";
                break;
            case 3:
                levelText.text = "Type: Water";
                break;
            case 4:
                levelText.text = "Type: Evil >:)";
                break;
            default:
                levelText.text = "No Type";
                break;
        }

        if (waterCount >= maxWater)
        {
            waterCount = maxWater;
        }

        waterTimer -= Time.deltaTime;
        if (waterTimer <= 0f)
        {
            if(waterCount < maxWater)
                waterCount++;
            waterTimer = waterRechargeRate;
        }
    }
}
