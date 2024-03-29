﻿using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly UserManager<APIUserClass> _userManager;

        public ProductRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService, UserManager<APIUserClass> userManager)
        {
            _mapper = mapper;
            _context = context;
            _mediaService = mediaService;
            _userManager = userManager;
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
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO { Product = Product, Styles = AssociatedStyles };

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
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO { Product = Product, Styles = AssociatedStyles };

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
                    ProductWithStyleDTO FullProduct = new ProductWithStyleDTO { Product = Product, Styles = AssociatedStyles };

                    ProductList.Add(FullProduct);
                }
            }

            return ProductList;
        }

        public async Task<List<ProductWithStyleDTO>> GetAllAvailableCompanyProducts(int CompanyId)
        {
            List<ProductWithStyleDTO> ProductWithStyleList = new List<ProductWithStyleDTO>();

            List<StyleDTO> StyleList = await _context.Styles
                .Where(x => x.IsAvailableForOrder == true && x.CompanyId == CompanyId)
                .ProjectTo<StyleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            _context.ChangeTracker.Clear();

            foreach (var item in StyleList)
            {
                ProductDTO Product = await _context.Products
                    .Where(x => x.Id == item.ProductId)
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (Product is not null)
                {
                    // Check if a ProductWithStyleDTO with the same ProductId already exists
                    var existingProductWithStyle = ProductWithStyleList.FirstOrDefault(pws => pws.Id == Product.Id);

                    if (existingProductWithStyle is null)
                    {
                        // If it doesn't exist, create a new one
                        ProductWithStyleDTO productWithStyle = new ProductWithStyleDTO
                        {
                            Id = Product.Id,
                            Product = Product,
                            Styles = new List<StyleDTO> { item }
                        };

                        ProductWithStyleList.Add(productWithStyle);
                    }
                    else existingProductWithStyle.Styles.Add(item); // If it exists, add the current StyleDTO to its Styles list
                }
            }

            return ProductWithStyleList;
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
            return await _context.Products
                .Where(x => (x.Name.ToLower().Contains(Keyword.ToLower()) || x.Description.ToLower().Contains(Keyword.ToLower()) || x.SEO.ToLower().Contains(Keyword.ToLower())))
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetCompanyProducts(int CompanyId)
        {
            return await _context.Products.Where(x => x.CompanyId == CompanyId).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<string>> GetAllCompanyCategories(int CompanyId)
        {
            List<CategoryDTO> List = await _context.Categories.Where(x => x.CompanyId == CompanyId).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
            List<string> ReturnList = new List<string>();

            foreach (var item in List)
            {
                ReturnList.Add(item.Name);
            }

            return ReturnList;
        }

        public async Task<List<ProductDTO>> GetCompanyProductsByKeyword(string Keyword, int CompanyId)
        {
            return await _context.Products
                .Where(x => (x.Name.ToLower().Contains(Keyword.ToLower()) || x.Description.ToLower().Contains(Keyword.ToLower()) || x.SEO.ToLower().Contains(Keyword.ToLower())) && x.CompanyId == CompanyId)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductWithStyleDTO> GetProduct(int ProductId)
        {
            ProductClass PClass = await _context.Products.Where(x => x.Id == ProductId).FirstOrDefaultAsync();
            if (PClass is null) return null;
            PClass.ViewCount++;
            _context.Products.Update(PClass);
            await _context.SaveChangesAsync();

            ProductDTO Product = _mapper.Map<ProductDTO>(PClass);

            _context.ChangeTracker.Clear();
            ProductImageClass Image = await _context.ProductImages.Where(x => x.ProductId == Product.Id).FirstOrDefaultAsync();
            Product.MainImageUrl = Image.ImageUrl;

            _context.ChangeTracker.Clear();
            List<StyleDTO> Styles = await _context.Styles.Where(x => x.ProductId == ProductId).ProjectTo<StyleDTO>(_mapper.ConfigurationProvider).ToListAsync();
            if (Styles.Count < 1) return null;

            _context.ChangeTracker.Clear();
            foreach (var item in Styles)
            {
                item.ProductImageUrls = new List<string>();

                List<ProductImageClass> PIC = await _context.ProductImages.Where(x => x.StyleId == item.Id).ToListAsync();
                foreach (var obj in PIC)
                {
                    item.ProductImageUrls.Add(obj.ImageUrl);
                }
            }            

            ProductWithStyleDTO Full = new ProductWithStyleDTO { Product = Product, Styles = Styles };
            
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

            ProductWithStyleDTO Full = new ProductWithStyleDTO { Product = Product, Styles = Styles };

            return Full;
        }
        public async Task<List<SingleProductCartDTO>> GetUserSingles(string UserId)
        {
            APIUserClass User = await _userManager.FindByIdAsync(UserId);
            if (User is null) return null;

            return await _context.SingleProductCarts.Where(x => x.UserId == UserId && !x.Submitted && !x.Abandoned).ProjectTo<SingleProductCartDTO>(_mapper.ConfigurationProvider).ToListAsync();
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
        public async Task<List<ProductUpsellItemDTO>> GetAllUpsellItems()
        {
            return await _context.ProductUpsells.ProjectTo<ProductUpsellItemDTO>(_mapper.ConfigurationProvider).ToListAsync();
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
            var Image = await _mediaService.AddPhotoAsync(IncomingDTO.ImageUrl);

            ProductClass Obj = new ProductClass(IncomingDTO, Image.Url.ToString());

            await _context.Products.AddAsync(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> NewStyle(NewStyleDTO IncomingDTO)
        {
            ProductClass Exists = await _context.Products.Where(x => x.Id == IncomingDTO.ProductId).FirstOrDefaultAsync();

            if (Exists is null) return false;

            List<string> ImageUrls = new List<string>();

            foreach (var image in IncomingDTO.Images)
            {
                var Image = await _mediaService.AddPhotoAsync(image);
                ImageUrls.Add(Image.Url.ToString());
            }

            StyleClass Obj = new StyleClass(IncomingDTO);

            await _context.Styles.AddAsync(Obj);
            await _context.SaveChangesAsync();

            if (await _context.Styles.Where(x => x.Id == Obj.Id).FirstOrDefaultAsync() is null) return false;

            _context.ChangeTracker.Clear();
            foreach (var item in ImageUrls)
            {
                ProductImageClass PIObj = new ProductImageClass
                {
                    Id = 0,
                    StyleId = Obj.Id,
                    ProductId = Exists.Id,
                    ImageUrl = item
                };

                await _context.ProductImages.AddAsync(PIObj);
                await _context.SaveChangesAsync();
            }

            return true;
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
