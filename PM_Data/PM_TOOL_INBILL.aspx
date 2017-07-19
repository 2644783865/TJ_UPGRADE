<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_TOOL_INBILL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_TOOL_INBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btn_addrow" runat="server" Text="增加行" OnClick="btn_addrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_delectrow" runat="server" Text="删除行" OnClick="btn_delectrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClientClick="history.go(-1)" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;
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
                                    <td style="font-size: x-large; text-align: center;" colspan="3">
                                        刀具入库单
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        入库单号：<asp:Label ID="lblInCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        收料员：<asp:Label ID="lblInDoc" runat="server"></asp:Label>
                                        <asp:Label ID="lblInDocID" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td align="left">
                                        收料日期：<asp:Label ID="lblInDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="刀具入库单" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 300px">
                                        <div class="cpbox6 xscroll">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                            <asp:Repeater ID="PM_TOOLIN_List_Repeater" runat="server" >
                                                                <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                                        <td>
                                                                            <strong>行号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>刀具类别</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>规格型号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>数量</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>备注</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="Tr1" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                                        runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                            <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" onclick="checkme(this)"
                                                                                Checked="false"></asp:CheckBox>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TYPE" runat="server" Text='<%#Eval("TYPE")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="NAME" runat="server" Text='<%#Eval("NAME")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="MODEL" runat="server" Text='<%#Eval("MODEL")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="INNUM" runat="server" Text='<%#Eval("INNUM")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="NOTE" runat="server" Text='<%#Eval("NOTE")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                                <td colspan="16" align="center">
                                                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                        没有数据！</asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
