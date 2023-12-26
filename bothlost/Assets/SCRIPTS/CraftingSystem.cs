using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;

    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //Category Buttons

    Button toolsBTN;

    //Craft Buttons

    Button craftAxeBTN;
    Button craftCampFireBTN;

    //Requirement Text

    Text AxeReq1, AxeReq2;
    Text campFireReq1, campFireReq2;
    public bool isOpen;

    //All Blueprint

    public Blueprint AxeBLP = new Blueprint("Axe",2,"Rock",3,"Branch",3);
    public Blueprint cmpfireBLP = new Blueprint("campfire", 2, "Rock", 4, "Wood", 2);



    public static CraftingSystem Instance { get; set; }


    private void Awake()
    {
        if(Instance != null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        //campFire
        campFireReq1 = toolsScreenUI.transform.Find("CampFire").transform.Find("req1").GetComponent<Text>();
        campFireReq2 = toolsScreenUI.transform.Find("CampFire").transform.Find("req2").GetComponent<Text>();


        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();

        craftCampFireBTN = toolsScreenUI.transform.Find("CampFire").transform.Find("Button").GetComponent<Button>();

        craftAxeBTN.onClick.AddListener(delegate { AxeCrafting(); });

        craftCampFireBTN.onClick.AddListener(delegate { CampFireCrafting(); });

    }
    void AxeCrafting()
    {
        Inventory.Instance.AddToInventory("Axe");
        Inventory.Instance.RemoveItem("rock", 3);
        Inventory.Instance.RemoveItem("branch", 3);
        StartCoroutine(calculate());

        RefreshNeededItems();


    }

    void CampFireCrafting()
    {
        Inventory.Instance.AddToInventory("CampFire");
        Inventory.Instance.RemoveItem("rock", 4);
        Inventory.Instance.RemoveItem("wood", 2);
        StartCoroutine(calculate());

        RefreshNeededItems();


    }
    /*
    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        //Add item to Inventory
        Inventory.Instance.AddToInventory("Axe");

        if(blueprintToCraft.numOfRequirements ==1)
        {
            Inventory.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if(blueprintToCraft.numOfRequirements ==2)
        {
            Inventory.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            Inventory.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }


        StartCoroutine(calculate());

        RefreshNeededItems();
        //Remove resources from Inventory

        //
    }*/

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);
        Inventory.Instance.ReCalculeList();
    }


    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Debug.Log("i is pressed");
            craftingScreenUI.SetActive(true);
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            isOpen = false;
        }

    }

    
    private void RefreshNeededItems()
    {
        


        axeButtonActive();

        campFireButtonActive();




    }

    void axeButtonActive()
    {
        int rock_count = 0;
        int branch_count = 0;
        int wood_count = 0;

        inventoryItemList = Inventory.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "rock":
                    rock_count += 1;
                    break;
                case "branch":
                    branch_count += 1;
                    break;
                case "wood":
                    wood_count += 1;
                    break;

            }

        }
        AxeReq1.text = "3 Rock [" + rock_count + "]";
        AxeReq2.text = "3 Branch [" + branch_count + "]";
        if (rock_count >= 3 && branch_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }

    }

    void campFireButtonActive()
    {
        int rock_count = 0;
        int branch_count = 0;
        int wood_count = 0;

        inventoryItemList = Inventory.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "rock":
                    rock_count += 1;
                    break;
                case "branch":
                    branch_count += 1;
                    break;
                case "wood":
                    wood_count += 1;
                    break;

            }

        }
        campFireReq1.text = "4 Rock [" + rock_count + "]";
        campFireReq2.text = "2 Wood [" + wood_count + "]";
        if (rock_count >= 4 && wood_count >= 2)
        {
            craftCampFireBTN.gameObject.SetActive(true);
        }
        else
        {
            craftCampFireBTN.gameObject.SetActive(false);
        }

    }



}
