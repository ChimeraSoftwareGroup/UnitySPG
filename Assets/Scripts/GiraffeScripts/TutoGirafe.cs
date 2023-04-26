using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGirafe : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private void Start()
    {
        Invoke("StartTutoAnimation", 5f);
    }

    private void StartTutoAnimation()
    {
        _animator.Play("GirafeTuto");

    }
}
