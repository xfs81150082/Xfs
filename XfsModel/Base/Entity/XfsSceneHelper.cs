namespace Xfs
{
    public static class XfsSceneHelper
    {
        public static int DomainZone(this XfsEntity entity)
        {
            return ((XfsScene)entity.Domain).Zone;
        }

        public static XfsScene DomainScene(this XfsEntity entity)
        {
            return (XfsScene)entity.Domain;
        }
    }
}