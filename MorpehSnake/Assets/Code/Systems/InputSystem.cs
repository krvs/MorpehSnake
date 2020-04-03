using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem
{
    private Filter _input;

    public override void OnAwake()
    {
        _input = World.Filter.With<InputData>();
    }

    public override void OnUpdate(float deltaTime)
    {
        var inputBag = _input.Select<InputData>();
        ref var input = ref inputBag.GetComponent(0);

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        if (new Vector2(x, y).sqrMagnitude > 0.01f)
        {
            Direction direction;
            if (Mathf.Abs(x) > Mathf.Abs(y))
                direction = x > 0f ? Direction.Right : Direction.Left;
            else
                direction = y > 0f ? Direction.Up : Direction.Down;
            
            if (!AreDirectionsOpposite (direction, input.Direction)) {
                input.Direction = direction;
            }
        }
    }

    private static bool AreDirectionsOpposite (Direction a, Direction b) {
        if ((int) a > (int) b) {
            var t = a;
            a = b;
            b = t;
        }
        return a == Direction.Up && b == Direction.Down || a == Direction.Right && b == Direction.Left;
    }
}