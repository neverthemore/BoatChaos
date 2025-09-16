using UnityEngine;

public interface IMover
{
    public void Update(float deltaTime);
    public void SetDirection(Vector2 direction);
}

public interface IRotator
{
    public void Update(float deltaTime);
    public void SetDirection(Vector2 direction);
}
