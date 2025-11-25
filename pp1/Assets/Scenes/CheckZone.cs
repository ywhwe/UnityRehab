using UnityEngine;
using TMPro;

public class CheckZone : MonoBehaviour
{
    public TextMeshProUGUI d61Text, d62Text, d63Text;

    public DiceManager diceManager;
    public float stopThreshold = 0.1f;

    private bool _d61ReportedSettled = false;
    private bool _d62ReportedSettled = false;
    private bool _d63ReportedSettled = false;

    void OnTriggerStay(Collider other) {
        Transform parentT = other.transform.parent;
        GameObject detected = null;
        Rigidbody detectedRb = null;

        if (parentT != null)
        {
            detected = parentT.gameObject;
            detectedRb = detected.GetComponent<Rigidbody>();
        }
        else
        {
            detected = other.gameObject;
            detectedRb = detected.GetComponent<Rigidbody>();
        }

        if (detectedRb != null && detected != null)
        {
            if (detectedRb.linearVelocity.magnitude <= stopThreshold && detectedRb.angularVelocity.magnitude <= stopThreshold)
            {
                string diceName = detected.name;
                bool alreadyReported = false;

                switch (diceName)
                {
                    case "d61" : if (_d61ReportedSettled) alreadyReported = true; break;
                    case "d62" : if (_d62ReportedSettled) alreadyReported = true; break;
                    case "d63" : if (_d63ReportedSettled) alreadyReported = true; break;
                }

                if (alreadyReported) return;

                TextMeshProUGUI text = null;

                string faceValueString = other.gameObject.name;
                int faceValueInt;

                if (int.TryParse(faceValueString, out faceValueInt)) {
                    
                    switch (detected.name)
                    {
                        case "d61" : text = d61Text; _d61ReportedSettled = true; break;
                        case "d62" : text = d62Text; _d62ReportedSettled = true; break;
                        case "d63" : text = d63Text; _d63ReportedSettled = true; break;
                        default: Debug.LogWarning($"[CheckZone] 알 수 없는 주사위 오브젝트 감지: {parentT.gameObject.name}"); break;
                    }

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

                    if (diceManager != null) diceManager.SetDiceFace(detected.name, faceValueInt);
                    else Debug.LogError("[CheckZone] DiceManager 연결 안됨...");
                }
            }
        }
    }

    // private void OnTriggerExit(Collider other) {
    //     GameObject parentDiceObject = null;
    //     if (other.transform.parent != null) parentDiceObject = other.transform.parent.gameObject;
    //     else parentDiceObject = other.gameObject;

    //     if (parentDiceObject != null)
    //     {
    //         TextMeshProUGUI target = null;

    //         switch (parentDiceObject.name)
    //         {
    //             case "d61" : target = d61Text; break;
    //             case "d62" : target = d62Text; break;
    //             case "d63" : target = d63Text; break;
    //         }

    //         if (target != null) target.text = "";
    //     }
    // }

    public void ResetAllDice()
    {
        if (diceManager != null)
        {
            diceManager.ResetDice();

            if (d61Text != null) d61Text.text = "";
            if (d62Text != null) d62Text.text = "";
            if (d63Text != null) d63Text.text = "";

            _d61ReportedSettled = false;
            _d62ReportedSettled = false;
            _d63ReportedSettled = false;
        }
    }
}
