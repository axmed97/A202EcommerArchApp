using Entities.DTOs.ProductDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace WebUI.ViewModels
{
    public class HomeVM
    {
        public List<CategoryFeaturedDTO> CategoryFeatureds { get; set; }
        public List<ProductFeaturedDTO> ProductFeatureds { get; set; }
    }
}
