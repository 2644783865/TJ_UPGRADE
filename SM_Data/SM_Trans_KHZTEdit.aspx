<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_KHZTEdit.aspx.cs" MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.FM_Data.SM_Trans_KHZTEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">客户自提
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />

     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    
    <div align="center">
    <table class="edittable">
        <caption>客户自提信息编辑</caption>
        <tbody>
            <tr>
                <td >年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td >日期</td>
                <td><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日" >
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td >货物名称</td>
                <td><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
          
            <tr>
                <td >数量</td>
                <td><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr>
                <td >体积（立方米）</td>
            
                <td><asp:TextBox ID="TextBoxTJ" runat="server"></asp:TextBox>
                                  
                </td>
            </tr>
           
            <tr>
                <td >备注</td>
                <td><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>                                    
        </tbody>
        <tfoot>
            <tr>
                <td colspan="2">
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
   </asp:Content>
