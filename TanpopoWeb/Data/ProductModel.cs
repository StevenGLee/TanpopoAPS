using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using static TanpopoWeb.Pages.MaterialInventory;
using System;

namespace TanpopoWeb.Data
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class ProductViewModel : ICloneable
    {
        public int Id { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specification { get; set; }

        public string Version { get; set; }

        public string ProductType { get; set; }

        public int MinProductionQuantity { get; set; }

        public int MaxProductionQuantity { get; set; }

        public int ProductionLeadTime { get; set; }

        public bool IsActive { get; set; } = true;

        // 新增的物料字段
        public List<MaterialViewModel> Materials { get; set; }

        public object Clone()
        {
            return new ProductViewModel
            {
                Id = Id,
                ProductCode = ProductCode,
                Name = Name,
                Specification = Specification,
                Version = Version,
                ProductType = ProductType,
                MinProductionQuantity = MinProductionQuantity,
                MaxProductionQuantity = MaxProductionQuantity,
                ProductionLeadTime = ProductionLeadTime,
                IsActive = IsActive,
                Materials = Materials?.ConvertAll(m => (MaterialViewModel)m.Clone())
            };
        }

        public static List<ProductViewModel> GenerateSampleProducts()
        {
            return new List<ProductViewModel>
            {
                new ProductViewModel
                {
                    Id = 1,
                    ProductCode = "PRD-0001",
                    Name = "高性能电机",
                    Specification = "12V, 2A, 3000RPM",
                    Version = "V2.1",
                    ProductType = "Standard",
                    MinProductionQuantity = 100,
                    MaxProductionQuantity = 10000,
                    ProductionLeadTime = 15,
                    IsActive = true
                },
                new ProductViewModel
                {
                    Id = 2,
                    ProductCode = "PRD-0002",
                    Name = "定制控制器",
                    Specification = "输入电压: 24V, 输出电流: 5A",
                    Version = "V1.0",
                    ProductType = "Custom",
                    MinProductionQuantity = 50,
                    MaxProductionQuantity = 500,
                    ProductionLeadTime = 30,
                    IsActive = true
                },
                new ProductViewModel
                {
                    Id = 3,
                    ProductCode = "PRD-0003",
                    Name = "铝制外壳",
                    Specification = "尺寸: 100x50x20mm, 材质: 6061铝",
                    Version = "V1.2",
                    ProductType = "SemiFinished",
                    MinProductionQuantity = 200,
                    MaxProductionQuantity = 5000,
                    ProductionLeadTime = 7,
                    IsActive = true
                },
                new ProductViewModel
                {
                    Id = 4,
                    ProductCode = "PRD-0004",
                    Name = "润滑脂",
                    Specification = "耐温: -40°C至150°C, 包装: 1kg/桶",
                    Version = "V1.0",
                    ProductType = "RawMaterial",
                    MinProductionQuantity = 1000,
                    MaxProductionQuantity = 10000,
                    ProductionLeadTime = 5,
                    IsActive = true
                }
            };
        }
    }
}