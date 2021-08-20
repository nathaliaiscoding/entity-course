using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class LojaContext : DbContext
    {
        //nome da propriedade é o mesmo nome da tabela, definido no banco de dados.
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Endereco>().Property<int>("ClientId");
            modelBuilder.Entity<Endereco>().HasKey("ClientId");
            modelBuilder.Entity<Cliente>().HasOne(e => e.EnderecoDeEntrega).WithOne(c => c.Cliente).HasForeignKey<Endereco>("ClientId");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = LojaDB; Trusted_Connection = true;");
        }

    }
}