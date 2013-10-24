using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accord.Statistics.Analysis;
using Accord.Statistics.Formats;
using Accord.Math;
using Accord.Controls;

public partial class Webforms_PCA : System.Web.UI.Page
{
    // Colors used in the pie graphics
    private readonly Color[] colors = { Color.YellowGreen, Color.DarkOliveGreen, Color.DarkKhaki, Color.Olive,
            Color.Honeydew, Color.PaleGoldenrod, Color.Indigo, Color.Olive, Color.SeaGreen };


    private PrincipalComponentAnalysis pca;
    private DescriptiveAnalysis sda;

    string[] sourceColumnNames;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        ProcessPCA();
    }

    private void ProcessPCA()
    {
        string fileName = txtFilePath.Text.Trim();//Server.MapPath("~/Resources/Acoustic_1A_Num.xlsx");
        ExcelReader db = new ExcelReader(fileName, true, false);
        DataTable dt;
        if (string.IsNullOrEmpty(txtSheetName.Text))
        {
            string[] lstSheet = db.GetWorksheetList();
            dt = db.GetWorksheet(lstSheet[0]);
        }
        else
        {
            dt = db.GetWorksheet(txtSheetName.Text);
        }
        gvData.DataSource = dt;
        gvData.DataBind();

        // Creates a matrix from the source data table
        double[,] sourceMatrix = dt.ToMatrix(out sourceColumnNames);

        // Creates the Simple Descriptive Analysis of the given source
        sda = new DescriptiveAnalysis(sourceMatrix, sourceColumnNames);

        sda.Compute();

        // Populates statistics overview tab with analysis data
        dgvStatisticCenter.DataSource = new ArrayDataView(sda.DeviationScores, sourceColumnNames);
        dgvStatisticCenter.DataBind();
        dgvStatisticStandard.DataSource = new ArrayDataView(sda.StandardScores, sourceColumnNames);
        dgvStatisticStandard.DataBind();
        dgvStatisticCovariance.DataSource = new ArrayDataView(sda.CovarianceMatrix, sourceColumnNames);
        dgvStatisticCovariance.DataBind();
        dgvStatisticCorrelation.DataSource = new ArrayDataView(sda.CorrelationMatrix, sourceColumnNames);
        dgvStatisticCorrelation.DataBind();
        dgvDistributionMeasures.DataSource = sda.Measures;
        dgvDistributionMeasures.DataBind();

        // Creates the Principal Component Analysis of the given source
        pca = new PrincipalComponentAnalysis(sda.Source, AnalysisMethod.Standardize);


        // Compute the Principal Component Analysis
        pca.Compute();

        // Populates components overview with analysis data
        dgvFeatureVectors.DataSource = new ArrayDataView(pca.ComponentMatrix);
        dgvFeatureVectors.DataBind();


        dgvProjectionComponents.DataSource = pca.Components;
        dgvProjectionComponents.DataBind();
    }
}