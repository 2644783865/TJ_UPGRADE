<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_Bill_summarizing.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_Bill_summarizing"
    Title="无标题页" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right" width="44%">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:Button ID="btn_insert" runat="server" Text="下推" OnClick="save_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="Button1" runat="server" Text="全部下推" OnClick="allsave_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />
                                    &nbsp;&nbsp;
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:Panel ID="Paneleng_pj" runat="server" Visible="false">
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="3">
                                物料需用单
                                <asp:TextBox ID="TextBox_pid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lablepj" runat="server" Text="项目："></asp:Label>
                                <asp:DropDownList ID="DropDownList_PJ" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_PJ_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lable_eng" runat="server" Text="工程："></asp:Label>
                                <asp:DropDownList ID="downlist_eng" runat="server" AutoPostBack="true" OnSelectedIndexChanged="downlist_eng_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:Label ID="Labelexecutor" runat="server" Text="执行人："></asp:Label>
                                <asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="border: 1px solid #000000; height: 300px; overflow: auto">
                        <div class="fixbox xscroll">
                            <table id="tab" class="nowrap fixtable fullwidth" align="center">
                                <asp:Repeater ID="billsummarizingRepeater" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                            <td>
                                                <strong>行号</strong>
                                            </td>
                                            <td>
                                                <strong>材料ID</strong>
                                            </td>
                                            <td>
                                                <strong>名称</strong>
                                            </td>
                                            <td>
                                                <strong>规格</strong>
                                            </td>
                                            <td>
                                                <strong>材质</strong>
                                            </td>
                                            <td>
                                                <strong>定尺</strong>
                                            </td>
                                            <td>
                                                <strong>所需数量</strong>
                                            </td>
                                            <td>
                                                <strong>数量单位</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                <asp:CheckBox ID="CKBOX_USEKCALTER" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                    Checked="false"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_FIXEDSIZETEXT" runat="server" Text='<%#get_pur_fixed(Eval("PUR_FIXEDSIZE").ToString())%>'></asp:Label>
                                                <asp:Label ID="PUR_FIXEDSIZE" runat="server" Text='<%#Eval("PUR_FIXEDSIZE")%>' Visible="false"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td bgcolor="#FFFFCC">
                                                <asp:Label ID="PUR_NEDDNUM" runat="server" Text='<%#Eval("PUR_NEDDNUM")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="NoDataPane" runat="server">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Label ID="message" runat="server" Text=""></asp:Label>
                                <asp:Button ID="btn_look" runat="server" Text="查看" OnClick="btn_look_Click" />
                                <asp:Button ID="backno" runat="server" Text="返回" OnClick="backno_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
