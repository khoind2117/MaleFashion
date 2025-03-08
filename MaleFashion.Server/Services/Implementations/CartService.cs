using MaleFashion.Server.Models.DTOs.Cart;
using MaleFashion.Server.Models.DTOs.CartItem;
using MaleFashion.Server.Models.DTOs.Color;
using MaleFashion.Server.Models.DTOs.Product;
using MaleFashion.Server.Models.DTOs.ProductVariant;
using MaleFashion.Server.Models.DTOs.Size;
using MaleFashion.Server.Models.Entities;
using MaleFashion.Server.Repositories.Interfaces;
using MaleFashion.Server.Services.Interfaces;
using System.Security.Claims;

namespace MaleFashion.Server.Services.Implementations
{
    public class CartService : ICartService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private Guid? GetBasketId()
        {
            var cookies = _httpContextAccessor.HttpContext?.Request.Cookies;

            if (cookies != null && cookies.ContainsKey("BasketId"))
            {
                return Guid.Parse(cookies["BasketId"]);
            }

            return null;
        }

        private Guid CreateBasketIdAndSetInCookies()
        {
            var newBasketId = Guid.NewGuid();

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("BasketId", newBasketId.ToString(),
                new CookieOptions { Expires = DateTime.UtcNow.AddMonths(3) });

            return newBasketId;
        }

        public async Task<CartDto?> GetCartAsync()
        {
            var userId = GetUserId();
            var basketId = GetBasketId();

            Cart? cart = null;
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            }
            else if (basketId.HasValue && basketId != Guid.Empty)
            {
                cart = await _unitOfWork.CartRepository.GetCartByBasketIdAsync(basketId.Value);
            }

            if (cart == null)
            {
                return null;
            }

            return new CartDto
            {
                UserId = userId,
                BasketId = basketId,
                LastUpdated = cart.LastUpdated,
                CartItemDtos = cart.CartItems?.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    ProductVariantId = item.ProductVariantId,
                    ProductVariantDto = new ProductVariantDto
                    {
                        Id = item.ProductVariant.Id,
                        Stock = item.ProductVariant.Stock,
                        ProductDto = new ProductDto
                        {
                            Id = item.ProductVariant.Product.Id,
                            Name = item.ProductVariant.Product.Name,
                            Price = item.ProductVariant.Product.Price,
                            IsActive = item.ProductVariant.Product.IsActive,
                        },
                        ColorDto = new ColorDto
                        {
                            Id = item.ProductVariant.Color.Id,
                            Name = item.ProductVariant.Color.Name,
                            ColorCode = item.ProductVariant.Color.ColorCode,
                        },
                        SizeDto = new SizeDto
                        {
                            Id = item.ProductVariant.Size.Id,
                            Name = item.ProductVariant.Size.Name,
                        }
                    },
                    CartId = cart.Id,
                }).ToList()
            };
        }

        public async Task<bool> MergeCartAsync()
        {
            var userId = GetUserId();
            var basketId = GetBasketId();
         
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var userCart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            var guestCart = await _unitOfWork.CartRepository.GetCartByBasketIdAsync(basketId.Value);

            if (guestCart == null)
            {
                return false;
            }

            if (userCart == null)
            {
                userCart = new Cart
                {
                    UserId = userId,
                    BasketId = null,
                    LastUpdated = DateTime.Now,
                    CartItems = guestCart.CartItems
                };

                await _unitOfWork.CartRepository.AddAsync(userCart);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                foreach (var guestItem in guestCart.CartItems)
                {
                    var existingItem = userCart.CartItems?.FirstOrDefault(item => item.ProductVariantId == guestItem.ProductVariantId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += guestItem.Quantity;
                    }
                    else
                    {
                        userCart.CartItems?.Add(new CartItem
                        {
                            ProductVariantId = guestItem.ProductVariantId,
                            Quantity = guestItem.Quantity
                        });
                    }
                }

                await _unitOfWork.CartRepository.UpdateAsync(userCart);
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CartRepository.DeleteAsync(guestCart.Id);
            await _unitOfWork.SaveChangesAsync();

            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("BasketId");

            return true;
        }

        public async Task<bool> AddProductToCartAsync(int productVariantId, int quantity)
        {
            var userId = GetUserId();
            var basketId = GetBasketId();

            if (string.IsNullOrEmpty(userId) && basketId == null)
            {
                basketId = CreateBasketIdAndSetInCookies();
            }

            Cart? cart = null;
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            }
            else if (basketId.HasValue && basketId != Guid.Empty)
            {
                cart = await _unitOfWork.CartRepository.GetCartByBasketIdAsync(basketId.Value);
            }

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    BasketId = basketId,
                    LastUpdated = DateTime.Now,
                    CartItems = new List<CartItem>()
                };
                await _unitOfWork.CartRepository.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            var cartItem = cart.CartItems?.FirstOrDefault(item => item.ProductVariantId == productVariantId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems?.Add(new CartItem
                {
                    ProductVariantId = productVariantId,
                    Quantity = quantity,
                });
            }

            cart.LastUpdated = DateTime.Now;

            await _unitOfWork.CartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveProductFromCartAsync(int productVariantId)
        {
            var userId = GetUserId();
            var basketId = GetBasketId();

            Cart? cart = null;
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            }
            else if (basketId.HasValue && basketId != Guid.Empty)
            {
                cart = await _unitOfWork.CartRepository.GetCartByBasketIdAsync(basketId.Value);
            }

            if (cart == null)
            {
                return false;
            }

            var cartItem = cart.CartItems?.FirstOrDefault(item => item.ProductVariantId == productVariantId);
            if (cartItem == null)
            {
                return false;
                
            }

            cart.CartItems?.Remove(cartItem);

            cart.LastUpdated = DateTime.Now;

            await _unitOfWork.CartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
