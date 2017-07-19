<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" 
AutoEventWireup="true" CodeBehind="QX_Power_Detail.aspx.cs" Inherits="FSGD_PMS.Systems.QX_Power_Detail" %>


<asp:Content ID="Content1"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    添加权限</asp:Content> 
    
<asp:Content ID="Content2"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <%--  <div class="RightContentTop"><asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle">
        添加权限</div></div>
    <div class=RightContent>--%>
         <div class="box-inner"><div class="box_right"><div class=box-title><table width=100% ><tr><td> 
         添加权限(带<span class="red">*</span>号的为必填项)</td></tr></table></div></div></div>
         <div class="box-wrapper">
            <div class="box-outer">

        <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
                <td class="tableTitle" valign="top">页面地址：</td>
                <td>
                    <asp:TextBox ID="TBPage" Columns="50" runat="server"></asp:TextBox>
                </td>
                <td class="tableTitle">控件数：</td>
                <td>
                    <asp:Label ID="LBUpdateCtrlNum" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="TBControlNum" runat="server" AutoPostBack="true" OnTextChanged="TBControlNum_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tableTitle" valign="top">名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="TBPageName" runat="server"></asp:TextBox>
                </td>
            </tr>  
            <tr>
                <td class="tableTitle" valign="top">
                    控件ID：<br />
                    <asp:Button ID="BtnCtrlAdd" runat="server" Text="添加控件" 
                        onclick="BtnCtrlAdd_Click" />
                </td>
                <td>
                    <asp:Table ID="TCtrlID" runat="server">
                    </asp:Table>
                </td>
                <td class="tableTitle">控件名称：</td>
                <td>
                    <asp:Table ID="TCtrlName" runat="server">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td class="tableTitle" valign="top">所属类别：</td>
                <td colspan="3">
                    <asp:DropDownList ID="DDLCategory" runat="server">
                        
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="Confirm" runat="server" Text="   确定   " 
                        onclick="Confirm_Click"  />
                    <input id="Reset1" type="reset" value="   取消   " />
                </td>
            </tr> 
        </table>
           
     </div><!--box-outer END -->
   </div> <!--box-wrapper END -->

    
    
    
    
    
</asp:Content>