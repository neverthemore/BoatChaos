using System;

namespace ShipGame.Events
{
    public static class EventBus<TEvent>
    {
        private static event Action<TEvent> OnEvent;
        private static event Action OnGameStartEvent;

        public static void Subscribe(Action<TEvent> callback) => OnEvent += callback;
        public static void Unsubscribe(Action<TEvent> callback) => OnEvent -= callback;
        public static void Publish(TEvent evt) => OnEvent?.Invoke(evt);

    }

    public static class GameStartEventBus
    {
        private static event Action OnGameStart;
        private static event Action OnGameOver;
        private static event Action OnGameVictory;

        public static void SubscribeToGameStart(Action callback) => OnGameStart += callback;
        public static void UnsubscribeFromGameStart(Action callback) => OnGameStart -= callback;
        public static void PublishGameStart() => OnGameStart?.Invoke();


        public static void SubscribeToGameOver(Action callback) => OnGameOver += callback;
        public static void UnsubscribeFromGameOver(Action callback) => OnGameOver -= callback;
        public static void PublishGameOver() => OnGameOver?.Invoke();

        public static void SubscribeToGameVictory(Action callback) => OnGameVictory += callback;
        public static void UnsubscribeFromGameVictory(Action callback) => OnGameVictory -= callback;
        public static void PublishGameVictory() => OnGameVictory?.Invoke();
    }
}
