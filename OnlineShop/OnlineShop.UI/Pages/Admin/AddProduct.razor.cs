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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        private string forBaby = null;
        private List<File> files = new();
        private List<UploadResult> uploadResults = new();
        private int maxAllowedFiles = 3;
        private bool shouldRender;
        private bool addMoreDetails;

        protected override bool ShouldRender() => shouldRender;

        private async Task OnInputFileChangeAsync(InputFileChangeEventArgs e)
        {
            shouldRender = false;
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
            foreach (var uploadResult in uploadResults)
            {
                if (uploadResult.StoredFileName != current.StoredFileName)
                {
                    uploadResult.MainImage = false;
                }
                else
                {
                    uploadResult.MainImage = true;
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

            var result = await _adminService.AddProductAsync(model, new CancellationToken());
        }

        protected override async Task OnInitializedAsync()
        {
            categories = await _categoryService.GetCategoriesAsync(CancellationToken.None);
        }

        public class UploadResult
        {
            public string FileName { get; set; }
            public int ErrorCode { get; set; }
            public bool Uploaded { get; set; }
            public string StoredFileName { get; set; }
            public string ImageUrl { get; set; }
            public bool MainImage { get; set; }

            //Todo დროებითია სერვერზე რო აიწევა მარტო imageUrl წავა
            public string ImagePath => "http://127.0.0.1:8887/images" + "/" + StoredFileName;
        }
    }
}