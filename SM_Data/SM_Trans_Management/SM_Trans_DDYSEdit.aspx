<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_DDYSEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_DDYSEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>����������Ϣ�༭</title>
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
    <td><h4 style="padding-top:8px">����������Ϣ�༭</h4></td>
    <td width="100px" align="right" valign="middle">
    <h5>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Back_Click" CausesValidation="false">
      <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/Assets/images/goback.gif" runat="server" Width="18px" Height="18px"/> 
        ������һ��</asp:LinkButton>&nbsp;</h5>
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
                <td colspan="2" class="r_bg">���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy��MM��dd��">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="r_bg">��λ</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxUNIT" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxUNITRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxUNIT"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td rowspan="2" class="r_bg" style="text-align:center">�����Ԫ��</td>
                <td class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxJSDJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxJSDJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxJSDJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxJSDJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJSDJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td class="r_bg">�ܼ�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxJSZJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxJSZJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxJSZJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxJSZJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxJSZJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td rowspan="3" class="r_bg" style="text-align:center">������Ϣ</td>
                <td class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxCH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCH"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td class="r_bg">���˵�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxQYD" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxQYDRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxQYD"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td class="r_bg">Ŀ�ĵ�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxMDD" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxMDDRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMDD"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">��ע</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>                                    
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" class="right_bg">
                    �༭ʱ�䣺<asp:Label ID="LabelDate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    �༭�ˣ�<asp:Label ID="LabelClerk" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center" class="right_bg">
                    <asp:Button ID="Continue" runat="server" Text="�������" OnClick="Continue_Click"  CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="ȷ������" OnClick="Confirm_Click"  CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="��������" CausesValidation="false" OnClick="Back_Click"  CssClass="buttOver" />                    
                </td>
            </tr>
        </tfoot>
    </table>
    </div></div>
    </div>
    </form>
</body>
</html>
