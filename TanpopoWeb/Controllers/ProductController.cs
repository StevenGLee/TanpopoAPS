using Microsoft.AspNetCore.Mvc;
using TanpopoWeb.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace TanpopoWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private List<ProductViewModel> _products;

        // 模拟从数据库加载产品数据
        public async Task<List<ProductViewModel>> GetProductsAsync()
        {
            // 在实际应用中，这里应该调用数据库或服务来获取数据
            await Task.Delay(100); // 模拟异步操作

            if (_products == null)
            {
                _products = ProductViewModel.GenerateSampleProducts();
            }

            return _products;
        }

        // 新建或更新产品
        [HttpPost("SaveProduct")]
        public IActionResult SaveProduct([FromBody] ProductViewModel product)
        {
            try
            {
                if (_products == null)
                {
                    _products = new List<ProductViewModel>();
                }

                if (product.Id == 0)
                {
                    // 新建产品
                    product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
                    _products.Add(product);
                    return Ok(new { Success = true, Message = $"产品 {product.Name} 已创建" });
                }
                else
                {
                    // 更新现有产品
                    var originalProduct = _products.FirstOrDefault(p => p.Id == product.Id);
                    if (originalProduct == null)
                    {
                        return NotFound(new { Success = false, Message = $"未找到 ID 为 {product.Id} 的产品" });
                    }

                    originalProduct.ProductCode = product.ProductCode;
                    originalProduct.Name = product.Name;
                    originalProduct.Specification = product.Specification;
                    originalProduct.Version = product.Version;
                    originalProduct.ProductType = product.ProductType;
                    originalProduct.MinProductionQuantity = product.MinProductionQuantity;
                    originalProduct.MaxProductionQuantity = product.MaxProductionQuantity;
                    originalProduct.ProductionLeadTime = product.ProductionLeadTime;
                    originalProduct.IsActive = product.IsActive;

                    return Ok(new { Success = true, Message = $"产品 {product.Name} 已更新" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = $"保存产品失败: {ex.Message}" });
            }
        }

        // 删除产品
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                if (_products == null)
                {
                    _products = new List<ProductViewModel>();
                }

                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return NotFound(new { Success = false, Message = $"未找到 ID 为 {productId} 的产品" });
                }

                _products.Remove(product);
                return Ok(new { Success = true, Message = $"产品 {product.Name} 已删除" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = $"删除产品失败: {ex.Message}" });
            }
        }
    }
}