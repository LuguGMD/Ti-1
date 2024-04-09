using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Text life;
    [SerializeField] PlayerController pc;

    [SerializeField] Text points;
    [SerializeField] StatsManager sm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life.text = "Life: " + pc.life.ToString();
        points.text = "Points: " + sm.points.ToString();
    }
}
