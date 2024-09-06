using UnityEngine;
using UnityEngine.UI;

public class PerformanceOverlay : MonoBehaviour
{
    public Text performanceText;

    public void ShowPerformance(int partsCollected, float timeTaken)
    {
        performanceText.text = "Parts Collected: " + partsCollected + "\nTime Taken: " + timeTaken + "s";
        gameObject.SetActive(true);
    }

    public void HidePerformance()
    {
        gameObject.SetActive(false);
    }
}
