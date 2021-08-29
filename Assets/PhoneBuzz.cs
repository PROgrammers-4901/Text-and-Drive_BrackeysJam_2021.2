using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBuzz : MonoBehaviour
{
    public void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
