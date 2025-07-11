using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site_Lanchonete.Models;

namespace Site_Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public AdminImagensController(IOptions<ConfigurationImagens> myConfig, IWebHostEnvironment hostingEnviroment)
        {
            _myConfig = myConfig.Value;
            _hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não slecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
            }
            try
            {
                long size = files.Sum(f => f.Length);

                var filePathsName = new List<string>();

                var filePath = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos);

                foreach (var formFile in files)
                {
                    if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains(".png"))
                    {
                        var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                        filePathsName.Add(fileNameWithPath);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidos, com tamanho total de: {size} bytes";

                ViewBag.Arquivos = filePathsName;
                return View(ViewData);
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Errl : {ex.Message}";
                return View(ViewData);
            }
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            try
            {
                var userImagesPath = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos);

                DirectoryInfo dir = new DirectoryInfo(userImagesPath);

                FileInfo[] files = dir.GetFiles();

                model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

                if (files.Length == 0)
                {
                    ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
                }

                model.Files = files;
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro : {ex.Message}";
            }
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            try
            {
                string _imagemDeleta = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos + "\\", fname);

                if ((System.IO.File.Exists(_imagemDeleta)))
                {
                    System.IO.File.Delete(_imagemDeleta);

                    ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
                }
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro : {ex.Message}";
            }
            return View("index");
        }
    }
}
