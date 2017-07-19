<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_CanBuAdd.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CanBuAdd" Title="新增餐补统计" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增餐补统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer">
            <table id="Table1" runat="server" width="80%">
                <tr>
                    <td style="width: 20%">
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                    </td>
                    <td style="width:20%">
                        <asp:DropDownList ID="ddlMoth" runat="server">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                    </td>
                    <td style="width: 20%">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_OnClick" />
                    </td>
                </tr>
            </table>
            <table align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                <tr>
                    <td>
                        工号
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_WORKNO" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        姓名
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_NAME" runat="server" OnTextChanged="Textname_TextChanged"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtST_NAME"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Label ID="lbstid" runat="server" Visible="false"></asp:Label>
                    </td>
                    
                    <td>
                        部门
                    </td>
                    <td>
                        <asp:TextBox ID="txtDEP_NAME" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        工作日出勤
                    </td>
                    <td>
                        <asp:TextBox ID="txtKQ_CHUQIN" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        餐补天数
                    </td>
                    <td>
                        <asp:TextBox ID="txtKQ_CBTS" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    
                    <td>
                        调整天数
                    </td>
                    <td>
                        <asp:TextBox ID="txt_cbtzts" runat="server" ></asp:TextBox>
                    </td>
                    
                    
                    <td>
                        餐补标准
                    </td>
                    <td>
                        <asp:TextBox ID="txtCB_BIAOZ" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        月度餐补
                    </td>
                    <td>
                        <asp:TextBox ID="txtCB_MonthCB" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        补发
                    </td>
                    <td>
                        <asp:TextBox ID="txtCB_BuShangYue" runat="server" OnTextChanged="txtCB_BuShangYue_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        合计
                    </td>
                    <td>
                        <asp:TextBox ID="txtCB_HeJi" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        备注
                    </td>
                    <td>
                        <asp:TextBox ID="txtCB_BeiZhu" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
