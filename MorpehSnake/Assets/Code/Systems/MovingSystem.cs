using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovingSystem))]
public sealed class MovingSystem : UpdateSystem
{
    public GameConfig Config;
    
    private Filter _inputFilter;
    private Filter _snakeFilter;

    private float _moveRate = 0.2f;
    private float _timer = 0;
    
    public override void OnAwake()
    {
        _inputFilter = World.Filter.With<InputData>();
        _snakeFilter = World.Filter.With<Snake>();
    }

    public override void OnUpdate(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer < _moveRate)
            return;
        _timer = 0;

        var inputBag = _inputFilter.Select<InputData>();
        ref var inputData = ref inputBag.GetComponent(0);

        var snakeBag = _snakeFilter.Select<Snake>();
        ref var snake = ref snakeBag.GetComponent(0);

        var prevPos = snake.Transform.position;

        if (inputData.Direction == Direction.Up)
            snake.Transform.position += Vector3.up;

        if (inputData.Direction == Direction.Down)
            snake.Transform.position += Vector3.down;

        if (inputData.Direction == Direction.Left)
            snake.Transform.position += Vector3.left;

        if (inputData.Direction == Direction.Right)
            snake.Transform.position += Vector3.right;

        for (var i = 0; i < snake.Body.Count; i++)
        {
            var tempPos = snake.Body[i].position;
            snake.Body[i].position = prevPos;
            prevPos = tempPos;
        }
        
        if(snake.Transform.position.x > Config.XSize)
            snake.Transform.position = new Vector3(snake.Transform.position.x - Config.XSize, snake.Transform.position.y, 0);
        if(snake.Transform.position.x < 0)
            snake.Transform.position = new Vector3(snake.Transform.position.x + Config.XSize, snake.Transform.position.y, 0);
        
        if(snake.Transform.position.y > Config.YSize)
            snake.Transform.position = new Vector3(snake.Transform.position.x, snake.Transform.position.y - Config.YSize, 0);
        if(snake.Transform.position.y < 0)
            snake.Transform.position = new Vector3(snake.Transform.position.x, snake.Transform.position.y + Config.YSize, 0);
    }
}