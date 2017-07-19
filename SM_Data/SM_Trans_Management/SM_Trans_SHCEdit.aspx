<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_SHCEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_SHCEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <title>散货船信息编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div align="center">
     <table class="edittable">
        <caption>散货船信息编辑</caption>
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
                <td colspan="2">发运形式</td>
                <td><asp:TextBox ID="TextBoxFYXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYXS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2">发运批次</td>
                <td><asp:TextBox ID="TextBoxFYPC" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYPCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYPC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">货物描述</td>
                <td><asp:TextBox ID="TextBoxHWMS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxHWMSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHWMS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td rowspan="3">货量</td>
                <td>件数</td>
                <td><asp:TextBox ID="TextBoxJS" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxJSRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxJS" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxJSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJS"></asp:RequiredFieldValidator>
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
                <td>毛重（T）</td>
                <td><asp:TextBox ID="TextBoxMZ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxMZRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxMZ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxMZRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMZ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">装运港</td>
                <td><asp:TextBox ID="TextBoxZYG" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxZYGRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZYG"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td rowspan="3">装运起始时间</td>
                <td>集港完毕</td>
                <td><asp:TextBox ID="TextBoxJGWB" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxJGWBCalendarExtender" runat="server" TargetControlID="TextBoxJGWB" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxJGWBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJGWB"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>装船</td>
                <td><asp:TextBox ID="TextBoxZC" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxZCCalendarExtenderTextBoxZC" runat="server" TargetControlID="TextBoxZC" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxZCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZC"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td>海运</td>
                <td><asp:TextBox ID="TextBoxHY" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxHYCalendarExtender" runat="server" TargetControlID="TextBoxHY" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxHYRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHY"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td colspan="2">船名</td>
                <td><asp:TextBox ID="TextBoxCM" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCM"></asp:RequiredFieldValidator>                
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
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" OnClick="Back_Click" CausesValidation="false" CssClass="button" />                    
                </td>
            </tr>
        </tfoot>
    </table>   
    </div>
    </form>
</body>
</html>
