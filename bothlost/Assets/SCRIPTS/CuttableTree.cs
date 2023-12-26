using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CuttableTree : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wood;
    //string tagName;
    private float timePressed = 0f;
    private bool isPressed = false;
    // Image handIcon;
    //TextMeshProUGUI infotext;


    void Start()
    {
       // tagName = gameObject.tag;
        // GameObject obj = GameObject.FindGameObjectWithTag("info_subtext");
        //infotext = obj.GetComponentInChildren<TextMeshProUGUI>();


    }

   

    // Update is called once per frame
    void Update()
    {

        //burasý toplanabilir bir neseneye bakýyorsa
        if (SelectionManager.instance.getHit().collider != null && SelectionManager.instance.getHit().collider.gameObject == gameObject)
        {




            if (Input.GetMouseButtonDown(0))
            {
                isPressed = true;
                timePressed = Time.time;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isPressed = false;
                if (Time.time - timePressed > 1f)
                {
                    Destroy(gameObject);
                    Instantiate(wood, transform.position + new Vector3(0, 5,0), Quaternion.identity);
                    Instantiate(wood, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                }
            }





        }

        if (!SelectionManager.instance.getOnTarget())
        {

            // Debug.Log("eski haline döndü");
        }




    }
}
