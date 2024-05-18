namespace GameZone.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);

        Task Create(CreateGameFormVM model);
        Task<Game?> Update(EditGameFormVM model);
        bool Delete(int id);


    }
}
