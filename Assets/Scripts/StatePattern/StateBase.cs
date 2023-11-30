using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase 
{
    public string StateID;

    public StateBase(string stateID)
    {
        StateID = stateID;
    }




    public virtual void OnEnter(StateMachineBase context)
    {
        Debug.Log($"OnEnter: {StateID}");
    }
    public virtual void OnExit(StateMachineBase context)
    {
        Debug.Log($"OnExit: {StateID}");
    }




    public virtual void OnCollisionEnter(StateMachineBase context)
    {
        Debug.Log($"OnCollisionEnter: {StateID}");
    }
    public virtual void OnCollisionStay(StateMachineBase context)
    {
        Debug.Log($"OnCollisionStay: {StateID}");
    }
    public virtual void OnCollisionExit(StateMachineBase context)
    {
        Debug.Log($"OnCollisionExit: {StateID}");
    }




    public virtual void OnTriggerEnter(StateMachineBase context)
    {
        Debug.Log($"OnTriggerEnter: {StateID}");
    }
    public virtual void OnTriggerStay(StateMachineBase context)
    {
        Debug.Log($"OnTriggerStay: {StateID}");
    }
    public virtual void OnTriggerExit(StateMachineBase context)
    {
        Debug.Log($"OnTriggerExit: {StateID}");
    }




    public virtual void OnFixedUpdate(StateMachineBase context)
    {
        Debug.Log($"OnFixedUpdate: {StateID}");
    }
    public virtual void OnUpdate(StateMachineBase context)
    {
        //Debug.Log($"OnUpdate: {StateID}");
    }
    public virtual void OnLateUpdate(StateMachineBase context)
    {
        Debug.Log($"OnLateUpdate: {StateID}");
    }


}
