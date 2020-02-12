using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class ItemViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Item Number")]
        public string ItemNumber { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }
        [Required]
        [Display(Name = "Reorder Level")]
        public int ItemReorderLevel { get; set; }
        [Required]
        [Display(Name = "Reorder Quantity")]
        public int ItemReorderQty { get; set; }
        [Required]
        [Display(Name = "Unit of Measure")]
        public string ItemUnit { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string ItemCategoryId { get; set; }
        public IEnumerable<SelectListItem> ItemCategories { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        [Display(Name = "Choose the image")]
        public string ImagePath { get; set; }
        public IEnumerable<ItemSupplier> SuppliersByItem {get; set;}
        public int InventoryQuantity { get; set; }

    }
}