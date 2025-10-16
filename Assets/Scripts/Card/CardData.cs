using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public SpriteRenderer cardImage;
    public TextMeshPro txt_Cost;
    int dark_Match_CoefficientB;


    public void SetCost() 
    {
        this.txt_Cost.text = "5";

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
