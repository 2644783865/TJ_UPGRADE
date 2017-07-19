<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Expense_look.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Expense_look" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lbl_spzt" runat="server" Text="Label" Visible="true"></asp:Label>
                                <asp:Button ID="btn_Audit" runat="server" Text="提交审批" OnClick="btn_Audit_Click" OnClientClick="javascript:return confirm('确认提交吗？');" />
                                &nbsp;
                                <asp:HyperLink ID="hyl_back" runat="server" CssClass="hand" onclick="history.go(-1);">返回上一页</asp:HyperLink>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:TabContainer ID="tab_Add_caigou" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0">
            <asp:TabPanel ID="Tab_audit" runat="server" HeaderText="审 核" TabIndex="0">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_info" runat="server" Text="* 所有审核通过后将自动下推，请仔细检查！" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rblSHDJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblSHDJ_Changed">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        生产工时审批
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Panel ID="pal_first" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                                            PopupControlID="pal_select1">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select1_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_second" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect2"
                                                            PopupControlID="pal_select2">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select2" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select2_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_third" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect3"
                                                            PopupControlID="pal_select3">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select3" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select3_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr3_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_cgd" runat="server" HeaderText="生产工时" TabIndex="1">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-wrapper">
                            <%-- <div style="height: 6px" class="box_top">
                            </div>--%>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: x-large; text-align: center;" colspan="3">
                                            生产工时审批
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  align="center">
                                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                            <asp:TextBox ID="txt_date" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                        <td  visible="false">
                                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                                            <asp:TextBox ID="txt_docunum" runat="server" Text="" Width="200px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="border: 1px solid #000000;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div style="border: 1px solid #000000; height: 330px">
                                                <div class="cpbox5 xscroll">
                                                    <table id="tab" class="nowrap cptable fullwidth">
                                                        <asp:Repeater ID="PM_GongShi_List_Repeater" runat="server">
                                                           <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                                        <td>
                                                                            <strong>行号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>任务单号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>顾客名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>合同号</strong>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <strong>图号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>备注</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>总计工时</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                           <ItemTemplate>
                                                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                                        <td>
                                                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                            <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                                onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>'/>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="TSA_MAP" runat="server" Text='<%#Eval("TSA_MAP")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="GS_NOTE" runat="server" Text='<%#Eval("GS_NOTE")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="GS_HOURS" runat="server" Text='<%#Eval("GS_HOURS")%>'/>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td colspan="17" align="center">
                                                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                    没有数据！</asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
