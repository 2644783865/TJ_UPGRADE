<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GJJBLEDIT.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GJJBLEDIT" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>公积金比例修改</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer" style="text-align: center">
            <table width="80%">
                <tr>
                    <td style="width: 20%">
                        
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_OnClick" />
                    </td>
                </tr>
            </table>
            <table id="Table1" border="1" cellspacing="0" cellpadding="0" runat="server">
                <tr style="width:100%">
                    <td style="width:100px">
                        单位
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="GJJ_DWBL" BorderStyle="None" runat="server"></asp:TextBox>%
                    </td>
                    <td style="width:100px">
                        个人
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="GJJ_GRBL" runat="server" BorderStyle="None"></asp:TextBox>%
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
