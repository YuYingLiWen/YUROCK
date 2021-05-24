namespace YuSystem.Gameplay.Utilities
{
    /// <summary>
    /// Defaut = 0,
    /// UI = 5,
    /// PlayerCollectibles = 27,
    /// Projectiles = 28,
    /// Asteroid = 29,
    /// Enemies = 30,
    /// Player = 31
    /// </summary>
    public enum Layers
    {
        Defaut,
        UI,
        Player,
        Projectiles,
        Enemies,
        Asteroids,
        Collectibles,
        Floor, //Added due to collision incompatiblity between the minigames, should be default but it is checked off...
        OutOfBounds //Added due to collision incompatiblity between the minigames, should be default but it is checked off...
    }
}
