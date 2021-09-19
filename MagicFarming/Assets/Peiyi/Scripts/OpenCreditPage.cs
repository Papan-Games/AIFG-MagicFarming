using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCreditPage : MonoBehaviour
{
    public GameObject CreditPage;

    public void OpenCreditPageFunc()
    {
        CreditPage.GetComponent<ChangingCreditPage>().canChangePage = true;
    }
}
