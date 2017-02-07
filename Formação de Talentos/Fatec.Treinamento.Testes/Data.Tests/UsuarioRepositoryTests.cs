﻿using System;
using System.Diagnostics;
using System.Linq;
using Fatec.Treinamento.Data.Repositories;
using Fatec.Treinamento.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fatec.Treinamento.Testes.Data.Tests
{
    [TestClass]
    public class UsuarioRepositoryTests
    {
        
        [TestMethod]
        public void TestarCrudUsuario()
        {
            var id = InserirUsuario();
            var usuario = ObterUsuario(id);
            usuario = AtualizarUsuario(usuario);
            ObterPorNome(usuario.Nome.Substring(0, 3));
            FazerLogin(usuario.Email, "123456");
            Excluir(usuario);
            Listar();

        }

        private void Listar()
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
                var lista = repo.Listar();
                Assert.IsTrue(lista.Count() > 1);
            }
        }

        private void FazerLogin(string email, string senha)
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
                var usuario = repo.Login(email, senha);
                Assert.IsTrue(usuario != null);
                Assert.AreEqual(email, usuario.Email);
                Assert.IsTrue(usuario.Perfil != null);
            }
        }
        private int InserirUsuario()
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
                var usuario = new Usuario
                {
                    Nome = "Demo teste 1",
                    Email = "teste@teste.com",
                    Senha = "123456",
                    Ativo = true,
                    Perfil = new Perfil { Id = 2, Nome = "Usuário" }
                };

                int id = repo.Inserir(usuario).Id ;

                Assert.IsTrue(id > 0);
                return id;
            }
        }
        
        public Usuario ObterUsuario(int id)
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
               
                var usuario = repo.Obter(id);
                
                Assert.AreEqual("Demo teste 1", usuario.Nome);
                Assert.IsTrue(usuario.Perfil != null);
                Assert.AreEqual(2, usuario.Perfil.Id);
                
                return usuario;

            }
        }


        private Usuario AtualizarUsuario(Usuario usuario)
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {

                usuario.Nome = "Nome Atualizado";
                repo.Atualizar(usuario);

                var usuarioAtualizado = repo.Obter(usuario.Id);

                Assert.AreEqual("Nome Atualizado", usuarioAtualizado.Nome);
                
                return usuarioAtualizado;
            }
        }


        private void ObterPorNome(string nome)
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
                var usuarios = repo.ListarPorNome(nome);

                Assert.IsTrue(usuarios.Any());
            }
        }

        private void Excluir(Usuario usuario)
        {
            using (UsuarioRepository repo = new UsuarioRepository())
            {
                repo.Excluir(usuario);
                var usuarioRet = repo.Obter(usuario.Id);
                Assert.IsTrue(usuarioRet == null);
            }
        }
    }
}
