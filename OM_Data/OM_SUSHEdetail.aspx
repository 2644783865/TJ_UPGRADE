<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_SUSHEdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SUSHEdetail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
新增宿舍信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer">
            <table id="Table1" runat="server" width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                <tr>
                    <td>
                        房间号
                    </td>
                    <td>
                        <asp:TextBox ID="tbhousenum" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    
                    <td>
                        现有人数
                    </td>
                    <td>
                        <asp:Label ID="lbxyrs" runat="server"></asp:Label>
                    </td>
                    <td>
                        容积人数
                    </td>
                    <td>
                        <asp:TextBox ID="tbrjrs" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        可入住数量
                    </td>
                    <td>
                        <asp:Label ID="lbkrzsl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        上下铺数量
                    </td>
                    <td>
                        <asp:TextBox ID="tbshangxp" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        组合床数量
                    </td>
                    <td>
                        <asp:TextBox ID="tbzuhc" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        单人床数量
                    </td>
                    <td>
                        <asp:TextBox ID="tbdanrc" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        衣柜数量
                    </td>
                    <td>
                        <asp:TextBox ID="tbyignum" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        椅子数量
                    </td>
                    <td>
                        <asp:TextBox ID="tbyiznum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        电视
                    </td>
                    <td>
                        <asp:TextBox ID="tbdiansbum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        电视柜
                    </td>
                    <td>
                        <asp:TextBox ID="tbdiansgnum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        空调
                    </td>
                    <td>
                        <asp:TextBox ID="tbkongtnum" runat="server"></asp:TextBox>
                    </td>
                 </tr>
                 <tr>
                    <td>
                        写字台
                    </td>
                    <td>
                        <asp:TextBox ID="tbxieztnum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        可调床铺个数
                    </td>
                    <td>
                        <asp:TextBox ID="tbketcpnum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        备注
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="tbnotess" runat="server" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        入住人员1
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_NAME1" runat="server" OnTextChanged="Textname_TextChanged1"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtST_NAME1"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Label ID="lbstid1" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        入住人员2
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_NAME2" runat="server" OnTextChanged="Textname_TextChanged2"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtST_NAME2"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Label ID="lbstid2" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        入住人员3
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_NAME3" runat="server" OnTextChanged="Textname_TextChanged3"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtST_NAME3"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Label ID="lbstid3" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        入住人员4
                    </td>
                    <td>
                        <asp:TextBox ID="txtST_NAME4" runat="server" OnTextChanged="Textname_TextChanged4"
                            AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionSetCount="10"
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtST_NAME4"
                            UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:Label ID="lbstid4" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
