<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Trans_LDHYEdit.aspx.cs"  MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_LDHYEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">零担货运
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <title>零担货运信息编辑</title>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager> 
    <div align="center">
     <table class="edittable">
        <caption>零担货运信息编辑</caption>
        <tbody>
            <tr>
                <td>年度</td>
                <td><asp:TextBox ID="TextBoxYEAR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYEARRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYEAR"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>项目名称</td>
                <td><asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxProjectRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxProject"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>货物名称</td>
                <td><asp:TextBox ID="TextBoxGOODNAME" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxGOODNAMERequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxGOODNAME"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>件数</td>
                <td><asp:TextBox ID="TextBoxNUM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxNUMRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxNUM" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxNUMRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxNUM"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>包装形式</td>
                <td><asp:TextBox ID="TextBoxBZXS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxBZXSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxBZXS"></asp:RequiredFieldValidator>
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
                <td>重量（KG）</td>
                <td><asp:TextBox ID="TextBoxZL" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxZLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxZL" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxZLRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxZL"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>应付运费（元）</td>
                <td><asp:TextBox ID="TextBoxYFYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYFYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYFYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYFYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYFYF"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>应收运费（元）</td>
                <td><asp:TextBox ID="TextBoxYSYF" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="TextBoxYSYFRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxYSYF" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="TextBoxYSYFRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSYF"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>运输方式</td>
                <td><asp:TextBox ID="TextBoxYSFS" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxYSFSRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxYSFS"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>发运日期</td>
                <td><asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBoxDateCalendarExtender" runat="server" TargetControlID="TextBoxDate" Format="yyyy年MM月dd日">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="TextBoxDateRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxDate"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>
            <tr>
                <td>操作人</td>
                <td><asp:TextBox ID="TextBoxCZR" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TextBoxCZRRequiredFieldValidator" runat="server" ErrorMessage="*" ControlToValidate="TextBoxCZR"></asp:RequiredFieldValidator>                                        
                </td>
            </tr>            
            <tr>
                <td>备注</td>
                <td><asp:TextBox ID="TextBoxBZ" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>运费结算情况</td>
                <td><asp:TextBox ID="TextBoxYFJSQK" runat="server" TextMode="MultiLine" Height="45px" Width="335px"></asp:TextBox></td>
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
   </asp:Content>
