using UnityEngine;

public class FuelTankCollision : MonoBehaviour
{

    private FuelTankCounter fuelTankCounter;

    private void Start()
    {
        fuelTankCounter = FindObjectOfType<FuelTankCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fuelTankCounter.IncrementFuelTankCount();
            Destroy(gameObject);
        }
    }
}