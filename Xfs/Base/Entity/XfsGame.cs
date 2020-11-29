namespace Xfs
{
    public static class XfsGame
    {
        public static XfsEventSystem EventSystem
        {
            get
            {
                return XfsEventSystem.Instance;
            }
        }

        private static XfsScene scene;

        public static XfsScene Scene
        {
            get
            {
                return scene ?? (scene = XfsEntitySceneFactory.CreateScene(1, XfsSceneType.Process, "Process"));
            }
        }

        public static XfsObjectPool ObjectPool
        {
            get
            {
                return XfsObjectPool.Instance;
            }
        }

        public static void Close()
        {
            scene?.Dispose();
            scene = null;
            XfsObjectPool.Instance.Dispose();
            XfsEventSystem.Instance.Dispose();
        }
    }
}