using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GameOverSystem))]
public sealed class GameOverSystem : UpdateSystem
{
    private Filter _snake;
    
    public override void OnAwake()
    {
        _snake = World.Filter.With<Snake>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var e in _snake)
        {
            var snake = e.GetComponent<Snake>();
            foreach (var body in snake.Body)
            {
                if (snake.Transform.position == body.transform.position)
                {
                    Application.Quit ();
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                }
            }
        }
    }
}