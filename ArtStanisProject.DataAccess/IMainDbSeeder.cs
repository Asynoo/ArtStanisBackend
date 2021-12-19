namespace ArtStanisProject.DataAccess
{
    public interface IMainDbSeeder
    {
        void SeedDevelopment();
        void SeedProduction();
    }
}