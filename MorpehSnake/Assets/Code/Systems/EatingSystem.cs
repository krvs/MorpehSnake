using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EatingSystem))]
public sealed class EatingSystem : UpdateSystem
{
    public GameConfig Config;
    private Filter _apple;
    private Filter _snake;
    
    public override void OnAwake()
    {
        _apple = World.Filter.With<Apple>();
        _snake = World.Filter.With<Snake>();
    }

    public override void OnUpdate(float deltaTime) {
        var appleBag = _apple.Select<Apple>();
        ref var apple = ref appleBag.GetComponent(0);

        var snakeBag = _snake.Select<Snake>();
        ref var snake = ref snakeBag.GetComponent(0);

        if (snake.Transform.position == apple.Transform.position)
        {
            snake.IsGrowing = true;
            snake.Score++;
            var position = new Vector2(Random.Range(0, Config.XSize), Random.Range(0, Config.YSize));
            apple.Transform.position = position + Vector2.one * 0.5f;
        }
    }
}