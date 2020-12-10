using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public HideOnScreen UIpanel1;
    public HideOnScreen TreePanel;
    public HideOnScreen WorldPanel;
    public GameObject InfoPanel;

    public Button homeButton;
    [HideInInspector]
    public bool isHitEscape = false;
    [HideInInspector]
    public bool showInfo;

    public Camera cam;
    private Manager gm;
    private Color baseColor;
    private Color fruitColor;
    private Color flowerColor;
    private Color waterColor;
    private Color evilColor;



    public float camDampTime;
    public float zoomSpeed;
    private Vector3 velocity = Vector3.zero;
    private Transform camTarget;
    private float camStartSize;
    private bool canInspect;

    public Transform camStartPos;

    public Chao chaoInInspection;
    public TreeObj treeInInspection;
    public FruitNode fruitInInspection;


    private void Start()
    {
        gm = this.GetComponent<Manager>();
        baseColor = new Color(1, 1, 1, 0.862f);
        fruitColor = new Color(0.980f, 0.825f, 1f, 0.99f);
        flowerColor = new Color(0.990f, 0.953f, 0.668f, 0.99f);
        waterColor = new Color(0.666f, 0.855f, 0.992f, 0.99f);
        evilColor = new Color(0.707f, 0.410f, 0.434f, 0.99f);


        camStartSize = cam.orthographicSize;
        gm.val1 = 0;
        gm.val2 = 0;
        gm.val3 = 0;
        gm.val4 = 0;

        UIpanel1.isHidden = true;
        TreePanel.isHidden = true;
        WorldPanel.isHidden = false;
        homeButton.gameObject.SetActive(false);
        showInfo = true;
        canInspect = true;

    }

    void Update()
    {
        ManageCamera();

        if (Input.GetMouseButtonDown(0))
        {
            fruitInInspection = null;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                WorldPanel.isHidden = true;


                if (hit.collider.gameObject.CompareTag("Chao") && canInspect)
                {
                    canInspect = false;
                    UIpanel1.isHidden = false;
                    TreePanel.isHidden = true;
                    chaoInInspection = hit.collider.gameObject.GetComponent<Chao>();
                    chaoInInspection.isInspected = true;
                    UIpanel1.GetComponent<Image>().color = chaoInInspection.GetComponent<SpriteRenderer>().color;

                    camTarget = chaoInInspection.transform;
                    homeButton.gameObject.SetActive(true);

                }
                if (hit.collider.gameObject.CompareTag("FruitNode"))
                {
                    fruitInInspection = hit.collider.gameObject.GetComponent<FruitNode>();
                    if (fruitInInspection.parentTree.isInspected) // if you're inspecting the tree
                    {
                        if (fruitInInspection.fruitHasSpawned)
                        {
                            if (fruitInInspection.isHoldingFruit)
                            {
                                // COLLECT FRUIT
                                Debug.Log("COLLECT FRUIT");
                                if (fruitInInspection.type == 1)
                                {
                                    gm.fruitCount++;
                                }
                                else if (fruitInInspection.type == 2)
                                {
                                    gm.flowerCount++;
                                }
                                else if (fruitInInspection.type == 3)
                                {
                                    gm.waterCount += 10;
                                }
                                else if (fruitInInspection.type == 4)
                                {
                                    gm.evilFruitCount++;
                                }

                                fruitInInspection.isHoldingFruit = false;
                                fruitInInspection.fruitHasSpawned = false;
                            }
                            else
                            {
                                // FRUIT IS NOT READY YET
                                Debug.Log("FRUIT IS NOT READY YET");
                            }
                        }
                    }
                }
                if (hit.collider.gameObject.CompareTag("Tree") && treeInInspection == null && canInspect)
                {
                    canInspect = false;
                    TreePanel.isHidden = false;
                    UIpanel1.isHidden = true;
                    treeInInspection = hit.collider.gameObject.GetComponent<TreeObj>();
                    treeInInspection.isInspected = true;

                    camTarget = treeInInspection.transform;
                    homeButton.gameObject.SetActive(true);
                }
            }
        }

        if (chaoInInspection != null)
        {
            gm.myName = chaoInInspection.myName;
            gm.level = chaoInInspection.Element;
            gm.val1 = chaoInInspection.HungerValue;
            gm.val2 = chaoInInspection.HappinessValue;
            gm.val3 = chaoInInspection.LifeValue;
            gm.val4 = chaoInInspection.TiredValue;
            
        }
        if (treeInInspection != null)
        {
            if (treeInInspection.TreeType == 1)
            {
                TreePanel.GetComponent<Image>().color = fruitColor;
                gm.treeName = "Plum Tree";
                gm.treeInfo.text = "Plums increase a Cherub's food meter";
            }
            else if (treeInInspection.TreeType == 2)
            {
                TreePanel.GetComponent<Image>().color = flowerColor;
                gm.treeName = "Flora Bush";
                gm.treeInfo.text = "Flowers increase a Cherub's happiness!";
            }
            else if (treeInInspection.TreeType == 3)
            {
                TreePanel.GetComponent<Image>().color = waterColor;
                gm.treeName = "Water Fountain";
                gm.treeInfo.text = "Refill your Watering Can here!";


            }
            else if (treeInInspection.TreeType == 4)
            {
                TreePanel.GetComponent<Image>().color = evilColor;
                gm.treeName = "Bloodroot Tree";
                gm.treeInfo.text = "Those pesky Evil Cherubs love Bloodfruit! No one else seems to, though.";


            }
            gm.fruitNodeCount = treeInInspection.numFruitNodes;
        }



        if (showInfo)
        {
            InfoPanel.SetActive(true);
        }
        else
            InfoPanel.SetActive(false);
    }

    void ManageCamera()
    {
        if (camTarget) // Move camera to target
        {
            Vector3 point = cam.WorldToViewportPoint(camTarget.position);
            Vector3 delta = camTarget.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = cam.transform.position + delta;
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, destination, ref velocity, camDampTime);
        }

        if (isHitEscape) // Is press Escape, reset target to start // RUNS ONE TIME
        {
            canInspect = true;

            if (chaoInInspection != null)
            {
                chaoInInspection.isInspected = false;
                chaoInInspection = null;
                gm.val1 = 0;
                gm.val2 = 0;
                gm.val3 = 0;
                gm.val4 = 0;
            }
            if (treeInInspection != null)
            {
                treeInInspection.isInspected = false;
                treeInInspection = null;
            }
            if (fruitInInspection != null)
            {
                fruitInInspection = null;
            }

            camTarget = camStartPos;
            UIpanel1.GetComponent<Image>().color = baseColor;
            TreePanel.GetComponent<Image>().color = baseColor;

            isHitEscape = false;
            UIpanel1.isHidden = true;
            TreePanel.isHidden = true;
            WorldPanel.isHidden = false;
            homeButton.gameObject.SetActive(false);
        }

        if (camTarget != null && camTarget.transform != camStartPos) // Zoom into target, if target is not start
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 4f, Time.deltaTime * zoomSpeed);

        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camStartSize, Time.deltaTime * zoomSpeed);
        }

    }
}
