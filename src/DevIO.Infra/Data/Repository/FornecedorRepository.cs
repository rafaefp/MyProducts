﻿using DevIO.Business.Models.Fornecedores;
using DevIO.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext db) : base(db)
        {
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                                        .Include(f => f.Endereco)
                                        .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                                        .Include(f => f.Endereco)
                                        .Include(f => f.Produtos)
                                        .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
