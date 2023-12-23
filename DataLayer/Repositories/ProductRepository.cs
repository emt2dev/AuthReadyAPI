using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AuthDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllunavailableAPIProducts()
        {
            List<StyleClass> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == false).ToListAsync();
            _context.ChangeTracker.Clear();

            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            foreach (var item in StyleList)
            {
                ProductClass P = await _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync();
                ProductList.Add(new ProductWithStyleDTO(_mapper.Map<ProductDTO>(P), _mapper.Map<StyleDTO>(item)));
            }

            return ProductList;
        }
        public async Task<List<ProductWithStyleDTO>> GetAllUnavailableCompanyProducts(int CompanyId)
        {
            List<StyleClass> StyleList = await _context.Styles.Where(x => x.CompanyId == CompanyId && x.IsAvailableForOrder == false).ToListAsync();
            _context.ChangeTracker.Clear();

            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            foreach (var item in StyleList)
            {
                ProductClass P = await _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync();
                ProductList.Add(new ProductWithStyleDTO(_mapper.Map<ProductDTO>(P), _mapper.Map<StyleDTO>(item)));
            }

            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllAvailableAPIProducts()
        {
            List<StyleClass> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == true).ToListAsync();
            _context.ChangeTracker.Clear();

            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            foreach (var item in StyleList)
            {
                ProductClass P = await _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync();
                ProductList.Add(new ProductWithStyleDTO(_mapper.Map<ProductDTO>(P), _mapper.Map<StyleDTO>(item)));
            }

            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllAvailableCompanyProducts(int CompanyId)
        {

            List<StyleClass> StyleList = await _context.Styles.Where(x => x.CompanyId == CompanyId && x.IsAvailableForOrder == true).ToListAsync();
            _context.ChangeTracker.Clear();

            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            foreach (var item in StyleList)
            {
                ProductClass P = await _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync();
                ProductList.Add(new ProductWithStyleDTO(_mapper.Map<ProductDTO>(P), _mapper.Map<StyleDTO>(item)));
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetAPIProducts()
        {
            return await _context.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAPIProductsByCategoryId(int CategoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == CategoryId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetAPIProductsByKeyword(string Keyword)
        {
            if (!_context.Categories.Any(x => x.Name == Keyword)) return new List<ProductDTO>();

            CategoryClass Found = await _context.Categories.Where(x => x.Name == Keyword).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();

            if (Found is null) return new List<ProductDTO>();

            return await _context.Products.Where(x => x.CategoryId == Found.Id).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProducts(int CompanyId)
        {
            return await _context.Products.Where(x => x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByCategoryName(string CategoryName, int CompanyId)
        {
            if (!_context.Categories.Any(x => x.Name == CategoryName && x.CompanyId == CompanyId)) return new List<ProductDTO>();

            return await _context.Products.Where(x => x.Name == CategoryName && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId)
        {
            if (!_context.Categories.Any(x => x.Name == Keyword && x.CompanyId == CompanyId)) return new List<ProductDTO>();

            CategoryClass Found = await _context.Categories.Where(x => x.Name == Keyword && x.CompanyId == CompanyId).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();

            if (Found is null) return new List<ProductDTO>();

            return await _context.Products.Where(x => x.CategoryId == Found.Id && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductDTO> GetProduct(int ProductId)
        {
            if (!_context.Products.Any(x => x.Id == ProductId)) return null;
            return await _context.Products.Where(x => x.Id == ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<ProductWithStyleDTO> GetProductCartCount()
        {
            StyleClass Styles = await _context.Styles.OrderByDescending(x => x.CartCount).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductWithStyleDTO> GetProductCartCount(int CompanyId)
        {
            StyleClass Styles = await _context.Styles.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(x => x.CartCount).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductWithStyleDTO> GetProductGrossIncome()
        {
            StyleClass Styles = await _context.Styles.OrderByDescending(x => x.GrossIncome).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductWithStyleDTO> GetProductGrossIncome(int CompanyId)
        {
            StyleClass Styles = await _context.Styles.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(x => x.GrossIncome).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductWithStyleDTO> GetProductOrderCount()
        {
            StyleClass Styles = await _context.Styles.OrderByDescending(x => x.OrderCount).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductWithStyleDTO> GetProductOrderCount(int CompanyId)
        {
            StyleClass Styles = await _context.Styles.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(x => x.OrderCount).FirstOrDefaultAsync();

            _context.ChangeTracker.Clear();

            ProductClass Product = await _context.Products.Where(x => x.Id == Styles.ProductId).FirstOrDefaultAsync();

            ProductWithStyleDTO DTO = new ProductWithStyleDTO(_mapper.Map<ProductDTO>(Product), _mapper.Map<StyleDTO>(Styles));
            return DTO;
        }

        public async Task<ProductDTO> GetProductViewCount()
        {
            return await _context.Products.OrderByDescending(p => p.ViewCount)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDTO> GetProductViewCount(int CompanyId)
        {
            return await _context.Products.Where(x => x.CompanyId == CompanyId)
                .OrderByDescending(p => p.ViewCount)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<StyleDTO>> GetAvailableProductStyles(int ProductId)
        {
            if (!await _context.Products.AnyAsync(x => x.Id == ProductId)) return new List<StyleDTO>();
            List<StyleDTO> List = await _context.Styles.Where(x => x.ProductId == ProductId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();

            _context.ChangeTracker.Clear();

            foreach (var item in List)
            {
                var Image = await _context.ProductImages.Where(x => x.StyleId == item.Id).FirstOrDefaultAsync();
                item.ProductImageUrls.Add(Image.ImageUrl);
            }

            return List;
        }

        // Add Rows
        public async Task<bool> NewCategory(NewCategoryDTO IncomingDTO)
        {
            CategoryClass Obj = _mapper.Map<CategoryClass>(IncomingDTO);

            int LastId = await _context.Categories.CountAsync();

            Obj.Id = LastId++; // prevents errors when saving new entities

            await _context.Categories.AddAsync(Obj);
            await _context.SaveChangesAsync();

            if (await _context.Categories.Where(x => x.Id == Obj.Id).FirstOrDefaultAsync() is not null) return true;

            return false;
        }
        public async Task<bool> NewProduct(NewProductDTO IncomingDTO)
        {
            ProductClass Obj = _mapper.Map<ProductClass>(IncomingDTO);

            int LastId = await _context.Products.CountAsync();

            Obj.Id = LastId++;

            await _context.Products.AddAsync(Obj);
            await _context.SaveChangesAsync();

            if (await _context.Products.Where(x => x.Id == Obj.Id).FirstOrDefaultAsync() is not null) return true;

            return false;
        }
        public async Task<bool> NewStyle(NewStyleDTO IncomingDTO)
        {
            StyleClass Obj = _mapper.Map<StyleClass>(IncomingDTO);

            int LastId = await _context.Styles.CountAsync();

            Obj.Id = LastId++;

            await _context.Styles.AddAsync(Obj);
            await _context.SaveChangesAsync();

            if (await _context.Styles.Where(x => x.Id == Obj.Id).FirstOrDefaultAsync() is not null) return true;

            return false;
        }
        public async Task<bool> NewProductImage(NewProductImageDTO IncomingDTO)
        {
            ProductImageClass Obj = _mapper.Map<ProductImageClass>(IncomingDTO);

            int LastId = await _context.ProductImages.CountAsync();

            Obj.Id = LastId++;

            await _context.ProductImages.AddAsync(Obj);
            await _context.SaveChangesAsync();

            if (await _context.ProductImages.Where(x => x.Id == Obj.Id).FirstOrDefaultAsync() is not null) return true;

            return false;
        }

        // Update Rows
        public async Task<bool> SetStyleToAvailable(int StyleId)
        {
            if (!await _context.Styles.AnyAsync(x => x.Id == StyleId)) return false;

            StyleClass Obj = await _context.Styles.Where(x => x.Id == StyleId).FirstOrDefaultAsync();
            Obj.AvailableOn = DateTime.Now;
            Obj.IsAvailableForOrder = true;

            _context.Styles.Update(Obj);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> SetStyleToUnavailable(int StyleId)
        {
            if (!await _context.Styles.AnyAsync(x => x.Id == StyleId)) return false;

            StyleClass Obj = await _context.Styles.Where(x => x.Id == StyleId).FirstOrDefaultAsync();
            Obj.UnavailableOn = DateTime.Now;
            Obj.IsAvailableForOrder = false;

            _context.Styles.Update(Obj);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProduct(ProductDTO IncomingDTO)
        {
            ProductClass Obj = _mapper.Map<ProductClass>(IncomingDTO);

            _context.Products.Update(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateCategory(CategoryDTO IncomingDTO)
        {
            CategoryClass Obj = _mapper.Map<CategoryClass>(IncomingDTO);

            _context.Categories.Update(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateStyle(StyleDTO IncomingDTO)
        {
            StyleClass Obj = _mapper.Map<StyleClass>(IncomingDTO);

            _context.Styles.Update(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
