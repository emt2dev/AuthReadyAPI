using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            List<StyleDTO> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == false).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();            

            if(StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    List<StyleDTO> AssociatedStyles = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.ProductId == Product.Id).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO(Product, AssociatedStyles);

                    ProductList.Add(FullProduct);
                }
            }
            
            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllUnavailableCompanyProducts(int CompanyId)
        {
            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            List<StyleDTO> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.CompanyId == CompanyId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    List<StyleDTO> AssociatedStyles = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.ProductId == Product.Id).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO(Product, AssociatedStyles);

                    ProductList.Add(FullProduct);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllAvailableAPIProducts()
        {
            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            List<StyleDTO> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == true).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    List<StyleDTO> AssociatedStyles = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.ProductId == Product.Id).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO(Product, AssociatedStyles);

                    ProductList.Add(FullProduct);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllAvailableCompanyProducts(int CompanyId)
        {
            List<ProductWithStyleDTO> ProductList = new List<ProductWithStyleDTO>();

            List<StyleDTO> StyleList = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.CompanyId == CompanyId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    List<StyleDTO> AssociatedStyles = await _context.Styles.Where(x => x.IsAvailableForOrder == false && x.ProductId == Product.Id).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO(Product, AssociatedStyles);

                    ProductList.Add(FullProduct);
                }
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
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<CategoryClass> Categories = await _context.Categories.Where(x => x.Name.Contains(CategoryName) && x.CompanyId == CompanyId).ToListAsync();
            _context.ChangeTracker.Clear();

            foreach (var item in Categories)
            {
                ProductDTO p = await _context.Products.Where(x => x.Name.Contains(CategoryName) && x.CompanyId == CompanyId || x.CategoryId == item.Id && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                ProductList.Add(p);
            }

            _context.ChangeTracker.Clear();
            List<StyleClass> Styles = await _context.Styles.Where(x => x.Name.Contains(CategoryName) && x.CompanyId == CompanyId || x.Description.Contains(CategoryName) && x.CompanyId == CompanyId).ToListAsync();
            _context.ChangeTracker.Clear();

            foreach (var item in Styles)
            {
                ProductDTO p = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                ProductList.Add(p);
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId)
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<CategoryClass> Categories = await _context.Categories.Where(x => x.Name.Contains(Keyword) && x.CompanyId == CompanyId).ToListAsync();
            _context.ChangeTracker.Clear();

            foreach (var item in Categories)
            {
                ProductDTO p = await _context.Products.Where(x => x.Name.Contains(Keyword) && x.CompanyId == CompanyId || x.CategoryId == item.Id && x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                ProductList.Add(p);
            }

            _context.ChangeTracker.Clear();
            List<StyleClass> Styles = await _context.Styles.Where(x => x.Name.Contains(Keyword) && x.CompanyId == CompanyId || x.Description.Contains(Keyword) && x.CompanyId == CompanyId).ToListAsync();
            _context.ChangeTracker.Clear();

            foreach (var item in Styles)
            {
                ProductDTO p = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                ProductList.Add(p);
            }

            return ProductList;
        }

        public async Task<ProductWithStyleDTO> GetProduct(int ProductId)
        {
            List<StyleDTO> Styles = await _context.Styles.Where(x => x.ProductId == ProductId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();
            if (Styles.Count < 1) return null;

            ProductDTO Product = await _context.Products.Where(x => x.Id == ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (Product is null) return null;

            ProductWithStyleDTO Full = new ProductWithStyleDTO(Product, Styles);

            return Full;
        }

        public async Task<List<ProductDTO>> GetProductCartCount()
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.OrderByDescending(x => x.CartCount).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductCartCount(int CompanyId)
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.CartCount).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductGrossIncome()
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.OrderByDescending(x => x.GrossIncome).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductGrossIncome(int CompanyId)
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.GrossIncome).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductOrderCount()
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.OrderByDescending(x => x.OrderCount).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductOrderCount(int CompanyId)
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            List<StyleClass> StyleList = await _context.Styles.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.OrderCount).ToListAsync();

            if (StyleList.Count > 0)
            {
                foreach (var item in StyleList)
                {
                    ProductDTO Product = await _context.Products.Where(x => x.Id == item.ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                    _context.ChangeTracker.Clear();

                    ProductList.Add(Product);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductViewCount()
        {
            List<ProductDTO> ProductList = await _context.Products.OrderByDescending(x => x.ViewCount).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();

            return ProductList;
        }

        public async Task<List<ProductDTO>> GetProductViewCount(int CompanyId)
        {
            List<ProductDTO> ProductList = await _context.Products.Where(x => x.CompanyId == CompanyId).OrderByDescending(x => x.ViewCount).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();

            return ProductList;
        }

        public async Task<ProductWithStyleDTO> GetAvailableProductStyles(int ProductId)
        {
            List<StyleDTO> Styles = await _context.Styles.Where(x => x.ProductId == ProductId && x.IsAvailableForOrder == true).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            _context.ChangeTracker.Clear();
            if (Styles.Count < 1) return null;

            ProductDTO Product = await _context.Products.Where(x => x.Id == ProductId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (Product is null) return null;

            ProductWithStyleDTO Full = new ProductWithStyleDTO(Product, Styles);

            return Full;
        }
        public async Task<List<SingleProductCartDTO>> GetUserSingles(string UserId)
        {
            // APIUser User = await _userManager.FindById(UserId);
            //return await _context.SingleProducts.Where(x => x.UserEmail == User.Email && !x.Submitted && !x.Abandonded).ToListAsync();
            return null;
        }
        public async Task<List<SingleProductCartDTO>> GetCompanySingles(int CompanyId)
        {
            List<SingleProductCartClass> List = await _context.SingleProductCarts.Where(x => x.CompanyId == CompanyId && !x.Abandoned && !x.Submitted).ToListAsync();
            List<SingleProductCartDTO> OutgoingList = new List<SingleProductCartDTO>();

            foreach (var item in List)
            {
                SingleProductCartDTO OutgoingDTO = new SingleProductCartDTO(item);

                SingleProductDTO Product = new SingleProductDTO(item.Item);
                Product.AddImages(item.Item.Images);

                OutgoingDTO.AddItemDTO(Product);

                OutgoingList.Add(OutgoingDTO);
            }

            return OutgoingList;
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
            ProductClass Obj = new ProductClass(IncomingDTO);

            await _context.Products.AddAsync(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> NewStyle(NewStyleDTO IncomingDTO)
        {
            StyleClass Obj = new StyleClass(IncomingDTO);

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
