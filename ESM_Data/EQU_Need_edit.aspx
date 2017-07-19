<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="EQU_Need_edit.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Need_edit"
    SmartNavigation="true" %>

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
                                        采购申请单
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                        <asp:TextBox ID="tb_dep" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_depid" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                        <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                                        <asp:TextBox ID="TextBox_pid" runat="server" Text="" Width="200px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="采购单" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 300px">
                                        <div class="cpbox6 xscroll">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                            <asp:Repeater ID="EQU_Need_List_Repeater" runat="server" OnItemDataBound="EQU_Need_List_Repeater_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                                        
                                                                        <td>
                                                                            <strong>设备/备件名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>规格型号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>数量</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>需求时间</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>申购理由</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="Tr1" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'" runat="server" align="center"> 
                                                                        <td>
                                                                            <asp:TextBox ID="EquName" runat="server" Text='<%#Eval("EquName")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="EquType" runat="server" Text='<%#Eval("EquType")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="EquNum" runat="server" Text='<%#Eval("EquNum")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="XQtime" runat="server" Text='<%#Eval("XQtime")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Reason" runat="server" Text='<%#Eval("Reason")%>'></asp:TextBox>
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
                        <div>
                            <table width="100%" style="text-align: center">
                                <tr>
                                    <td>
                                        负责人:
                                        <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        申请人:
                                        <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
