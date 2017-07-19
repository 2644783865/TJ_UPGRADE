<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_KYEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_KYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>空运信息编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
    <div align="center">
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title"> 
    <table width="100%">
    <tr>
    <td style="padding-top:3px; font-size:medium">空运信息编辑</td>
    <td width="100px" align="right" valign="middle">
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Back_Click" CausesValidation="false">
      <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/Assets/images/goback.gif" runat="server" Width="18px" Height="18px"/> 
        返回上一级</asp:LinkButton>&nbsp;
    </td>
    </tr></table>
    </div>
    </div>
    </div>
    <div class="box-wrapper">
    <div class="box-outer">
    <table  class="tabGg" cellpadding="0" cellspacing="0">        
        <tbody>
            <tr>
                <td  class="r_bg">年度</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">项目名称</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">货物名称</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">件数</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">包装形式</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxBZXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxBZXS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">体积（m3）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxTJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">重量（KG）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">运费（元）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYF"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">运输公司</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYSGS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYSGSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSGS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">发运日期</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td class="r_bg">发运人</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxFYR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYRRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYR"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td class="r_bg">备注</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="r_bg">运费结算情况</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYFJSQK" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>                                     
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" class="right_bg">
                    编辑时间：<asp:Label ID="LabelDate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    编辑人：<asp:Label ID="LabelClerk" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center" class="right_bg">
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" CausesValidation="false" OnClick="Back_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;                    
                </td>
            </tr>
        </tfoot>
    </table>    
    </div></div>
    </div>
    </form>
</body>
</html>
