<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_Bgyp_Real.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.master"
    Inherits="ZCZJ_DPF.OM_Data.OM_Bgyp_Real" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    修改基础定额
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript"></script>

    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                
                    <td align="left">
                    <asp:Button ID="change_month_max" runat="Server" OnClick="change_month_max_onclick" Text="修改"/>
                    <asp:Button ID="save_month_max" runat="Server" OnClick="save_month_max_onclick" Visible="false" Text="保存"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table id="tab" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1" width="100%">
                <tr>
                    <td>
                        综合管理部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_02" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                      
                    </td>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        技术部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_03" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                      
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        生产管理部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_04" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                       
                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        采购部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_05" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                       
                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        财务部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_06" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                       
                    </td>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        市场部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_07" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                       
                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        设备安全管理部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_10" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                      
                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        工程师办公室:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_11" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                        
                    </td>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        质量部:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_12" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                      
                    </td>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        合计:
                    </td>
                    <td>
                        <asp:TextBox ID="MONTH_MAX_ALL" runat="server" Width="100px"></asp:TextBox>(￥)
                    </td>
                    <td>
                      
                    </td>
                    <td>
                      
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
