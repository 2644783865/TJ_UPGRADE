<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_KYEdit.aspx.cs" MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_KYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">空运信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager> 
    <div align="center">
    <table  class="edittable">
        <caption>空运信息编辑</caption>
        <tbody>
            <tr>
                <td>年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>项目名称</td>
                <td><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>货物名称</td>
                <td><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>件数</td>
                <td><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>包装形式</td>
                <td><asp:TextBox ID="TextBoxBZXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxBZXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxBZXS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>体积（m3）</td>
                <td><asp:TextBox ID="TextBoxTJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>重量（KG）</td>
                <td><asp:TextBox ID="TextBoxZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>运费（元）</td>
                <td><asp:TextBox ID="TextBoxYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYF"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>运输公司</td>
                <td><asp:TextBox ID="TextBoxYSGS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYSGSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSGS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>发运日期</td>
                <td><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td>发运人</td>
                <td><asp:TextBox ID="TextBoxFYR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYRRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYR"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td>备注</td>
                <td><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>运费结算情况</td>
                <td><asp:TextBox ID="TextBoxYFJSQK" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
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
                    <asp:Button ID="Back" runat="server" Text="放弃返回" CausesValidation="false" OnClick="Back_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;                    
                </td>
            </tr>
        </tfoot>
    </table>    
    </div>
    </asp:Content>
  
