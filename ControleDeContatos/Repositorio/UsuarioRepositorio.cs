using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;
        public UsuarioRepositorio(BancoContext context)
        {
            _context = context;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorId(int id) => _context.Usuarios.FirstOrDefault(u => u.Id ==id);

        public List<UsuarioModel> BuscarTodos() => _context.Usuarios.ToList();

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = BuscarPorId(usuario.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização do usuário!");
            
                usuarioDb.Nome = usuario.Nome;
                usuarioDb.Email = usuario.Email;
                usuarioDb.Login = usuario.Login;
                usuarioDb.Perfil = usuario.Perfil;
                usuarioDb.DataAtualizacao = DateTime.Now;
            
            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = BuscarPorId(id);

            if (usuarioDb == null) throw new Exception("Houve um erro na deleção do usuário!");

            _context.Usuarios.Remove(usuarioDb);
            _context.SaveChanges();

            return true;
        }
    }
}
