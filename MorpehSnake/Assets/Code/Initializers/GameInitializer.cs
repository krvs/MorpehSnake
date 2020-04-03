using System.Collections.Generic;
using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(GameInitializer))]
public sealed class GameInitializer : Initializer
{
    [SerializeField] private GameConfig _config;
    
    public override void OnAwake()
    {
        var gameEntity = World.CreateEntity();
        gameEntity.AddComponent<InputData>();

        var snakeEntity = World.CreateEntity();
        var snake = snakeEntity.AddComponent<Snake>();
        var snakeObject = Instantiate(_config.SnakePrefab, Vector2.one * 0.5f, Quaternion.identity);
        snake.Transform = snakeObject.transform;
        snake.Body = new List<Transform>();
        snakeEntity.SetComponent(snake);

        var appleEntity = World.CreateEntity();
        var apple = appleEntity.AddComponent<Apple>();
        var position = new Vector2(Random.Range(0, _config.XSize), Random.Range(0, _config.YSize));
        var appleObject = Instantiate(_config.ApplePrefab, position + Vector2.one * 0.5f, Quaternion.identity);
        apple.Transform = appleObject.transform;
        appleEntity.SetComponent(apple);

        Camera.main.orthographicSize = Mathf.Sqrt(_config.XSize * _config.YSize) / 2;
        Camera.main.transform.position = new Vector3(_config.XSize / 2, _config.YSize / 2, -1f);
    }

    public override void Dispose() {
    }
}