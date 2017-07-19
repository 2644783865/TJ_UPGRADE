<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="EQU_Repair_List.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Repair_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td width="40%">
                                        <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="按部门查看："></asp:Label>
                                        <asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="QueryButton_Click">
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="是否反馈："></asp:Label>
                                        <asp:DropDownList ID="dplBACK" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplBACK_Click">
                                            <asp:ListItem Selected="True" Text="全部" Value="%"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="未反馈"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="已反馈"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td runat="server" id="add">
                                        <asp:HyperLink ID="addpcpurbill" runat="server" NavigateUrl="~/ESM_Data/EQU_Repair_edit.aspx?action=add" Text=""><img src="../Assets/images/Add_new_img.gif" />新增维修申请</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox2 xscroll">
                                            <table id="tab" align="center" class="nowrap cptable fullwidth">
                                                <asp:Repeater ID="EQU_Repair_List_Repeater" runat="server" OnItemDataBound="EQU_Repair_List_Repeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单据编号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>型号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>报修时间</strong>
                                                            </td>
                                                            <td>
                                                                <strong>维修类别</strong>
                                                            </td>
                                                            <td>
                                                                <strong>报修内容</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申请人</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用部门</strong>
                                                            </td>
                                                            <td>
                                                                <strong>制单人</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审批状态</strong>
                                                            </td>
                                                            <td>
                                                                <strong>反馈状态</strong>
                                                            </td>
                                                            <td runat="server" id="hedit">
                                                                <strong>修改</strong>
                                                            </td>
                                                            <td runat="server" id="hlookup">
                                                                <strong>查看</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="DocuNum" runat="server" Text='<%#Eval("DocuNum")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="EquName" runat="server" Text='<%#Eval("EquName")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="EquType" runat="server" Text='<%#Eval("EquType")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SQtime" runat="server" Text='<%#Eval("SQtime")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="typeid" runat="server" Text='<%#Eval("Type")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="Type" runat="server" Text='<%#get_type(Eval("Type").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Reason" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SQRNAME" runat="server" Text='<%#Eval("SQRNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="USEDEPNAME" runat="server" Text='<%#Eval("USEDEPNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="DocuPerson" runat="server" Text='<%#Eval("DocuPerson")%>'></asp:Label>
                                                                <asp:Label ID="DocuPersonID" runat="server" Text='<%#Eval("DocuPersonID")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="PZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="feedback" runat="server" Text='<%#Eval("FeedBack")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="backTEXT" runat="server" Text='<%#get_feedback(Eval("FeedBack").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" id="bedit">
                                                                <asp:HyperLink ID="hyp_edit" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="修改"></asp:Label></asp:HyperLink>
                                                            </td>
                                                            <td runat="server" id="blookup">
                                                                <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                                                    <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="16" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
