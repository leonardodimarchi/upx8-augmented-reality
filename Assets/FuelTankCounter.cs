using UnityEngine;
using TMPro;

public class FuelTankCounter : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    public CustomSceneManager manager;

    private int fuelTankCount = 0;

    public void IncrementFuelTankCount()
    {
        fuelTankCount++;
        string text = $"var gasolina = {fuelTankCount}\n\nse (gasolina == 3) {{\n  finalizarJogo()\n}}";

        Debug.Log(text);
        UIText.text = text;

        if (fuelTankCount == 3)
        {
            manager.GoToTheNextScene();
        }
    }
}