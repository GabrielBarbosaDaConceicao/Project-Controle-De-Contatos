using ControleDeContatos.Models;
using System.Collections.Generic;


namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contado);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);

    }
}
