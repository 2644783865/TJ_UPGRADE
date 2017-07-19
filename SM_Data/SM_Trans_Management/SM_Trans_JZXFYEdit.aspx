<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_JZXFYEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_JZXFYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
    <title>��װ�䷢����Ϣ�༭</title>
    <script  type="text/javascript" language="javascript">
  
  function DefaultTextOnFocus(obj)
  {
     if(obj.value=="��ʽ��2000��")
     {
        obj.value="";
        obj.style.color="Black";
     }
     else
     {
       obj.style.color="Black";
     }
  }
  
  function DefaultTextOnBlur(obj)
  {
     if(obj.value=="")
     {
        obj.value="��ʽ��2000��";
        obj.style.color="Gray";
     }
     else
     {
        var num=obj.value;
        var patten=/^[0-9][0-9][0-9][0-9]['��']$/;
        if(!patten.test(num))
        { alert('��������ȷ��ʽ������\r\r�磺2000��');
        obj.value="��ʽ��2000��";
        obj.focus();
        }
     }
  }
</script>
</head>
<body>

   <script language="javascript" type="text/javascript">
 function auto_RZB()    {        if(document.getElementById('<%=TextBoxLFM.ClientID%>').value!=null)      {             if(document.getElementById('<%=TextBoxHZ.ClientID%>').value!=null&document.getElementById('<%=TextBoxHZ.ClientID%>').value!=0)        {          document.getElementById('<%=TextBoxRZB.ClientID%>').value=(document.getElementById('<%=TextBoxLFM.ClientID%>').value/document.getElementById('<%=TextBoxHZ.ClientID%>').value).toFixed(2);                 }      }    }
</script>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div align="center">
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">  
    <table width="100%">
    <tr>
    <td style="padding-top:3px; font-size:medium">��װ�䷢����Ϣ�༭</td>
    <td width="120px" align="right" valign="middle">    
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Back_Click" CausesValidation="false">
      <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/Assets/images/goback.gif" runat="server" Width="18px" Height="18px"/> 
        ������һ��&nbsp;</asp:LinkButton>
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
                <td colspan="2" class="r_bg">���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server" ForeColor="Gray" onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">��Ŀ����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxFYPC" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxFYPCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFYPC"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy��MM��dd��">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxHWMS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxHWMSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHWMS"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td rowspan="3" class="r_bg" style="text-align:center">װ����</td>
                <td class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxXS" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxXSRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxXS" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxXS"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLFM" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLFMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLFM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLFMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLFM"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">���أ�T��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxHZ" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxHZRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxHZ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxHZRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHZ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">���ر�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxRZB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxRZBRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxRZB" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxRZBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxRZB"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="r_bg">���װ����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxTJZXL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJZXLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJZXL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJZXLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJZXL"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">����װ����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZLZXL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLZXLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZLZXL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLZXLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZLZXL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="r_bg">���ͼ�����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxXXJXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxXXJXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxXXJXS"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="r_bg">װ�����ò���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZXSYCL" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxZXSYCLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZXSYCL"></asp:RequiredFieldValidator>
                </td>
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
                    <asp:Button ID="Continue" runat="server" Text="�������" OnClick="Continue_Click" CssClass="buttOver"  />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="ȷ������" OnClick="Confirm_Click" CssClass="buttOver"  />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="��������" OnClick="Back_Click" CausesValidation="false" CssClass="buttOver" />                    
                </td>
            </tr>
        </tfoot>
    </table>  
     </div>
    </div>     
    </div>
    </form>
</body>
</html>
