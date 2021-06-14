using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using OnlineShop.UI.Models.Category;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Models.Product.AdminProduct;
using OnlineShop.UI.Services.Abstract;

namespace OnlineShop.UI.Pages.Admin
{
    public partial class AddProduct
    {
        private AddProductRequest model = new() { Weight = new Weight() };

        private List<CategoryViewModel> categories = new(10);
        private string Message = "No file(s) selected";
        private IReadOnlyList<IBrowserFile> selectedFiles;
        private string forBaby = null;

        public async Task SubmitAsync()
        {
            foreach (var file in selectedFiles)
            {
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                UploadedFile uploadedFile = new UploadedFile();
                uploadedFile.FileName = file.Name;
                uploadedFile.FileContent = ms.ToArray();
                ms.Close();

                var resultUpload = await _adminService.UploadImageAsync(uploadedFile);
                var image = new Image() { Url = resultUpload.Data };

                model.Images.Add(image);
            }

            model.ForBaby = forBaby switch
            {
                "yes" => true,
                "no" => false,
                "null" => null,
                _ => model.ForBaby
            };

            Message = $"{selectedFiles.Count} file(s) uploaded on server";
            StateHasChanged();

            var result = await _adminService.AddProductAsync(model, new CancellationToken());
        }

        private void UploadImage(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            if (selectedFiles.Count > 0)
            {
                model.Images = new List<Image>();
            }
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            categories = await _categoryService.GetCategoriesAsync(CancellationToken.None);
        }
    }
}