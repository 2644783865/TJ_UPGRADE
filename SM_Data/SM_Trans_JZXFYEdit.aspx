<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_JZXFYEdit.aspx.cs"  MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_JZXFYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">集装箱发运信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
   
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div align="center">
    <table  class="edittable">
        <caption>集装箱发运信息编辑</caption>
        <tbody>
            <tr>
                <td colspan="2">年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">项目名称</td>
                <td><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">发运批次</td>
                <td><asp:TextBox ID="TextBoxFYPC" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYPCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYPC"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2">发运日期</td>
                <td><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2">货物描述</td>
                <td><asp:TextBox ID="TextBoxHWMS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxHWMSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHWMS"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td rowspan="3">装货量</td>
                <td>箱数</td>
                <td><asp:TextBox ID="TextBoxXS" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxXSRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxXS" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxXS"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>立方米</td>
                <td><asp:TextBox ID="TextBoxLFM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLFMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLFM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLFMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLFM"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>货重（T）</td>
                <td><asp:TextBox ID="TextBoxHZ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxHZRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxHZ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxHZRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHZ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">容重比</td>
                <td><asp:TextBox ID="TextBoxRZB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxRZBRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxRZB" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxRZBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxRZB"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2">体积装箱率</td>
                <td><asp:TextBox ID="TextBoxTJZXL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJZXLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJZXL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJZXLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJZXL"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">重量装箱率</td>
                <td><asp:TextBox ID="TextBoxZLZXL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLZXLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZLZXL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLZXLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZLZXL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2">箱型及箱数</td>
                <td><asp:TextBox ID="TextBoxXXJXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxXXJXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxXXJXS"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">装箱所用材料</td>
                <td><asp:TextBox ID="TextBoxZXSYCL" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxZXSYCLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZXSYCL"></asp:RequiredFieldValidator>
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
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click" CssClass="button"  />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click" CssClass="button"  />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" OnClick="Back_Click" CausesValidation="false" CssClass="button" />                    
                </td>
            </tr>
        </tfoot>
    </table>        
    </div>
    </asp:Content>
