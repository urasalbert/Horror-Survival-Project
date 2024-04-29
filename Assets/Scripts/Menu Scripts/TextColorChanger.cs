using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColorChanger : MonoBehaviour
{
    public Color hoveringColor;
    public Color normalColor;
    public TextMeshProUGUI NewGameText;
    public TextMeshProUGUI ExitText;

    public void HoveringNewGameColor()
    {
        NewGameText.color = hoveringColor;
    }
    public void LeavedNewGameColor()
    {
        NewGameText.color = normalColor;
    }
    public void HoveringExitColor()
    {
        ExitText.color = hoveringColor;
    }
    public void LeavedExitColor()
    {
        ExitText.color = normalColor;
    }

}
