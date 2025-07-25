﻿using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Site_Lanchonete.Areas.Admin.FastReportUtils;
using Site_Lanchonete.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Site_Lanchonete.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminLanchesReportController : Controller
{
    private readonly IWebHostEnvironment _webHostEnv;
    private readonly RelatorioLanchesService _relatorioLanchesService;

    public AdminLanchesReportController(IWebHostEnvironment webHostEnv,
        RelatorioLanchesService relatorioLanchesService)
    {
        _webHostEnv = webHostEnv;
        _relatorioLanchesService = relatorioLanchesService;
    }


    public async Task<ActionResult> LanchesCategoriaReport()
    {
        var webReport = new WebReport();
        var mssqlDataConnection = new MsSqlDataConnection();

        webReport.Report.Dictionary.AddChild(mssqlDataConnection);

        webReport.Report.Load(Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/reports",
                                           "lanchesCategoria.frx"));

        var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
        var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriasReport(), "CategoriasReport");

        webReport.Report.RegisterData(lanches, "LancheReport");
        webReport.Report.RegisterData(categorias, "CategoriasReport");
        return View(webReport);
    }

    [Route("LanchesCategoriaPDF")]
    public async Task<ActionResult> LanchesCategoriaPDF()
    {
        var webReport = new WebReport();
        var mssqlDataConnection = new MsSqlDataConnection();

        webReport.Report.Dictionary.AddChild(mssqlDataConnection);

        webReport.Report.Load(Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/reports",
                                           "lanchesCategoria.frx"));

        var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
        var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriasReport(), "CategoriasReport");

        webReport.Report.RegisterData(lanches, "LancheReport");
        webReport.Report.RegisterData(categorias, "CategoriasReport");

        webReport.Report.Prepare();

        Stream stream = new MemoryStream();

        webReport.Report.Export(new PDFSimpleExport(), stream);
        stream.Position = 0;

        //return File(stream, "application/zip", "LancheCategoria.pdf");
        return new FileStreamResult(stream, "application/pdf");
    }
}
