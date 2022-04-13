using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhoneControl : MonoBehaviour
{
    private int batteryCharge;
    private bool usable;
    private int dailyUsage = 10;
    private void Awake()
    {
        batteryCharge = 100;
        usable = true;
    }
    public void PhoneUse()
    {
        if (usable && batteryCharge >= dailyUsage)
            batteryCharge -= dailyUsage;
        else
        {
            batteryCharge = 0;
            usable = false;
        }
<<<<<<< HEAD
        Debug.Log("battery use " + batteryCharge);
=======
        print("battery use " + batteryCharge);
>>>>>>> ba1e7674 ([BUG] merge error)

    }
    public void PhoneCharge(int amount)
    {
        batteryCharge = batteryCharge > 100 ? batteryCharge + amount : 100;
        if (amount > 0) usable = true;
<<<<<<< HEAD
        Debug.Log("battery Charge " + batteryCharge);
=======
        print("battery Charge " + batteryCharge);
>>>>>>> ba1e7674 ([BUG] merge error)
    }
    void DisplayScreen()
    {
        if (usable == false) return;

        // get script
    }
}
