using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColorChanger : MonoBehaviour
{
    public Color hoveringColor;
    public Color normalColor;
    public TextMeshProUGUI NewGameText;//might use later for other screens name is not important
    public TextMeshProUGUI ExitText;
    public TextMeshProUGUI SaveText;

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
    public void SaveGameHoveringColor()
    {
        SaveText.color = hoveringColor;
    }
    public void LeaveSaveGameColor()
    {
        SaveText.color = normalColor;
    }
}
