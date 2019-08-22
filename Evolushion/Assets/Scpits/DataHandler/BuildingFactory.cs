namespace MINI
{
    public class BuildingFactory
    {
        public static object Create(string id)
        {
            Building Building = new Building();
                    Building.SetID(id);
                    return Building;
        }
    }
}