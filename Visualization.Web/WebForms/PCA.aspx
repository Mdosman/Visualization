<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCA.aspx.cs" Inherits="Webforms_PCA" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCA</title>
    <%--    <script type="text/javascript" src="../Scripts/numeric-1.2.6.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="File Path (.xls|.xlsx)"></asp:Label>
            <asp:TextBox ID="txtFilePath" runat="server" Width="765px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqFile" runat="server" ErrorMessage="Select the File Path." ControlToValidate="txtFilePath">*</asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Sheet/Table Name"></asp:Label>
            <asp:TextBox ID="txtSheetName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqFileSheet" runat="server" ErrorMessage="Select the Sheet Name." ControlToValidate="txtSheetName">*</asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"  ShowMessageBox="true"/>
            <br />
            <asp:GridView ID="gvData" runat="server" Caption="Original" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvStatisticCenter" runat="server" Caption="Centered Data" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvStatisticStandard" runat="server" Caption="Standardized Data" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvStatisticCovariance" runat="server" Caption="Covariance Matrix" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvStatisticCorrelation" runat="server" Caption="Correlation Matrix" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvDistributionMeasures" runat="server" Caption="Univariate Descriptive Statistics" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvFeatureVectors" runat="server" Caption="Eigenvectors Matrix" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <br />
            <asp:GridView ID="dgvProjectionComponents" runat="server" Caption="Principal Component Analysis" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            </asp:GridView>

        </div>
        <div id="pcaDiv"></div>
    </form>
    <%--    <script type="text/javascript">
        function pca(X) {
            /*
                Return matrix of all principle components as column vectors
            */
            var m = X.length;
            var sigma = numeric.div(numeric.dot(numeric.transpose(X), X), m);
            return numeric.svd(sigma).U;
        }

        function pcaReduce(U, k) {
            /*
                Return matrix of k first principle components as column vectors            
            */
            return U.map(function (row) {
                return row.slice(0, k)
            });
        }

        function pcaProject(X, Ureduce) {
            /*
                Project matrix X onto reduced principle components matrix
            */
            return numeric.dot(X, Ureduce);
        }

        function pcaRecover(Z, Ureduce) {
            /*
                Recover matrix from projection onto reduced principle components
            */
            return numeric.dot(Z, numeric.transpose(Ureduce));
        }

        window.onload = function () {
            var x, y, X = [];

            //var noise = function () { return Math.random() * 0.02 - 0.01 };

            //// Create random dataset with slope of 0.357 and noise
            //for (var i = 0; i < 1000; i++) {
            //    x = Math.random() * 2 - 1;
            //    y = x * 0.357;
            //    X.push([x + noise(), y + noise()]);
            //}


            //document.write(X);
            //document.write(U);
            // Print slope of first principle component
            //document.write(Math.abs(U[0][1] / U[0][0]).toFixed(3));

            var numCols = $("#gvData").find('tr')[0].cells.length;

            $('#gvData tr').each(function () {
                if (!this.rowIndex) return; // skip first row
                var cells = [];
                for (var i = 0; i < this.cells.length; i++) {
                    cells.push(this.cells[i].innerHTML);
                }

                X.push(cells);
            });

            // Get principle components
            var U = pca(X);

            var table = "<table><tr>";
            for (var j = 0; j < numCols; j++) {
                table += "<th width=\"150\">PCA" + (j + 1).toString() + "</th>";
            }

            for (var a = 0; a < numCols; a++) {
                table += "<tr>";
                for (var k = 0; k < numCols; k++) {
                    table += "<td align='center'>" + Math.abs(U[a][k]).toFixed(6) + "</td>";
                }
                table += "</tr>";
            }
            $("#pcaDiv").append(table + "</table>");

        };

    </script>--%>
</body>
</html>
