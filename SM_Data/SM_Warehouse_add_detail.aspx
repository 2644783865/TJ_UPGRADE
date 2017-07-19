<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_add_detail.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_add_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            安全库存添加/修改(带<span class="red">*</span>号的为必填项)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                    <td width="25%" height="25" align="right">
                        物料编码:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TextMarid" runat="server" OnTextChanged="TextMarid_TextChanged"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
                        <asp:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="TextMarid"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="TextBoxNameRequiredFieldValidator" ControlToValidate="TextMarid"
                            runat="server" ErrorMessage="物料编码不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        物料名称:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TBmname" runat="server"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TBmname"
                            runat="server" ErrorMessage="物料名称不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        规格型号:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TBguige" runat="server"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        材质:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TBcaizhi" runat="server"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        国标:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TBgb" runat="server"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        报警数量:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TextBoxNumber" runat="server"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
                        <asp:RequiredFieldValidator ID="DropDownListParentRequiredFieldValidator" ControlToValidate="TextBoxNumber"
                            runat="server" ErrorMessage="报警数量不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        合理库存量:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="txtReasonableNum" runat="server"></asp:TextBox>&nbsp;<font color="#ff0000">*</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtReasonableNum"
                            runat="server" ErrorMessage="合理库存量不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        单位:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        种类:
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlType">
                            <asp:ListItem Text="钢材类" Value="1"></asp:ListItem>
                            <asp:ListItem Text="焊材类" Value="2"></asp:ListItem>
                            <asp:ListItem Text="耗材类" Value="3"></asp:ListItem>
                             <asp:ListItem Text="油漆类" Value="4"></asp:ListItem>
                              <asp:ListItem Text="采购成品类" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        是否班组结算物料:
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlsfjsbz">
                            <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="25%" height="25" align="right">
                        备注:
                    </td>
                    <td width="75%" class="category">
                        <asp:TextBox ID="TextBoxNote" Text="" runat="server" TextMode="MultiLine" Height="45px"
                            Width="335px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="Confirm" runat="server" Text="确定" OnClick="Confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Cancel" runat="server" Text="取消" CausesValidation="False" OnClick="Cancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
