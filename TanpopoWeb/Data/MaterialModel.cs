using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace TanpopoWeb.Data
{
    public enum MaterialAvailabilityStatus
    {
        [Display(Name = "可用")]
        Available,

        [Display(Name = "在途")]
        InTransit,

        [Display(Name = "在检")]
        InInspection,

        [Display(Name = "锁定")]
        Locked
    }

    public class MaterialViewModel : ICloneable
    {
        public string MaterialCode { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int SafetyStock { get; set; }
        public MaterialAvailabilityStatus AvailabilityStatus { get; set; }
        public int ReorderPoint { get; set; }
        public int EconomicOrderQuantity { get; set; }
        public int LeadTime { get; set; }

        public object Clone()
        {
            return new MaterialViewModel
            {
                MaterialCode = MaterialCode,
                MaterialName = MaterialName,
                CurrentStock = CurrentStock,
                SafetyStock = SafetyStock,
                AvailabilityStatus = AvailabilityStatus,
                ReorderPoint = ReorderPoint,
                EconomicOrderQuantity = EconomicOrderQuantity,
                LeadTime = LeadTime
            };
        }

        public static List<MaterialViewModel> GenerateSampleMaterials()
        {
            return new List<MaterialViewModel>
            {
                new MaterialViewModel
                {
                    MaterialCode = "MAT-1001",
                    MaterialName = "高性能电机",
                    CurrentStock = 120,
                    SafetyStock = 50,
                    AvailabilityStatus = MaterialAvailabilityStatus.Available,
                    ReorderPoint = 30,
                    EconomicOrderQuantity = 100,
                    LeadTime = 7
                },
                new MaterialViewModel
                {
                    MaterialCode = "MAT-1002",
                    MaterialName = "定制控制器",
                    CurrentStock = 30,
                    SafetyStock = 20,
                    AvailabilityStatus = MaterialAvailabilityStatus.InInspection,
                    ReorderPoint = 25,
                    EconomicOrderQuantity = 80,
                    LeadTime = 10
                },
                new MaterialViewModel
                {
                    MaterialCode = "MAT-1003",
                    MaterialName = "铝制外壳",
                    CurrentStock = 150,
                    SafetyStock = 40,
                    AvailabilityStatus = MaterialAvailabilityStatus.Locked,
                    ReorderPoint = 35,
                    EconomicOrderQuantity = 120,
                    LeadTime = 5
                }
            };
        }
    }
}