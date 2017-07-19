<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_CNJGEdit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_CNJGEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
    <title>���ڼ�����Ϣ�༭</title>
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
 function auto_RZB()    {        if(document.getElementById('<%=TextBoxLLZTJ.ClientID%>').value!=null)      {             if(document.getElementById('<%=TextBoxLLZZL.ClientID%>').value!=null&document.getElementById('<%=TextBoxLLZZL.ClientID%>').value!=0)        {          document.getElementById('<%=TextBoxRZB.ClientID%>').value=document.getElementById('<%=TextBoxLLZTJ.ClientID%>').value/document.getElementById('<%=TextBoxLLZZL.ClientID%>').value;                 }      }    }
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
    <td style="padding-top:3px; font-size:medium">���ڼ�����Ϣ�༭</td>
    <td width="100px"  valign="middle">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Back_Click" CausesValidation="false">
      <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/Assets/images/goback.gif" runat="server" Width="18px" Height="18px"/> 
        ������һ��</asp:LinkButton>&nbsp;
    </td>
    </tr></table>    
    
    </div>
    </div>
    </div>
    <div class="box-wrapper">
    <div class="box-outer">
    <table class="tabGg" cellpadding="0" cellspacing="0" >        
        <tbody>
            <tr>
                <td colspan="2"  class="r_bg" >���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server" ForeColor="Gray" onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="r_bg" >����/����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxCC" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="r_bg" >Ŀ�ĸ�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxMDG" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxMDGRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMDG"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2"  class="r_bg" >�������رȣ�������/�֣�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxRZB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxRZBRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxRZB" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxRZBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxRZB"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg"  style="text-align:center">��ͬ</td>
                <td  class="r_bg" >��ͬ���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxHTH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxHTHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHTH"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td rowspan="7" class="r_bg" style="text-align:center">����</td>
                <td  class="r_bg" >��ʼ����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLSTARTDATE" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxLLSTARTDATECalendarExtender" runat="server" TargetControlID="TextBoxLLSTARTDATE" Format="yyyy��MM��dd��">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxLLSTARTDATERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLSTARTDATE"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td  class="r_bg" >��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLENDDATE" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxLLENDDATECalendarExtender" runat="server" TargetControlID="TextBoxLLENDDATE" Format="yyyy��MM��dd��">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxLLENDDATERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLENDDATE"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td  class="r_bg" >�Ǵ��������T��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLFDJZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLFDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLFDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLFDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLFDJZL"></asp:RequiredFieldValidator>                                
                </td>
            </tr>
            <tr>
                <td  class="r_bg" >���������T��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLDJZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLDJZL"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td  class="r_bg" >�������m3��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLZTJ" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLZTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLZTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLZTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLZTJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td  class="r_bg" >��������T��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLZZL" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLZZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLZZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLZZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLZZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td  class="r_bg" >����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxLLCC" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td rowspan="3" class="r_bg" style="text-align:center">��������</td>
                <td  class="r_bg" >�Ǵ��������T��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGBFDJZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBFDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBFDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBFDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBFDJZL"></asp:RequiredFieldValidator>               
                </td>
            </tr>            
            <tr>
                <td  class="r_bg" >�������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGBDJZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBDJZL"></asp:RequiredFieldValidator>                               
                </td>
            </tr>
            <tr>
                <td  class="r_bg" >��Ԫ��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGBJE" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBJERegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBJE" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBJERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBJE"></asp:RequiredFieldValidator>                                               
                </td>
            </tr>            
            <tr>
                <td colspan="2"  class="r_bg" >�вĽ��裨�֣�</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZCJSZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxZCJSZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZCJSZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZCJSZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZCJSZL"></asp:RequiredFieldValidator>                                                               
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="r_bg" >�вĽ��貿�ֽ��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZCJSJE" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxZCJSJERegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZCJSJE" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZCJSJERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZCJSJE"></asp:RequiredFieldValidator>                                                                               
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="r_bg" >��ע</td>
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
                    <asp:Button ID="Continue" runat="server" Text="�������" OnClick="Continue_Click" CssClass="buttOver"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="ȷ������" OnClick="Confirm_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="��������" OnClick="Back_Click" CausesValidation="false" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
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
