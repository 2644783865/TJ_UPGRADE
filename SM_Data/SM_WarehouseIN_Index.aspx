<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseIN_Index.aspx.cs"
    MasterPageFile="~/Masters/SMBaseMaster.Master" Inherits="ZCZJ_DPF.SM_WarehouseIN_Index"
    Title="入库管理" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <br />
            <br />
            <table style="width: 80%; border: solid 1px black;">
                <tr>
                    <td colspan="2" style="font-size: x-large" align="center" height="60px">
                        请选择入库类型
                    </td>
                </tr>
                <tr valign="middle">
                    <td width="20%" height="60px" valign="middle">
                    </td>
                    <td width="80%" height="60px" valign="middle">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseIn_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">采购入库</asp:HyperLink>
                        &nbsp;
                    </td>
                </tr>
                <%--   <tr valign="middle"  >
                <td height="60px"  valign="middle" >
                </td>
                <td><asp:HyperLink ID="HyperLink2" runat="server"
                     NavigateUrl="~/SM_Data/SM_WarehouseIn_WX_Manage.aspx"  Target="_blank" Font-Underline="false" Font-Bold="True">外委入库</asp:HyperLink>
                    &nbsp;</td>
                    
            </tr>--%>
                <tr valign="middle">
                    <td height="60px" valign="middle">
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SM_Data/SM_YULIAO_IN.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">余料入库</asp:HyperLink>
                        &nbsp;
                    </td>
                </tr>
                <tr valign="middle">
                    <td height="60px" valign="middle">
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseIn_Other_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">其他入库</asp:HyperLink>
                        &nbsp;
                    </td>
                </tr>
                <tr valign="middle">
                    <td height="60px" valign="middle">
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/SM_Data/SM_Warehouse_TempInManage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">结转备库</asp:HyperLink>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
