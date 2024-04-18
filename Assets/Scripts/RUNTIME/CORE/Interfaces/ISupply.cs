using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISupply
{
    float Capacity { get; }
    float WorkRadius { get; }

    float RefillSpeed { get; }
    float RefillAmount { get; }

    float CurrentSupply { get; }

    public bool Supply(IConsume consume);
    public void Refill();
    public bool IsEmpty();
}
