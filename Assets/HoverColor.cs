using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverColor : MonoBehaviour
{
    public Button drawCardButton;
    public Button drawCoinButton;
    private Color originalCardColorButton;
    private ColorBlock cb;


    // Start is called before the first frame update
    void Start()
    {
        cb=drawCardButton.colors;
        originalCardColorButton=cb.selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
