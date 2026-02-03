using UnityEngine;
using UnityEngine.UI;

public class LockTimer : MonoBehaviour
{

    public Piece piece;

    public float maximum;
    public float current;
    public Image mask;

    void Start()
    {
        maximum = piece.lockDelay;
    }

    void Update()
    {
        current = this.piece.GetLockTime();
        getFillAmount();
    }

    public void getFillAmount()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }

    public void Reset()
    {
        current = 0;
    }
}
