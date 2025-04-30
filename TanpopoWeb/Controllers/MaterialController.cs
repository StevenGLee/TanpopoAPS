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
    public class MaterialController : ControllerBase
    {
        private List<MaterialViewModel> _materials;

        // 模拟从数据库加载物料数据
        public async Task<List<MaterialViewModel>> GetMaterialsAsync()
        {
            // 在实际应用中，这里应该调用数据库或服务来获取数据
            await Task.Delay(100); // 模拟异步操作

            if (_materials == null)
            {
                _materials = MaterialViewModel.GenerateSampleMaterials();
            }

            return _materials;
        }

        // 新建或更新物料
        [HttpPost("SaveMaterial")]
        public IActionResult SaveMaterial([FromBody] MaterialViewModel material)
        {
            try
            {
                // 验证表单
                if (material.CurrentStock < 0 || material.SafetyStock < 0 || material.ReorderPoint < 0 || material.EconomicOrderQuantity < 0 || material.LeadTime < 0)
                {
                    return BadRequest(new { Success = false, Message = "所有库存相关参数不能为负数" });
                }

                if (material.CurrentStock < material.SafetyStock)
                {
                    return BadRequest(new { Success = false, Message = "当前库存量不能低于安全库存" });
                }

                if (_materials == null)
                {
                    _materials = new List<MaterialViewModel>();
                }

                if (string.IsNullOrEmpty(material.MaterialCode) || material.MaterialCode.StartsWith("NEW-MAT-"))
                {
                    // 新建物料
                    if (string.IsNullOrEmpty(material.MaterialCode))
                    {
                        material.MaterialCode = $"MAT-{DateTime.Now:yyyyMMdd}-{_materials.Count + 1:D4}";
                    }

                    _materials.Add(material);
                    return Ok(new { Success = true, Message = $"物料 {material.MaterialCode} 已创建" });
                }
                else
                {
                    // 更新现有物料
                    var originalMaterial = _materials.FirstOrDefault(m => m.MaterialCode == material.MaterialCode);
                    if (originalMaterial == null)
                    {
                        return NotFound(new { Success = false, Message = $"未找到编码为 {material.MaterialCode} 的物料" });
                    }

                    originalMaterial.MaterialName = material.MaterialName;
                    originalMaterial.CurrentStock = material.CurrentStock;
                    originalMaterial.SafetyStock = material.SafetyStock;
                    originalMaterial.AvailabilityStatus = material.AvailabilityStatus;
                    originalMaterial.ReorderPoint = material.ReorderPoint;
                    originalMaterial.EconomicOrderQuantity = material.EconomicOrderQuantity;
                    originalMaterial.LeadTime = material.LeadTime;

                    return Ok(new { Success = true, Message = $"物料 {material.MaterialCode} 已更新" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = $"保存物料失败: {ex.Message}" });
            }
        }

        // 删除物料
        [HttpDelete("{materialCode}")]
        public IActionResult DeleteMaterial(string materialCode)
        {
            try
            {
                if (_materials == null)
                {
                    _materials = new List<MaterialViewModel>();
                }

                var material = _materials.FirstOrDefault(m => m.MaterialCode == materialCode);
                if (material == null)
                {
                    return NotFound(new { Success = false, Message = $"未找到编码为 {materialCode} 的物料" });
                }

                _materials.Remove(material);
                return Ok(new { Success = true, Message = $"物料 {material.MaterialCode} 已删除" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = $"删除物料失败: {ex.Message}" });
            }
        }
    }
}