<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="WL_Type_Edit.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.WL_Type_Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%" >
        <tr>
            <td>编辑分类信息(带<span class="red">*</span>号的为必填项)</td>
        </tr>
    </table>
    </div>
    </div>
    </div>
         
    <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>
    <div class="box-wrapper">
    <div class="box-outer">
    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
            <td width="25%" height="25" align="right"> 类别名称:</td>
            <td width="75%" class="category"><asp:TextBox ID="t_name" runat="server"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
            <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" ControlToValidate="t_name" runat="server" ErrorMessage="类别名称不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" height="25" align="right">大类名称:</td>          
            <td width="75%" class="category"><asp:DropDownList ID="t_parentname" runat="server" AutoPostBack="False"></asp:DropDownList>&nbsp;<font color="#ff0000">*</font>
            <asp:RequiredFieldValidator ID="parentnameRequiredFieldValidator" ControlToValidate="t_parentname" runat="server" ErrorMessage="父类名字不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" height="25" align="right">物料状态:</td>   
            <td width="75%" class="category">
            <asp:RadioButtonList ID="t_status" runat="server" RepeatColumns="2">
            <asp:ListItem Text="停用" Value="0"></asp:ListItem>
            <asp:ListItem Text="在用" Value="1" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td width="25%" height="25" align="right">备注:</td>     
            <td width="75%" class="category"><asp:TextBox ID="t_comment" Text="" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox>
            </td>
        </tr>
        <tr>  
            <td></td>
            <td align="left">
                <asp:Button ID="btnConfirm" runat="server" Text="确定" OnClick="btnConfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="False" OnClick="btnCancel_Click" /></td>
        </tr>
          <%--  <asp:Label ID="idLabel"  Visible="false" runat="server"></asp:Label>--%>
    </table>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->
         
</asp:Content>
