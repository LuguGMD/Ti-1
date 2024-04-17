using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    public TextMeshProUGUI lifeText;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifeText(int life)
    {
        lifeText.text = "Life: " + life.ToString();
    }
}
