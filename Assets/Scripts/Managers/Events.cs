using UnityEngine;

public class GameEvent
{
}

public static class Events
{
    public static PlayerLoseEvent PlayerLoseEvent = new();
    public static GameStartedEvent GameStartedEvent = new();
    public static PassColliderEvent PassColliderEvent = new();
}

public class PlayerLoseEvent : GameEvent
{
}

public class GameStartedEvent : GameEvent
{
}

public class PassColliderEvent : GameEvent
{
    
}