<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Position_Edit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Position_Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%" >
        <tr>
            <td>编辑仓位信息(带<span class="red">*</span>号的为必填项)</td>
        </tr>
    </table>
    </div>
    </div>
    </div>
         
    <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
    <div class="box-wrapper">
    <div class="box-outer">
    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
            <td width="25%" height="25" align="right"> 仓位名称:</td>
            <td width="75%" class="category"><asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
            <asp:RequiredFieldValidator ID="TextBoxNameRequiredFieldValidator" ControlToValidate="TextBoxName" runat="server" ErrorMessage="仓库名称不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" height="25" align="right">所属仓库名称:</td>          
            <td width="75%" class="category"><asp:DropDownList ID="DropDownListParent" runat="server" AutoPostBack="False"></asp:DropDownList>&nbsp;<font color="#ff0000">*</font>
            <asp:RequiredFieldValidator ID="DropDownListParentRequiredFieldValidator" ControlToValidate="DropDownListParent" runat="server" ErrorMessage="所属仓库名不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" height="25" align="right">备注:</td>     
            <td width="75%" class="category"><asp:TextBox ID="TextBoxNote" Text="" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox>
            </td>
        </tr>
        <tr>  
            <td></td>
            <td align="left">
                <asp:Button ID="Confirm" runat="server" Text="确定" OnClick="Confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Cancel" runat="server" Text="取消" CausesValidation="False" OnClick="Cancel_Click" /></td>
        </tr>

    </table>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->

</asp:Content>
