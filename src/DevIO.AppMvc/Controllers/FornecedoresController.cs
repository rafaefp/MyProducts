using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Fornecedores.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DevIO.AppMvc.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, 
                                      IFornecedorService fornecedorService, 
                                      IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [Route("lista-de-fornecedores")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [Route("novo-fornecedor")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null) return HttpNotFound();

            return View(fornecedorViewModel);
        }

        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return HttpNotFound();

            return View(fornecedorViewModel);
        }

        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return HttpNotFound();

            await _fornecedorService.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
        
        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}