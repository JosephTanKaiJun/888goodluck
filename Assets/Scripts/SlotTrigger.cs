using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public int slotIndex; // Slot index for tracking

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleItem"))
        {
            PuzzleItem puzzleItem = other.GetComponent<PuzzleItem>();
            if (puzzleItem != null)
            {
                PuzzleRoomGameManager.Instance.RegisterItemInSlot(slotIndex, puzzleItem.itemID);
                PuzzleRoomGameManager.Instance.CheckAllSlotsFilled();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleItem"))
        {
            PuzzleItem puzzleItem = other.GetComponent<PuzzleItem>();
            if (puzzleItem != null)
            {
                PuzzleRoomGameManager.Instance.RegisterItemInSlot(slotIndex, -1); // Reset slot
                PuzzleRoomGameManager.Instance.CheckAllSlotsFilled();
            }
        }
    }
}
