<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchange_all.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_all"
    Title="变更管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:HyperLink ID="addpcpurbill" CssClass="hand" runat="server" BackColor="Red" Visible="false">
                                        驳回变更</asp:HyperLink>&nbsp;&nbsp;
                                        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="addpcpurbill"
                                            PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-65" CacheDynamicResults="false">
                                        </asp:PopupControlExtender>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="palPSHTLB" Style="visibility: hidden; border-style: solid; border-width: 1px;
                                border-color: blue; background-color: Menu;" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="430px">
                                            <tr>
                                                <td>
                                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                            title="关闭">X</a>
                                                    </div>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    驳回意见：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tb_bhyj" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="驳回"
                                                        Visible="false" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="overflow: auto; width: 100%; height: 600px;">
                            <div class="cpbox xscroll">
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td style="font-size: small; text-align: center;" colspan="4">
                                                变更信息
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%;" align="left">
                                                &nbsp;&nbsp;&nbsp;申&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请&nbsp;&nbsp;&nbsp;&nbsp;人:
                                                <asp:TextBox ID="tb_pname" runat="server" Text="" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="tb_pid" runat="server" Text="" Visible="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 33%;" align="left">
                                                &nbsp;&nbsp;&nbsp;变&nbsp;&nbsp;更&nbsp;&nbsp;日&nbsp;&nbsp;期:<asp:TextBox ID="Tb_shijian"
                                                    runat="server" Text="" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 34%;" align="left">
                                                &nbsp;&nbsp;&nbsp;变&nbsp;&nbsp;更&nbsp;&nbsp;批&nbsp;&nbsp;号:<asp:TextBox ID="TextBox_pid"
                                                    runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%;" align="left">
                                                &nbsp;&nbsp;&nbsp;任&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;务&nbsp;&nbsp;&nbsp;&nbsp;号:
                                                <asp:TextBox ID="tb_zh" runat="server" Text="" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 33%;" align="left">
                                                &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目:<asp:TextBox
                                                    ID="tb_pjinfo" runat="server" Text="" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                                <asp:TextBox ID="tb_pjname" runat="server" Visible="false" Text=""></asp:TextBox>
                                            </td>
                                            <td style="width: 34%;" align="left">
                                                &nbsp;&nbsp;&nbsp;设&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备:<asp:TextBox
                                                    ID="tb_enginfo" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                                <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                                <asp:TextBox ID="tb_engname" runat="server" Visible="false" Text=""></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table width="100%" class="nowrap cptable fullwidth">
                                    <asp:Repeater ID="tbpc_purbgclallRepeater" runat="server" OnItemDataBound="tbpc_purbgclallRepeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
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
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>变更数量</strong>
                                                </td>
                                                <td>
                                                    <strong>变更辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>执行数量</strong>
                                                </td>
                                                <td>
                                                    <strong>执行辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                                <td>
                                                    <strong>是否执行</strong>
                                                </td>
                                                <td>
                                                    <strong>执行人</strong>
                                                </td>
                                                <td>
                                                    <strong>查看</strong>
                                                </td>
                                                <td>
                                                    <strong>变更</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <%--<%#Container.ItemIndex + 1 %>--%>
                                                    <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_PTCODE" runat="server" Text='<%#Eval("BG_PTCODE")%>'></asp:Label>
                                                    <asp:Label ID="MP_ID" runat="server" Text='<%#Eval("MP_ID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_MARID" runat="server" Text='<%#Eval("BG_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_MARNAME" runat="server" Text='<%#Eval("BG_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_MARNORM" runat="server" Text='<%#Eval("BG_MARNORM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_MARTERIAL" runat="server" Text='<%#Eval("BG_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_GUOBIAO" runat="server" Text='<%#Eval("BG_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_NUNIT" runat="server" Text='<%#Eval("BG_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_NUM" runat="server" Text='<%#Eval("BG_NUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_FZNUM" runat="server" Text='<%#Eval("BG_FZNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_ZXNUM" runat="server" Text='<%#Eval("BG_ZXNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_ZXFZNUM" runat="server" Text='<%#Eval("BG_ZXFZNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("LENGTH")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("WIDTH")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_STATE" runat="server" Text='<%#Eval("BG_STATE")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="BG_STATETEXT" runat="server" Text='<%#get_bg_sta(Eval("BG_STATE").ToString())%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_ZXRENID" runat="server" Text='<%#Eval("BG_ZXRENID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="BG_ZXRENNM" runat="server" Text='<%#Eval("BG_ZXRENNM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%-- <asp:LinkButton ID="lkb_look" runat="server" OnClick="lkb_look_Click" PostBackUrl="">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icon-fuction/139.gif" AlternateText="确定" /></asp:LinkButton>--%>
                                                    <asp:Button ID="lkb_look" runat="server" Text="查看" OnClick="lkb_look_Click" BorderStyle="None"
                                                        Style="cursor: pointer" />
                                                </td>
                                                <td>
                                                    <%-- <asp:LinkButton ID="lkb_bg" runat="server" OnClick="lkb_bg_Click" PostBackUrl="">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/icon-fuction/139.gif" AlternateText="确定" /></asp:LinkButton>--%>
                                                    <asp:Button ID="lkb_bg" runat="server" Text="变更" OnClick="lkb_bg_Click" BorderStyle="None"
                                                        Style="cursor: pointer" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="18" align="center">
                                            <asp:Panel ID="NoDataPanebg" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td style="font-size: small; text-align: center;" colspan="4">
                                                采购计划
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table width="100%" class="nowrap cptable fullwidth">
                                    <asp:Repeater ID="tbpc_purbgyclRepeater" runat="server" OnItemDataBound="tbpc_purbgyclRepeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td id="hpihao" visible="false">
                                                    <strong>批号</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>类型</strong>
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
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>计划数量</strong>
                                                </td>
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <%-- <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>--%>
                                                <td>
                                                    <strong>采购员</strong>
                                                </td>
                                                <td>
                                                    <strong>是否行关闭</strong>
                                                </td>
                                                <td>
                                                    <strong>是否询比价</strong>
                                                </td>
                                                <td>
                                                    <strong>是否下订单</strong>
                                                </td>
                                                <td id="td0" runat="server" visible="false">
                                                    <strong>比价单号</strong>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <strong>订单号</strong>
                                                </td>
                                                <td>
                                                    <strong>状态</strong>
                                                </td>
                                                <%--  <td>
                                            <strong>备注</strong>
                                        </td>--%>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                ondblclick="databinddbl(this)">
                                                <td>
                                                    <asp:Label ID="ROWYCLSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                AutoPostBack="true" Checked="false" OnCheckedChanged="CheckedChanged"></asp:CheckBox>
                                            &nbsp;--%>
                                                </td>
                                                <td id="bpihao" visible="false">
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                </td>
                                                <%-- <td>
                                            <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                           
                                        </td>--%>
                                                <td>
                                                    <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hyhclose" runat="server" CssClass="hand">
                                                        <asp:Label ID="PUR_CSTATE1" runat="server" Text='<%#get_pur_cst(Eval("PUR_CSTATE").ToString())%>'></asp:Label></asp:HyperLink>
                                                    <asp:Label ID="PUR_CSTATE" runat="server" Text='<%#Eval("PUR_CSTATE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hypbjd" runat="server" CssClass="hand">
                                                        <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("picno").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="Hyporder" runat="server" CssClass="hand">
                                                        <asp:Label ID="PUR_ORDER" runat="server" Text='<%#get_pur_dd(Eval("orderno").ToString())%>'></asp:Label></asp:HyperLink>
                                                </td>
                                                <td id="td0" runat="server" visible="false">
                                                    <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_pur_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                </td>
                                                <%-- <td>
                                            <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                        </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="19" align="center">
                                            <asp:Panel ID="NoDataPanep" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
