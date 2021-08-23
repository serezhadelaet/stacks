using Level;
using Player;
using UI;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoInstaller
{
    [SerializeField] private GameOverlay gameOverlay = null;
    [SerializeField] private WinOverlay winOverlay = null;
    [SerializeField] private LoseOverlay loseOverlay = null;
    [SerializeField] private GuideOverlay guideOverlay = null;
    [SerializeField] private LevelsSwitcher levelsSwitcher = null;

    public override void InstallBindings()
    {
        BindGameOverlay();
        BindGuideOverlay();
        BindWinOverlay();
        BindLoseOverlay();
        BindLevelsFactory();
        BindLevelsSwitcher();
        BindPlayersFactory();
    }

    private void BindGameOverlay()
    {
        Container
            .Bind<GameOverlay>()
            .FromInstance(gameOverlay)
            .AsSingle();
    }

    private void BindGuideOverlay()
    {
        Container
            .Bind<GuideOverlay>()
            .FromInstance(guideOverlay)
            .AsSingle();
    }
    
    private void BindWinOverlay()
    {
        Container
            .Bind<WinOverlay>()
            .FromInstance(winOverlay)
            .AsSingle();
    }
    
    private void BindLoseOverlay()
    {
        Container
            .Bind<LoseOverlay>()
            .FromInstance(loseOverlay)
            .AsSingle();
    }
    
    private void BindLevelsFactory()
    {
        Container.BindFactory<GameObject, Level.Level, LevelsFactory>().FromFactory<PrefabFactory<Level.Level>>();
    }
    
    private void BindPlayersFactory()
    {
        Container.BindFactory<GameObject, BasePlayer, PlayersFactory>()
            .FromFactory<PrefabFactory<BasePlayer>>();
    }
    
    private void BindLevelsSwitcher()
    {
        Container
            .Bind<LevelsSwitcher>()
            .FromInstance(levelsSwitcher)
            .AsSingle();
    }
}