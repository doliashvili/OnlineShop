using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnlineShop.UI.Enums;
using OnlineShop.UI.Models.Category;
using OnlineShop.UI.Models.Product;
using OnlineShop.UI.Models.Product.AdminProduct;

namespace OnlineShop.UI.Pages.Admin
{
    public partial class AddProduct : ComponentBase
    {
        private AddProductRequest model = new() { Weight = new Weight() { WeightType = WeightType.Gr } };//todo gasasworebelia

        private List<CategoryViewModel> categories;
        private string forBaby = null;
        private List<File> files = new();
        private List<UploadResult> uploadResults = new();
        private int maxAllowedFiles = 3;
        private bool shouldRender;
        private bool addMoreDetails;

        protected override bool ShouldRender() => shouldRender;

        private async Task OnInputFileChangeAsync(InputFileChangeEventArgs e)
        {
            long maxFileSize = 1024 * 1024 * 15;
            var upload = false;

            using var content = new MultipartFormDataContent();
            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                if (uploadResults.SingleOrDefault(
                    f => f.FileName == file.Name) is null)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    files.Add(
                        new()
                        {
                            Name = file.Name
                        });

                    if (file.Size < maxFileSize)
                    {
                        content.Add(
                            content: fileContent,
                            name: "\"files\"",
                            fileName: file.Name);

                        upload = true;
                    }
                    else
                    {
                        uploadResults.Add(
                            new()
                            {
                                FileName = file.Name,
                                ErrorCode = 6,
                                Uploaded = false
                            });
                    }
                }
            }

            if (upload)
            {
                var response = await _adminService.UploadImageAsync(content);

                var newUploadResults = await response.Content
                    .ReadFromJsonAsync<IList<UploadResult>>();

                uploadResults = uploadResults.Concat(newUploadResults).ToList();

                model.Images = new List<Image>(uploadResults.Count);
            }

            shouldRender = true;
        }

        private static bool FileUpload(IList<UploadResult> uploadResults,
            string fileName, out UploadResult result)
        {
            result = uploadResults.SingleOrDefault(f => f.FileName == fileName);

            if (result is null)
            {
                result = new();
                result.ErrorCode = 5;
            }

            return result.Uploaded;
        }

        private void ChangeMainImage(UploadResult current)
        {
            for (int i = 0; i < uploadResults.Count; i++)
            {
                if (uploadResults[i].StoredFileName != current.StoredFileName)
                {
                    uploadResults[i].MainImage = false;
                }
                else
                {
                    uploadResults[i].MainImage = true;
                }
            }
        }

        private class File
        {
            public string Name { get; set; }
        }

        public async Task SubmitAsync()
        {
            model.ForBaby = forBaby switch
            {
                "yes" => true,
                "no" => false,
                "null" => null,
                _ => model.ForBaby
            };

            if (model.Discount is not null)
            {
                model.Discount *= (float)0.01;
            }

            if (uploadResults.TrueForAll(x => !x.MainImage))
            {
                uploadResults[0].MainImage = true;
            }

            foreach (var uploadResult in uploadResults)
            {
                var image = new Image()
                {
                    MainImage = uploadResult.MainImage,
                    //todo ეს უნდა შეიცვალოს როცა პროდაქშენზე გავა უნდა მიეთითოს სწორი ფაილის მისამართი
                    Url = uploadResult.ImagePath,
                };
                model.Images.Add(image);
            }

            var result = await _adminService.AddProductAsync(model, new CancellationToken());
            if (result)
            {
                _toastService.ShowSuccess("პროდუქტი წარმატებით აიტვირთა");
                _navigationManager.NavigateTo("/products-add");
            }
            else
            {
                _toastService.ShowError("პროდუქტი სამწუხაროდ ვერ აიტვირთა");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            categories = await _categoryService.GetCategoriesAsync(CancellationToken.None);
            shouldRender = true;
        }
    }
}