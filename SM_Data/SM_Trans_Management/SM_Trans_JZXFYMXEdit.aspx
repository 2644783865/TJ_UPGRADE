<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_JZXFYMXEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_JZXFYMXEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
    <title>集装箱明细信息编辑</title>
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
    <td style="padding-top:3px; font-size:medium">集装箱明细信息编辑</td>
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
    <table class="tabGg" cellpadding="0" cellspacing="0">        
        <tbody>
            <tr>
                <td  class="r_bg">项目名称</td>
                <td class="right_bg"><asp:Label ID="LabelProject" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td  class="r_bg">发运批次</td>
                <td class="right_bg"><asp:Label ID="LabelFYPC" runat="server"></asp:Label></td>
            </tr>            
            <tr>
                <td  class="r_bg">发运日期</td>
                <td class="right_bg"><asp:Label ID="LabelTransDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td  class="r_bg">货物名称</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td  class="r_bg">生产制号</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxSCZH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxSCZHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxSCZH"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  class="r_bg">货物重量</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxHWZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxHWZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxHWZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxHWZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHWZL"></asp:RequiredFieldValidator>
                </td>
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
                    <asp:Button ID="Back" runat="server" Text="放弃返回" OnClick="Back_Click" CausesValidation="false" CssClass="buttOver" />                    
                </td>
            </tr>
        </tfoot>
    </table>        
    </div></div>
    </div>
    </form>
</body>
</html>
