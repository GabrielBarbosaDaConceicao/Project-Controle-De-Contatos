﻿using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   usuario = _usuarioRepositorio.Adicionar(usuario);

                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {

                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);

            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o contato!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o contato, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}
