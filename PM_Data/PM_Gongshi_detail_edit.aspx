<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PopupBase.Master"
    CodeBehind="PM_Gongshi_detail_edit.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Gongshi_detail_edit" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    &nbsp;&nbsp;加工工时明细修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <table width="100%">
                <tr>
                    <td width="25%">
                        <strong>顾客名称：</strong><asp:Label ID="lbCUSNAME" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        <strong>合同号：</strong><asp:Label ID="lbCONTR" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        <strong>任务单号：</strong><asp:Label ID="lbTSAID" runat="server"></asp:Label>
                    </td>
                    <td width="25%">
                        <strong>时间：</strong><asp:Label ID="lbYEAR" runat="server"></asp:Label>.
                        <asp:Label ID="lbMONTH" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" class="toptable grid" border="true">
                <tr>
                    <td align="right" width="45%" height="30px">
                        图号：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtTUHAO" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        图名：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtTUMING" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        加工设备号：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtEQUID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        加工设备名称：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtEQUNAME" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        设备系数：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtEQUFACTOR" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        加工工时：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtEQUHOUR" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        工时费用（元）：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtEQUMONEY" runat="server"></asp:TextBox>
                        <span class="red">&nbsp;自动计算：工时费用 = 设备系数 * 加工工时</span>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="45%" height="30px">
                        备注：
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:TextBox ID="txtNOTE" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" height="30px">
                        <asp:Button ID="btnupdate" runat="server" Text="修改" OnClick="btnupdate_Click" />
                        &nbsp;&nbsp;
                        <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../JS/jquery/jquery-3.2.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" charset="GB2312" language="javascript">
        $('#<%=txtEQUHOUR.ClientID %>').on('input propertychange', function() {
            $('#<%=txtEQUMONEY.ClientID %>').val($('#<%=txtEQUFACTOR.ClientID %>').val() * $('#<%=txtEQUHOUR.ClientID %>').val());
        });
        $('#<%=txtEQUFACTOR.ClientID %>').on('input propertychange', function() {
            $('#<%=txtEQUMONEY.ClientID %>').val($('#<%=txtEQUFACTOR.ClientID %>').val() * $('#<%=txtEQUHOUR.ClientID %>').val());
        });
        $('#<%=txtEQUMONEY.ClientID %>').on('input propertychange', function() {
            $('#<%=txtEQUMONEY.ClientID %>').val($('#<%=txtEQUFACTOR.ClientID %>').val() * $('#<%=txtEQUHOUR.ClientID %>').val());
        });
    </script>

</asp:Content>
