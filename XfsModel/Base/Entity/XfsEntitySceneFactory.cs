namespace Xfs
{
    public static class XfsEntitySceneFactory
    {
        public static XfsScene CreateScene(long id, int zone, XfsSceneType sceneType, string name, XfsScene parent = null)
        {
            XfsScene scene = new XfsScene(id, zone, sceneType, name);
            scene.IsRegister = true;
            scene.Parent = parent;
            scene.Domain = scene;

            return scene;
        }

        public static XfsScene CreateScene(int zone, XfsSceneType sceneType, string name, XfsScene parent = null)
        {
            long id = XfsIdGeneraterHelper.GenerateId();
            XfsScene scene = new XfsScene(id, zone, sceneType, name);
            scene.IsRegister = true;
            scene.Parent = parent;
            scene.Domain = scene;

            return scene;
        }
    }
}