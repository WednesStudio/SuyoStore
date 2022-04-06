using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTime : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FunctionTimer.Create(TestingAction, 3f);
    }

    private void TestingAction()
    {
        Debug.Log("Testing!");
    }
}
