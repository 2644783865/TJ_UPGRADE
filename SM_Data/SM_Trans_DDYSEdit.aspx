<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_DDYSEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_DDYSEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>倒短运输信息编辑</title>
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div align="center">
    <table class="edittable">
        <caption>倒短运输信息编辑</caption>
        <tbody>
            <tr>
                <td colspan="2">年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">日期</td>
                <td><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2">货物名称</td>
                <td><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td colspan="2">单位</td>
                <td><asp:TextBox ID="TextBoxUNIT" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxUNITRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxUNIT"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2">数量</td>
                <td><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td rowspan="2">结算金额（元）</td>
                <td>单价</td>
                <td><asp:TextBox ID="TextBoxJSDJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxJSDJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxJSDJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxJSDJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJSDJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td>总价</td>
                <td><asp:TextBox ID="TextBoxJSZJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxJSZJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxJSZJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxJSZJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJSZJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td rowspan="3">运输信息</td>
                <td>车号</td>
                <td><asp:TextBox ID="TextBoxCH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCH"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td>启运地</td>
                <td><asp:TextBox ID="TextBoxQYD" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxQYDRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxQYD"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td>目的地</td>
                <td><asp:TextBox ID="TextBoxMDD" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxMDDRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMDD"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td colspan="2">备注</td>
                <td><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
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
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click"  CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click"  CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" CausesValidation="false" OnClick="Back_Click"  CssClass="button" />                    
                </td>
            </tr>
        </tfoot>
    </table>
    </div>
    </form>
</body>
</html>
