<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOut_Index.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOut_Index"
    Title="出库管理" %>

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
                <tr height="60px">
                    <td colspan="2" style="font-size: x-large" align="center">
                        请选择出库类型
                    </td>
                </tr>
                <tr valign="middle">
                    <td width="20%" height="60px">
                        &nbsp;
                    </td>
                    <td width="80%" valign="middle">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseOUT_LL_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">生产领料单</asp:HyperLink>
                        &nbsp;
                    </td>
                </tr>
                <%--     <tr>
                    <td height="60px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseOUT_WW_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">委外加工</asp:HyperLink>
                    </td>
                </tr>--%>
                
                 <tr>
                    <td height="60px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SM_Data/SM_YULIAO_OUT.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">余料出库</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td height="60px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseOUT_XS_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">销售出库</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td height="60px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/SM_Data/SM_WarehouseOUT_QT_Manage.aspx"
                            Target="_blank" Font-Underline="false" Font-Bold="True">其他出库</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
