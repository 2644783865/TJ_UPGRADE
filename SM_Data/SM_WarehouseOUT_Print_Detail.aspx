﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseOUT_Print_Detail.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_Print_Detail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出库明细打印单</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 800px; height: 100%; overflow: hidden; margin: 0px auto 0px auto">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
            Height="500px" Width="100%">
            <LocalReport ReportPath="SM_Data\Report_Out_Detail.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
