<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_Purchange_detaila.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_detaila" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="取消" />&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" Text="确定" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="overflow: auto; width: 100%">
                    <div class="cpbox xscroll">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        变更信息
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="tbpc_purbgdetailRepeater" runat="server" OnItemDataBound="tbpc_purbgdetailRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle">
                                        <th>
                                            <strong>行号</strong>
                                        </th>
                                        <th>
                                            <strong>材料ID</strong>
                                        </th>
                                        <th>
                                            <strong>名称</strong>
                                        </th>
                                        <th>
                                            <strong>规格</strong>
                                        </th>
                                        <th>
                                            <strong>材质</strong>
                                        </th>
                                        <th>
                                            <strong>国标</strong>
                                        </th>
                                        <th>
                                            <strong>单位</strong>
                                        </th>
                                        <th>
                                            <strong>重量</strong>
                                        </th>
                                        <th>
                                            <strong>数量</strong>
                                        </th>
                                        <th>
                                            <strong>备注</strong>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <%--<%#Container.ItemIndex + 1 %>--%>
                                            &nbsp;
                                            <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARID")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_MARID" runat="server" Text='<%#Eval("BG_MARID")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NAME")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_MARNAME" runat="server" Text='<%#Eval("BG_MARNAME")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%-- <%#Eval("PUR_MARNORM")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_MARNORM" runat="server" Text='<%#Eval("BG_MARNORM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_MARTERIAL" runat="server" Text='<%#Eval("BG_MARTERIAL")%>'></asp:Label>&nbsp;
                                        </td>
                                        <%--<td>
                                                    <asp:Label ID="PUR_FIXEDSIZETEXT" runat="server" Text='<%#get_pur_fixed(Eval("PUR_FIXEDSIZE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PUR_FIXEDSIZE" runat="server" Text='<%#Eval("PUR_FIXEDSIZE")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                </td>--%>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_GUOBIAO" runat="server" Text='<%#Eval("BG_GUOBIAO")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NUNIT")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_NUNIT" runat="server" Text='<%#Eval("BG_NUNIT")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NUNIT")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_WEIGHT" runat="server" Text='<%#Eval("BG_WEIGHT")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NUNIT")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="BG_NUM" runat="server" Text='<%#Eval("BG_NUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="BG_NOTE" runat="server" Text='<%#Eval("BG_NOTE")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
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
                            <asp:Repeater ID="tbpc_purbgycldetailRepeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle">
                                        <th>
                                            <strong>行号</strong>
                                        </th>
                                        <th id="hpihao" visible="false">
                                            <strong>批号</strong>
                                        </th>
                                        <th>
                                            <strong>项目</strong>
                                        </th>
                                        <th>
                                            <strong>工程</strong>
                                        </th>
                                        <th>
                                            <strong>材料ID</strong>
                                        </th>
                                        <th>
                                            <strong>名称</strong>
                                        </th>
                                        <th>
                                            <strong>规格</strong>
                                        </th>
                                        <th>
                                            <strong>材质</strong>
                                        </th>
                                        <th>
                                            <strong>国标</strong>
                                        </th>
                                        <th>
                                            <strong>计划数量</strong>
                                        </th>
                                        <th>
                                            <strong>占用库存</strong>
                                        </th>
                                        <%-- <th>
                                                    <strong>定尺</strong>
                                                </th>--%>
                                        <th>
                                            <strong>采购数量</strong>
                                        </th>
                                        <th>
                                            <strong>单位</strong>
                                        </th>
                                        <th>
                                            <strong>计划号</strong>
                                        </th>
                                        <th>
                                            <strong>采购员</strong>
                                        </th>
                                        <th>
                                            <strong>状态</strong>
                                        </th>
                                        <th>
                                            <strong>备注</strong>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                        ondblclick="databinddbl(this)">
                                        <td>
                                            <%--<%#Container.ItemIndex + 1 %>--%>
                                            &nbsp;
                                            <asp:Label ID="ROWYCLSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td id="bpihao" visible="false">
                                            <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%-- <%#Eval("PUR_PJID")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_PJID" runat="server" Text='<%#Eval("PUR_PJID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PUR_PJNAME" runat="server" Text='<%#Eval("PUR_PJNAME")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_ENGID")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_ENGID" runat="server" Text='<%#Eval("PUR_ENGID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PUR_ENGNAME" runat="server" Text='<%#Eval("PUR_ENGNAME")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARID")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NAME")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%-- <%#Eval("PUR_MARNORM")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_MARTERIAL")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_USTNUM" runat="server" Text='<%#Eval("PUR_USTNUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <%-- <td>
                                                    <asp:Label ID="PUR_FIXEDSIZETEXT" runat="server" Text='<%#get_pur_fixed(Eval("PUR_FIXEDSIZE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PUR_FIXEDSIZE" runat="server" Text='<%#Eval("PUR_FIXEDSIZE")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                </td>--%>
                                        <td>
                                            <%-- <%#Eval("PUR_NUM")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_NUNIT")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <%--<%#Eval("PUR_PTCODE")%>&nbsp;--%>&nbsp;
                                            <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>&nbsp;
                                            <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>&nbsp;
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>'></asp:Label>&nbsp;
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="17" align="center">
                                    <asp:Panel ID="NoDataPanep" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        最新采购计划
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="tbpc_purbgxcldetailRepeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle">
                                        <th>
                                            <strong>行号</strong>
                                        </th>
                                        <th>
                                            <strong>计划号</strong>
                                        </th>
                                        <th>
                                            <strong>材料ID</strong>
                                        </th>
                                        <th>
                                            <strong>名称</strong>
                                        </th>
                                        <th>
                                            <strong>规格</strong>
                                        </th>
                                        <th>
                                            <strong>材质</strong>
                                        </th>
                                        <th>
                                            <strong>国标</strong>
                                        </th>
                                        <th>
                                            <strong>所需数量</strong>
                                        </th>
                                        <th>
                                            <strong>数量单位</strong>
                                        </th>
                                        <th>
                                            <strong>备注</strong>
                                        </th>
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
                                            <asp:Label ID="MP_TRACKNUM" runat="server" Text='<%#Eval("MP_TRACKNUM")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_MARID" runat="server" Text='<%#Eval("MP_MARID")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NAME" runat="server" Text='<%#Eval("MP_NAME")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_GUIGE" runat="server" Text='<%#Eval("MP_GUIGE")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CAIZHI" runat="server" Text='<%#Eval("MP_CAIZHI")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_STANDARD" runat="server" Text='<%#Eval("MP_STANDARD")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NUMBER" runat="server" Text='<%#Eval("MP_NUMBER")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_UNIT" runat="server" Text='<%#Eval("MP_UNIT")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NOTE" runat="server" Text='<%#Eval("MP_NOTE")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="11" align="center">
                                    <asp:Panel ID="NoDataPanex" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
