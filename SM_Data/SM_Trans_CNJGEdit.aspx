<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_CNJGEdit.aspx.cs" MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_CNJGEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">厂内集港
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/jscript">
    function auto1()
    {
  
      if(document.getElementById('<%=TextBoxLLZTJ.ClientID%>').value!=null)
      {
     
        if(document.getElementById('<%=TextBoxLLZZL.ClientID%>').value!=null&document.getElementById('<%=TextBoxLLZZL.ClientID%>').value!=0)
        {
          document.getElementById('<%=TextBoxRZB.ClientID%>').value=document.getElementById('<%=TextBoxLLZTJ.ClientID%>').value/document.getElementById('<%=TextBoxLLZZL.ClientID%>').value;
         
        }
      }
    }
    </script>
    
   
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>  
    <div align="center">
    <table class="edittable">
        <caption>厂内集港信息编辑</caption>
        <tbody>
            <tr>
                <td colspan="2">年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">船次/批次</td>
                <td><asp:TextBox ID="TextBoxCC" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCCRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">目的港</td>
                <td><asp:TextBox ID="TextBoxMDG" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxMDGRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMDG"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td colspan="2">理论容重比（立方米/吨）</td>
                <td><asp:TextBox ID="TextBoxRZB" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxRZBRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="填写格式不对" Text="*" ControlToValidate="TextBoxRZB" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxRZBRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxRZB"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>合同</td>
                <td>合同编号</td>
                <td><asp:TextBox ID="TextBoxHTH" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxHTHRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxHTH"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td rowspan="7">理论</td>
                <td>开始日期</td>
                <td><asp:TextBox ID="TextBoxLLSTARTDATE" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxLLSTARTDATECalendarExtender" runat="server" TargetControlID="TextBoxLLSTARTDATE" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxLLSTARTDATERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLSTARTDATE"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td>结束日期</td>
                <td><asp:TextBox ID="TextBoxLLENDDATE" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxLLENDDATECalendarExtender" runat="server" TargetControlID="TextBoxLLENDDATE" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxLLENDDATERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLENDDATE"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td>非大件重量（T）</td>
                <td><asp:TextBox ID="TextBoxLLFDJZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLFDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLFDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLFDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLFDJZL"></asp:RequiredFieldValidator>                                
                </td>
            </tr>
            <tr>
                <td>大件重量（T）</td>
                <td><asp:TextBox ID="TextBoxLLDJZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxLLDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLDJZL"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td>总体积（m3）</td>
                <td><asp:TextBox ID="TextBoxLLZTJ" runat="server" onblur="auto1()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLZTJRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="填写格式不对" Text="*" ControlToValidate="TextBoxLLZTJ" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLZTJRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLZTJ"></asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr>
                <td>总重量（T）</td>
                <td><asp:TextBox ID="TextBoxLLZZL" runat="server" onblur="auto1()"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxLLZZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="填写格式不对" Text="*" ControlToValidate="TextBoxLLZZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxLLZZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLLZZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>车次</td>
                <td><asp:TextBox ID="TextBoxLLCC" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td rowspan="3">过磅结算</td>
                <td>非大件重量（T）</td>
                <td><asp:TextBox ID="TextBoxGBFDJZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBFDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBFDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBFDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBFDJZL"></asp:RequiredFieldValidator>               
                </td>
            </tr>            
            <tr>
                <td>大件重量</td>
                <td><asp:TextBox ID="TextBoxGBDJZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBDJZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBDJZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBDJZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBDJZL"></asp:RequiredFieldValidator>                               
                </td>
            </tr>
            <tr>
                <td>金额（元）</td>
                <td><asp:TextBox ID="TextBoxGBJE" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxGBJERegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxGBJE" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxGBJERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGBJE"></asp:RequiredFieldValidator>                                               
                </td>
            </tr>            
            <tr>
                <td colspan="2">中材建设（吨）</td>
                <td ><asp:TextBox ID="TextBoxZCJSZL" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxZCJSZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZCJSZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZCJSZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZCJSZL"></asp:RequiredFieldValidator>                                                               
                </td>
            </tr>
            <tr>
                <td colspan="2">中材建设部分金额</td>
                <td><asp:TextBox ID="TextBoxZCJSJE" runat="server"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="TextBoxZCJSJERegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZCJSJE" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZCJSJERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZCJSJE"></asp:RequiredFieldValidator>                                                                               
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
                    <asp:Button ID="Continue" runat="server" Text="继续添加" OnClick="Continue_Click" CssClass="button"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Confirm" runat="server" Text="确定返回" OnClick="Confirm_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Back" runat="server" Text="放弃返回" OnClick="Back_Click" CausesValidation="false" CssClass="button" />&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </tfoot>
    </table>
    </div>
</asp:Content>
