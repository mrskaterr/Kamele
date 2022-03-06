using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        var fillAmount = current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}