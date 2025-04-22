using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public int slotIndex; // Slot index for tracking
    private bool isItemPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleItem"))
        {
            if (!isItemPlaced)  // Make sure item is placed only once per slot
            {
                PuzzleItem puzzleItem = other.GetComponent<PuzzleItem>();
                if (puzzleItem != null)
                {
                    PuzzleRoomGameManager.Instance.RegisterItemInSlot(slotIndex, puzzleItem.itemID);
                    isItemPlaced = true; // Mark this slot as filled
                    PuzzleRoomGameManager.Instance.CheckAllSlotsFilled();
                }
            }
        }
    }
}
