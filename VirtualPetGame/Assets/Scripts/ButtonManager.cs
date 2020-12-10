using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public GameObject FruitTreePrefab;
    public GameObject FlowerTreePrefab;
    public GameObject FountainTreePrefab;
    public GameObject EvilTreePrefab;

    public Button homeButton;
    public Button infoButton;
    public Button closeInfoButton;
    public Button editNameButton;

    public Button FeedButton;
    public Button FlowerButton;
    public Button WaterButton;
    public Button EvilButton;
    public Button PetButton;
    public Image PetImage;
    public Button TransformButton;
    public Text TransformConText;

    public Button SpawnButton;
    public Image SpawnButtonImage;


    private bool toggle;
    public RectTransform inputTransform;
    public RectTransform nameTransform;
    public RectTransform offScreen;



    private ClickManager gm;
    private Manager man;

    void Start()
    {
        gm = this.GetComponent<ClickManager>();
        man = this.GetComponent<Manager>();
        TransformConText.text = "";

        homeButton.onClick.AddListener(() => HomeButtonClicked());
        infoButton.onClick.AddListener(() => ShowInfo());
        closeInfoButton.onClick.AddListener(() => closeInfo());
        editNameButton.onClick.AddListener(() => InputName());


        FeedButton.onClick.AddListener(() => Feed());
        FlowerButton.onClick.AddListener(() => Flower());
        WaterButton.onClick.AddListener(() => Water());
        EvilButton.onClick.AddListener(() => Evil());
        TransformButton.onClick.AddListener(() => TransformGO());
        PetButton.onClick.AddListener(() => Pet());
        SpawnButton.onClick.AddListener(() => SpawnCherub());

    }

    private void Update()
    {
        if (gm.chaoInInspection != null)
        {
            PetImage.fillAmount = gm.chaoInInspection.petTimer / 100;
        }

        if (gm.treeInInspection != null)
        {
            SpawnButtonImage.fillAmount = gm.treeInInspection.timer / 100;
        }
    }

    void SpawnCherub()
    {
        gm.treeInInspection.SpawnChao();
    }

    void HomeButtonClicked()
    {
        gm.isHitEscape = true;
    }

    void ShowInfo()
    {
        gm.showInfo = true;
    }

    void closeInfo()
    {
        gm.showInfo = false;
    }

    void Pet()
    {
        gm.chaoInInspection.GetPet();
    }

    void TransformGO()
    {
        if (gm.chaoInInspection.canTransform)
        {
            // INSTANCIATE 
            switch (gm.chaoInInspection.Element)
            {
                case 1:
                    Instantiate(FruitTreePrefab, gm.chaoInInspection.transform.position, Quaternion.identity);
                    Destroy(gm.chaoInInspection.gameObject);
                    break;
                case 2:
                    Instantiate(FlowerTreePrefab, gm.chaoInInspection.transform.position, Quaternion.identity);
                    Destroy(gm.chaoInInspection.gameObject);
                    break;
                case 3:
                    Instantiate(FountainTreePrefab, gm.chaoInInspection.transform.position, Quaternion.identity);
                    Destroy(gm.chaoInInspection.gameObject);
                    break;
                case 4:
                    Instantiate(EvilTreePrefab, gm.chaoInInspection.transform.position, Quaternion.identity);
                    Destroy(gm.chaoInInspection.gameObject);
                    break;
                default:
                    break;
            }
        }
        else
        {
            StartCoroutine(Delay());
            if (gm.chaoInInspection.Element == 0)
            {
                TransformConText.text = "You must level up first...\nTry filling a Resource Bar all the way.";
            }
            else if (gm.chaoInInspection.Element != 0)
            {
                switch (gm.chaoInInspection.Element)
                {
                    case 1:
                        TransformConText.text = "Food Value must be above 50%";
                        break;
                    case 2:
                        TransformConText.text = "Happiness must be above 50%";
                        break;
                    case 3:
                        TransformConText.text = "Water Value must be above 50%";
                        break;
                    case 4:
                        TransformConText.text = "Food Value must be above 50% \nHappiness must be above 50%";
                        break;
                    default:
                        TransformConText.text = "Cannot Transform...";
                        break;
                }
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(4f);
        TransformConText.text = "";
    }

    void InputName()
    {
        if (toggle)
        {
            inputTransform.position = nameTransform.position;
            inputTransform.GetComponent<InputField>().text = "";
            toggle = false;
        }
        else
        {
            inputTransform.position = offScreen.position;
            if (gm.chaoInInspection != null)
            {
                gm.chaoInInspection.myName = inputTransform.GetComponent<InputField>().text;
            }
            toggle = true;

        }



    }

    void Feed()
    {
        if (man.fruitCount > 0)
        {
            if (gm.chaoInInspection != null && !gm.chaoInInspection.isSleeping && gm.chaoInInspection.canGive)
            {
                // FEED CHAO
                man.fruitCount--;
                gm.chaoInInspection.GetFood();


            }
        }
    }
    void Flower()
    {
        if (man.flowerCount > 0)
        {
            if (gm.chaoInInspection != null && !gm.chaoInInspection.isSleeping && gm.chaoInInspection.canGive)
                {
                // FLOWER CHAO
                man.flowerCount--;
                gm.chaoInInspection.GetFlower();


            }
        }
    }
    void Water()
    {
        if (man.waterCount > 0)
        {
            if (gm.chaoInInspection != null && !gm.chaoInInspection.isSleeping && gm.chaoInInspection.canGive)
            {
                // WATER CHAO
                man.waterCount--;
                gm.chaoInInspection.GetWater();
            }
        }
    }
    void Evil()
    {
        if (man.evilFruitCount > 0)
        {
            if (gm.chaoInInspection != null && !gm.chaoInInspection.isSleeping && gm.chaoInInspection.canGive)
            {
                // EVIL CHAO
                man.evilFruitCount--;
                gm.chaoInInspection.GetEvil();
            }
        }
    }
}
