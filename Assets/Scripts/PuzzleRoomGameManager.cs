using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomGameManager : MonoBehaviour
{
    public static PuzzleRoomGameManager Instance;

    [System.Serializable]
    public struct SlotLightPair
    {
        public GameObject slot;    // The slot GameObject (optional)
        public Light slotLight;    // The light for this slot
    }

    public SlotLightPair[] slotLightPairs; // Array of slot-light pairs

    private List<int> correctOrder;        // Randomly generated correct order
    private List<int?> playerOrder;        // Player's item order (nullable)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        GenerateRandomOrder();
        ResetLights();
        playerOrder = new List<int?>(new int?[slotLightPairs.Length]); // Initialize player order list
    }

    void GenerateRandomOrder()
    {
        List<int> ids = new List<int>();
        for (int i = 0; i < slotLightPairs.Length; i++)
        {
            ids.Add(i); // ID based on index for dynamic item count
        }

        correctOrder = new List<int>();
        for (int i = 0; i < slotLightPairs.Length; i++)
        {
            int index = Random.Range(0, ids.Count);
            correctOrder.Add(ids[index]);
            ids.RemoveAt(index);
        }

        Debug.Log("Correct order: " + string.Join(", ", correctOrder));
    }

    public void RegisterItemInSlot(int slotIndex, int itemID)
    {
        playerOrder[slotIndex] = itemID;
    }

    public void CheckAllSlotsFilled()
    {
        // Check if all slots are filled
        foreach (var item in playerOrder)
        {
            if (!item.HasValue)
                return; // Some slots are still empty
        }

        // All slots are filled, now check order
        CheckOrder();
    }

    void CheckOrder()
    {
        int correctCount = 0;

        for (int i = 0; i < slotLightPairs.Length; i++)
        {
            if (playerOrder[i].HasValue && playerOrder[i].Value == correctOrder[i])
            {
                slotLightPairs[i].slotLight.color = Color.green;
                correctCount++;
            }
            else
            {
                slotLightPairs[i].slotLight.color = Color.red;
            }
        }

        if (correctCount == correctOrder.Count)
        {
            Debug.Log("Correct order!");
            UpdateGameState(GameState.Completed);
        }
        else
        {
            Debug.Log("Incorrect order.");
        }
    }

    private void ResetLights()
    {
        foreach (var pair in slotLightPairs)
        {
            pair.slotLight.color = Color.white;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("Game state changed to: " + newState);
    }
}


public enum GameState
{
    Start,
    InProgress,
    Completed,
    Failed
}
