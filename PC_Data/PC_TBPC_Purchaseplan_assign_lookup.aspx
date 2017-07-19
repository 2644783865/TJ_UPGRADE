<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_assign_lookup.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_assign_lookup"
    Title="任务分工" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购计划单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td width="100px" align="left">
                                        采购任务分工：
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnBack" runat="server" Text="返 回" OnClick="btnBack_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="4">
                                        任务分工
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:TextBox
                                            ID="TextBox_pid" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="Label_view" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：<asp:TextBox ID="tb_pj" runat="server" Enabled="false"
                                            Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;工&nbsp;&nbsp;&nbsp;程：<asp:TextBox
                                            ID="tb_eng" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 33%;">
                                        &nbsp;&nbsp;&nbsp;分 &nbsp;工 &nbsp;人：<asp:TextBox ID="TextBoxexecutor" runat="server"
                                            Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorbianhao" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="Tb_shijian" runat="server"
                                            Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 34%;">
                                        &nbsp;&nbsp;&nbsp;材 &nbsp;料 &nbsp;类 &nbsp;别：<asp:DropDownList ID="DropDownList_TY"
                                            runat="server" DataTextField="PUR_TY_NAME" DataValueField="PUR_TY_ID" OnSelectedIndexChanged="DropDownList_TY_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="border: 1px solid #000000; height: 310px; overflow: auto">
                            <div style="height: 300px; overflow: auto">
                                <table width="100%" align="center" cellpadding="1" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <asp:Repeater ID="tbpc_purshaseplanrealityRepeater" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>材料ID</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                    <strong>材质</strong>
                                                </td>
                                                <td>
                                                    <strong>规格</strong>
                                                </td>
                                                <td>
                                                    <strong>定尺</strong>
                                                </td>
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <%--   <td>
                                    <strong>重量</strong>
                                </td>
                                <td>
                                    <strong>重量单位</strong>
                                </td>--%>
                                                <td>
                                                    <strong>采购人</strong>
                                                </td>
                                                <td>
                                                    <strong>当前状态</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_FIXEDSIZETEXT" runat="server" Text='<%#get_pur_fixed(Eval("PUR_FIXEDSIZE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PUR_FIXEDSIZE" runat="server" Text='<%#Eval("PUR_FIXEDSIZE")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <%--<td>
                                    <%#Eval("PUR_WEIGHT")%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("PUR_WUNIT")%>&nbsp;
                                </td>--%>
                                                <td>
                                                    <asp:Label ID="PUR_CGMANNAME" runat="server" Text='<%#Eval("PUR_CGMANNAME")%>'></asp:Label>
                                                    <asp:Label ID="PUR_CGMANCODE" runat="server" Text='<%#Eval("PUR_CGMANCODE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PRONODE" runat="server" Text='<%#get_pr_state(Eval("PUR_PRONODE").ToString())%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
