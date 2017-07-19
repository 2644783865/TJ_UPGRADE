<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="FM_Create_WareHouseInvDetail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Create_WareHouseInvDetail" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    下推入库单详细信息<asp:Label ID="lblwgCode" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <div style="width: 980px; height: 450px; overflow: auto; margin-right: auto; margin-left: auto;">
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <asp:Repeater ID="rptWGDetail" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle">
                        <td>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>入库单号</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>物料编码</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>物料名称</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>规格</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>采购单位</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>材质</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>国标</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>批号</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>长(mm)</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>宽(mm)</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>实收数量</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>单价</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>金额</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>税率</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>含税单价</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>含税金额</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>订单单号</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>业务员</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>制单人</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>计划跟踪号</strong>
                        </td>
                        <td style="white-space: nowrap">
                            <strong>备注</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                        <td style="white-space: nowrap">
                            <%#Container.ItemIndex+1%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_CODE") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_MARID") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("MNAME")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("GUIGE")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("CGDW")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("CAIZHI")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("GB")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_LOTNUM") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_LENGTH") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_WIDTH") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_RSNUM") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_UPRICE")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_AMOUNT")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_TAXRATE") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_CTAXUPRICE") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_CTAMTMNY") %>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_ORDERID")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("ClerkName")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("DocName")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_PTCODE")%>
                        </td>
                        <td style="white-space: nowrap">
                            <%#Eval("WG_NOTE") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
