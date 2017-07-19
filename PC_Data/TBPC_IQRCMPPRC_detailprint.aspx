<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBPC_IQRCMPPRC_detailprint.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.TBPC_IQRCMPPRC_detailprint" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="background-color: #C0C0C0">
        <div align="center" style="background-color: #FFFFFF; width: 1120px; height: 550px;">
        
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" Height="540px" Width="1060px">
                <LocalReport ReportPath="PC_Data\TBPC_IQRCMPPRC_detail.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                            Name="orderdataset_irqtbtotal" />
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                            Name="orderdataset_irqtbdetail" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        
        </div>
    </div>
    </form>
</body>
</html>
