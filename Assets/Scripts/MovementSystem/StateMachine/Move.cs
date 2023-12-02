using System;
using UnityEngine;

public class Move : StateBase
{
    private Transform _playerTransform;
    private Action<StateMachineBase> _updateMovement;

    private float _alignmentSpeed;
    private float _alignmentAngularSpeed;
    private float _movementSpeed;
    private Vector3 _lastPosition;

    public Move(string stateID, MovementData movementData) : base(stateID)
    {
        _alignmentAngularSpeed = movementData.AlignmentAngularSpeed;
        _alignmentSpeed = movementData.AlignmentSpeed;
        _movementSpeed = movementData.MovementSpeed;
    }

    public override void OnEnter(StateMachineBase context)
    {
        base.OnEnter(context);
        _playerTransform = context.transform;
        _updateMovement = SetPositionAndRotation;
    }

    public override void OnExit(StateMachineBase context)
    {
        base.OnExit(context);
        DollyCartManager.ResetCart();
        _updateMovement = null;
    }

    public override void OnUpdate(StateMachineBase context)
    {
        //base.OnUpdate(context);
        _updateMovement?.Invoke(context);

    }

    private void SetPositionAndRotation(StateMachineBase context)
    {
        //checks if position and rotation of player are aligned with the cart position and rotation 
        if (Vector3.Distance(_playerTransform.position, DollyCartManager.GetCartPosition()) > 0.1f ||
            Mathf.Abs(Quaternion.Dot(_playerTransform.rotation, DollyCartManager.GetCartRotation()) - 1f) > 0.0001f)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, DollyCartManager.GetCartPosition(), Time.deltaTime * _alignmentSpeed);
            _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, DollyCartManager.GetCartRotation(), Time.deltaTime * _alignmentAngularSpeed);
        }
        else
        {
            DollyCartManager.StartCartMovement(_movementSpeed);
            _updateMovement = PerformMovement;
        }
    }

    private void PerformMovement(StateMachineBase context)
    {
        //cast the context
        PlayerStateMachine playerSM = context as PlayerStateMachine;
        //save player position before updateing it
        _lastPosition = _playerTransform.position;
        //update player position and rotation
        _playerTransform.position = DollyCartManager.GetCartPosition();
        _playerTransform.rotation = DollyCartManager.GetCartRotation();

        //checks if the motion is completed
        if (_lastPosition == _playerTransform.position)
            playerSM.ChangeState(playerSM.Idle);
        
    }
}
