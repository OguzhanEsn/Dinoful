using UnityEngine;

public class ColorChangeButton : MonoBehaviour
{
    public PlayerController playerController;

    public void OnColorChangeButtonPress()
    {
        playerController.ChangeColor();
    }
}
