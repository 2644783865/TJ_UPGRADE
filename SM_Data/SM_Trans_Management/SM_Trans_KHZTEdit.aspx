<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_KHZTEdit.aspx.cs" MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.FM_Data.SM_Trans_Management.SM_Trans_KHZTEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">客户自提
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/main.css" rel="stylesheet" type="text/css" />
 <script  type="text/javascript" language="javascript">
  function auto_RZB()    {        if(document.getElementById('<%=TextBoxZTJ.ClientID%>').value!=null)      {             if(document.getElementById('<%=TextBoxZZL.ClientID%>').value!=null&document.getElementById('<%=TextBoxZZL.ClientID%>').value!=0)        {          document.getElementById('<%=TextBoxRZB.ClientID%>').value=(document.getElementById('<%=TextBoxZTJ.ClientID%>').value/document.getElementById('<%=TextBoxZZL.ClientID%>').value).toFixed(2);                 }      }    }
  function DefaultTextOnFocus(obj)
  {
     if(obj.value=="格式：2000年")
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
        obj.value="格式：2000年";
        obj.style.color="Gray";
     }
     else
     {
        var num=obj.value;
        var patten=/^[0-9][0-9][0-9][0-9]['年']$/;
        if(!patten.test(num))
        { alert('请输入正确格式！！！\r\r如：2000年');
        obj.value="格式：2000年";
        obj.focus();
        }
     }
  }
</script>
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
     <div align="center">
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
    <tr>
    <td style="padding-top:3px; font-size:medium"> 客户自提信息编辑</td>
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
                <td colspan="3" class="r_bg">年度</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxYEAR" runat="server" ForeColor="Gray" onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  colspan="3" class="r_bg">项目名称</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxPjname" runat="server"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="TextBoxPjnameRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxPjname"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td colspan="3" class="r_bg">工程名称</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxENGNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxENGNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxENGNAME"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
          
            <tr>
                <td colspan="3" class="r_bg">总重量</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZZL" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZZL"></asp:RequiredFieldValidator>                
                </td>
            </tr> 
            <tr>
                <td  colspan="3" class="r_bg">总体积</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZTJ" runat="server" onblur="auto_RZB()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZTJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td colspan="3" class="r_bg">容重比</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxRZB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxRZBRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxRZB" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxRZBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxRZB"></asp:RequiredFieldValidator>                
                </td>
            </tr> 
                        
            <tr>
                <td rowspan="5" class="r_bg" style="text-align:center">实际发运量</td>
                <td colspan="2" class="r_bg">登记日期</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxDJTIME" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDJTIMECalendarExtender" runat="server" TargetControlID="TextBoxDJTIME" Format="yyyy-MM-dd">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDJTIMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDJTIME"></asp:RequiredFieldValidator>                                                        
                </td>
            </tr>
            <tr>
                <td rowspan="2" class="r_bg" style="text-align:center">本期发运量</td>
                <td  class="r_bg">重量（吨）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td class="r_bg">体积（立方米）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxTJ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td rowspan="2" class="r_bg" style="text-align:center">本年度累计发运量</td>
                <td  class="r_bg">重量（吨）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxZL1" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZL1RegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL1" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZL1RequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL1"></asp:RequiredFieldValidator>
                </td></tr>
                <tr>
                <td class="r_bg">体积（立方米）</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxTJ1" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxTJ1RegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxTJ1" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxTJ1RequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTJ1"></asp:RequiredFieldValidator>
                </td>
            </tr>
           
            <tr>
                <td  colspan="3" class="r_bg">备注</td>
                <td class="right_bg"><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>                                    
        </tbody>
        <tfoot>
            <tr>
                <td colspan="4" class="right_bg">
                    编辑时间：<asp:Label ID="LabelDate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    编辑人：<asp:Label ID="LabelClerk" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center" class="right_bg">
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click"  CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click"  CssClass="buttOver" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" CausesValidation="false" OnClick="Back_Click"  CssClass="buttOver" />                    
                </td>
            </tr>
        </tfoot>
    </table>
    </div></div>
    </div>
   </asp:Content>
