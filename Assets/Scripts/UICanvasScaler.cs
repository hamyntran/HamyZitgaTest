using UnityEngine;
using UnityEngine.UI;

public class UICanvasScaler : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public const float BASE_SCREEN_RATIO = 0.5625f;

    
    private void Start()
    {
        if (Screen.width * 1f / Screen.height > BASE_SCREEN_RATIO)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
