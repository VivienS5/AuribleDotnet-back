namespace Aurible.Services
{
    public interface ILivreService
    {
        string GetLivreById(int id);
    }

    public class LivreService : ILivreService
    {
        public string GetLivreById(int id)
        {
            // Logique pour récupérer un livre fictif par ID
            return $"Livre avec ID {id}";
        }
    }
}
