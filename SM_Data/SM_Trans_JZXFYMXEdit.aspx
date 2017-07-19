<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_JZXFYMXEdit.aspx.cs"  MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_JZXFYMXEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">集装箱明细信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
 

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div align="center">
    <table class="edittable">
        <caption>集装箱明细信息编辑</caption>
        <tbody>
            <tr>
                <td>项目名称</td>
                <td><asp:Label ID="LabelProject" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>发运批次</td>
                <td><asp:Label ID="LabelFYPC" runat="server"></asp:Label></td>
            </tr>            
            <tr>
                <td>发运日期</td>
                <td><asp:Label ID="LabelTransDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>货物名称</td>
                <td><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>生产制号</td>
                <td><asp:TextBox ID="TextBoxSCZH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxSCZHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxSCZH"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>货物重量</td>
                <td><asp:TextBox ID="TextBoxHWZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxHWZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxHWZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxHWZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHWZL"></asp:RequiredFieldValidator>
                </td>
            </tr>                                  
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3">
                    编辑时间：<asp:Label ID="LabelDate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    编辑人：<asp:Label ID="LabelClerk" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" OnClick="Back_Click" CausesValidation="false" CssClass="button" />                    
                </td>
            </tr>
        </tfoot>
    </table>        
    </div>
  </asp:Content>
