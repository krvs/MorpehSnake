using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GrowingSystem))]
public sealed class GrowingSystem : UpdateSystem
{
    public GameConfig Config;
    
    private Filter _snake;
    
    public override void OnAwake()
    {
        _snake = World.Filter.With<Snake>();
    }

    public override void OnUpdate(float deltaTime) {
        var snakeBag = _snake.Select<Snake>();
        ref var snake = ref snakeBag.GetComponent(0);

        if (snake.IsGrowing)
        {
            snake.IsGrowing = false;
            var bodyPart = Instantiate(Config.SnakePrefab, new Vector2(100, 100), Quaternion.identity);
            snake.Body.Add(bodyPart.transform);
        }
    }
}