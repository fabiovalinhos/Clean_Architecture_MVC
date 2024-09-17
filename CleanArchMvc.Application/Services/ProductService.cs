using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        }

        // public async Task Add(ProductDTO productDTO)
        // {
        //     var productEntity = _mapper.Map<Product>(productDTO);
        //     await _productRepository.CreateAsync(productEntity);
        // }

        // public async Task<ProductDTO> GetById(int? id)
        // {
        //     var productEntity = await _productRepository.GetByIdAsync(id);
        //     return _mapper.Map<ProductDTO>(productEntity);
        // }

        // public async Task<ProductDTO> GetProductCategory(int? id)
        // {
        //     var productEntity = await _productRepository.GetProductCategoryAsync(id);
        //     return _mapper.Map<ProductDTO>(productEntity);
        // }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var getProductsQuery = new GetProductsQuery();

            if (getProductsQuery is null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(getProductsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        // public async Task Remove(int? id)
        // {
        //     var productEntity = _productRepository.GetByIdAsync(id).Result;
        //     await _productRepository.RemoveAsync(productEntity);
        // }

        // public async Task Update(ProductDTO productDTO)
        // {
        //     var productEntity = _mapper.Map<Product>(productDTO);
        //     await _productRepository.UpdateAsync(productEntity);
        // }
    }
}