using Project.Application.Models;

namespace Project.Repositories;

public class PessoaRepository : IRepository<Pessoa>
{
    private static Dictionary<int, Pessoa> pessoas = new Dictionary<int, Pessoa>();

    private int GetNextId()
    {
        if (pessoas.Keys.Count > 0)
            return pessoas.Keys.Max() + 1;

        return 1;
    }

    public async Task Add(Pessoa pessoa)
    {
        int id = GetNextId();
        pessoa.Id = id;

        await Task.Run(() => pessoas.Add(id, pessoa));
    }

    public async Task<bool> Delete(int id)
    {
        return await Task.Run(() => pessoas.Remove(id));
    }

    public async Task Edit(Pessoa pessoa)
    {
        await Task.Run(() =>
        {
            pessoas.Remove(pessoa.Id); 
            pessoas.Add(pessoa.Id, pessoa);
        });
    }

    public async Task<Pessoa> Get(int id)
    {
        return await Task.Run(() => pessoas.GetValueOrDefault(id)); 
    }

    public async Task<IEnumerable<Pessoa>> GetAll()
    {
        return await Task.Run(() => pessoas.Values.ToList());
    }
}
