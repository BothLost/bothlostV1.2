using UnityEngine;

public class FireLightScript : MonoBehaviour
{
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;
    public bool isFireActive;

    public Light fireLight;
    public GameObject flame;

    float random;


    private GameObject player;
    int burningTime;




    private void Start()
    {
        burningTime = 10;

        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("putOutTheFire", burningTime);
    }

    void Update()
    {

        /*
		random = Random.Range(0.0f, 150.0f);
		float noise = Mathf.PerlinNoise(random, Time.time);
		fireLight.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);*/

        bool closeOne = isThereAnyCloseActiveFire();

        if (closeOne)
        {
            PlayerState.instance.isPlayerNearToFire = true;
        }
        else
        {
            PlayerState.instance.isPlayerNearToFire = false;
        }


    }

    bool isThereAnyCloseActiveFire()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("campfire");
        foreach (GameObject obj in objs)
        {
            FireLightScript campfireScript = obj.GetComponent<FireLightScript>();
            if (campfireScript != null && campfireScript.isFireActive)
            {
                float dist = Vector3.Distance(obj.transform.position, player.transform.position);
                if (dist < 5) //ateş yanıyor ve yakın
                {

                    return true;

                }
            }








        }
        return false;
    }


    void putOutTheFire()
    {
        Debug.Log("ateş söndürülecek");
        fireLight.enabled = false;
        isFireActive = false;
        PlayerState.instance.isPlayerNearToFire = false;
        flame.SetActive(false);
    }
}