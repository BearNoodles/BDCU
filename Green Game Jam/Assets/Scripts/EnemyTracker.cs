using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct EnemyMarker
//{
//    public GameObject enemy;
//    public GameObject marker;
//}

public class EnemyTracker : MonoBehaviour {

    Vector3 fishPosition;
    float camW;
    float camH;

    public GameObject markerPrefab;

    List<GameObject> enemies;
    GameObject marker;
    List<GameObject> enemyMarkers;
    List<GameObject> pollutionMarkers;
    GameObject netMarker;
    GameObject netMarker2;

    //Vector3 enemyMarkerScale;
    //Vector3 pollutionMarkerScale;
    //Vector3 netMarkerScale;

    float enemyMarkerScale;
    float pollutionMarkerScale;
    float netMarkerScale;

    Vector3 markerPosition;

    float enemyMarkerDistance;
    float pollutionMarkerDistance;
    float netMarkerDistance;

    public Sprite enemyMarkerSprite;
    public Sprite pollutionMarkerSprite;
    public Sprite netMarkerSprite;

    GameObject net;
    GameObject net2;



	// Use this for initialization
	void Start ()
    {
        camW = 13.0f;
        camH = 5.0f;

        enemyMarkerDistance = 4.0f;
        pollutionMarkerDistance = 5.0f;
        netMarkerDistance = 6.0f;
        //camH = GetComponentInChildren<Camera>().pixelHeight;
        
        enemyMarkers = new List<GameObject>();
        pollutionMarkers = new List<GameObject>();

        net = GameObject.FindGameObjectWithTag("Net");
        net2 = GameObject.FindGameObjectWithTag("Net2");

        enemyMarkerScale = 0.1f;
        pollutionMarkerScale = 0.01f;
        netMarkerScale = 0.03f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (GameObject m in enemyMarkers)
        {
            Destroy(m);
        }
        foreach (GameObject m in pollutionMarkers)
        {
            Destroy(m);
        }
        Destroy(netMarker);
        Destroy(netMarker2);
        enemyMarkers.Clear();
        pollutionMarkers.Clear();
        netMarker = null;
        netMarker2 = null;

        fishPosition = transform.position;

        foreach (GameObject g in (GameObject.FindGameObjectsWithTag("Enemy")))
        {
            if (CheckOutOfBounds(g.transform))
            {
                marker = Instantiate<GameObject>(markerPrefab);
                marker.transform.localScale *= enemyMarkerScale;
                marker.GetComponent<SpriteRenderer>().sprite = enemyMarkerSprite;
                markerPosition = fishPosition - g.transform.position;
                markerPosition.Normalize();
                marker.transform.position = fishPosition - (markerPosition * enemyMarkerDistance);
                enemyMarkers.Add(marker);
            }
        }

        foreach (GameObject g in (GameObject.FindGameObjectsWithTag("EnemyD")))
        {
            if (CheckOutOfBounds(g.transform))
            {
                marker = Instantiate<GameObject>(markerPrefab);
                marker.transform.localScale *= enemyMarkerScale;
                marker.GetComponent<SpriteRenderer>().sprite = enemyMarkerSprite;
                markerPosition = fishPosition - g.transform.position;
                markerPosition.Normalize();
                marker.transform.position = fishPosition - (markerPosition * enemyMarkerDistance);
                enemyMarkers.Add(marker);
            }
        }

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Pollution"))
        {
            if (CheckOutOfBounds(p.transform))
            {
                marker = Instantiate<GameObject>(markerPrefab);
                marker.transform.localScale *= pollutionMarkerScale;
                marker.GetComponent<SpriteRenderer>().sprite = pollutionMarkerSprite;
                markerPosition = fishPosition - p.transform.position;
                markerPosition.Normalize();
                marker.transform.position = fishPosition - (markerPosition * pollutionMarkerDistance);
                pollutionMarkers.Add(marker);
            }
        }

        if (CheckOutOfBounds(net.transform))
        {
            marker = Instantiate<GameObject>(markerPrefab);
            marker.transform.localScale *= netMarkerScale;
            marker.GetComponent<SpriteRenderer>().sprite = netMarkerSprite;
            markerPosition = fishPosition - net.transform.position;
            markerPosition.Normalize();
            marker.transform.position = fishPosition - (markerPosition * netMarkerDistance);
            netMarker = marker;
        }
        if (CheckOutOfBounds(net2.transform))
        {
            marker = Instantiate<GameObject>(markerPrefab);
            marker.transform.localScale *= netMarkerScale;
            marker.GetComponent<SpriteRenderer>().sprite = netMarkerSprite;
            markerPosition = fishPosition - net2.transform.position;
            markerPosition.Normalize();
            marker.transform.position = fishPosition - (markerPosition * netMarkerDistance);
            netMarker2 = marker;
        }


        //enemies.Clear();
    }

    bool CheckOutOfBounds(Transform t)
    {
        if(Mathf.Abs(fishPosition.x - t.position.x) > camW || Mathf.Abs(fishPosition.y - t.position.y) > camH)
        {
            return true;
        }
        return false;
    }
}


