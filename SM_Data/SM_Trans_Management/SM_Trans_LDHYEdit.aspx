<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_LDHYEdit.aspx.cs" MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_LDHYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">�㵣����
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
     <div align="center">
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
    <tr>
    <td style="padding-top:3px; font-size:medium">�㵣������Ϣ�༭</td>
    <td width="100px" align="right" valign="middle">
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
     <table class="tabGg" cellpadding="0" cellspacing="0">        
        <tbody>
            <tr>
                <td class="r_bg">���</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server" ForeColor="Gray" onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">��Ŀ����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">����</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">��װ��ʽ</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxBZXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxBZXS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">�����m3��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxTJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">������KG��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">Ӧ���˷ѣ�Ԫ��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYFYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYFYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYFYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYFYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYFYF"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="r_bg">Ӧ���˷ѣ�Ԫ��</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYSYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYSYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYSYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYSYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSYF"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">���䷽ʽ</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYSFS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYSFSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSFS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td class="r_bg">��������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy��MM��dd��">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td class="r_bg">������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxCZR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCZRRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCZR"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td class="r_bg">��ע</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="r_bg">�˷ѽ������</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYFJSQK" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>                                     
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" class="right_bg">
                    �༭ʱ�䣺<asp:Label ID="LabelDate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    �༭�ˣ�<asp:Label ID="LabelClerk" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" class="right_bg" style="text-align:center">
                    <asp:Button ID="Continue" runat="server" Text="�������" OnClick="Continue_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="ȷ������" OnClick="Confirm_Click" CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="��������" OnClick="Back_Click" CausesValidation="false" CssClass="buttOver" />                    
                </td>
            </tr>
        </tfoot>
    </table>       
    </div>
    </div>
    </div>
   </asp:Content>