using UnityEngine;
using TMPro;

public class CheckZone : MonoBehaviour
{
    public TextMeshProUGUI text;

    void OnTriggerStay(Collider other) {
        if (Dice.diceVelocity.magnitude <= 0.01f)
        {
            switch(other.gameObject.name)
            {
                case "1" : text.text = "1"; break;
                case "2" : text.text = "2"; break;
                case "3" : text.text = "3"; break;
                case "4" : text.text = "4"; break;
                case "5" : text.text = "5"; break;
                case "6" : text.text = "6"; break;
                default : text.text = "???"; break;
            }
        }
    }
}
