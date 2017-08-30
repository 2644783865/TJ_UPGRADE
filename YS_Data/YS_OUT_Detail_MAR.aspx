<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="YS_OUT_Detail_MAR.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_OUT_Detail_MAR" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产领料单明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript">
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:HiddenField ID="hfdTotalRN" runat="server" />
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                            <asp:HiddenField ID="hfdPageNum" runat="server" />
                            <asp:HiddenField ID="hfdTotalFRN" runat="server" />
                        </td>
                        <td align="right">
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="overflow: auto">
                <asp:Panel ID="PanelBody" runat="server">
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                        没有相关记录!</asp:Panel>
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class=" nowrap toptable grid"
                        border="1" frame="border">
                        <asp:Repeater ID="RepeaterLL" runat="server" OnItemDataBound="RepeaterLL_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        新单编号
                                    </td>
                                    <td>
                                        领料部门
                                    </td>
                                    <td>
                                        旧单编号
                                    </td>
                                    <td>
                                        生产制号
                                    </td>
                                    <td>
                                        物料代码
                                    </td>
                                    <td>
                                        物料名称
                                    </td>
                                    <td>
                                        型号规格
                                    </td>
                                    <td>
                                        材质
                                    </td>
                                    <td>
                                        批号
                                    </td>
                                    <td>
                                        长
                                    </td>
                                    <td>
                                        宽
                                    </td>
                                    <td>
                                        单位
                                    </td>
                                    <td>
                                        实发数量
                                    </td>
                                    <td>
                                        张(支)
                                    </td>
                                    <td>
                                        单价
                                    </td>
                                    <td>
                                        金额
                                    </td>
                                    <td>
                                        发料人
                                    </td>
                                    <td>
                                        制单人
                                    </td>
                                    <td>
                                        制单日期
                                    </td>
                                    <td>
                                        审核人
                                    </td>
                                    <td>
                                        审核日期
                                    </td>
                                    <td>
                                        审核状态
                                    </td>
                                    <td>
                                        仓库
                                    </td>
                                    <td>
                                        仓位
                                    </td>
                                    <td>
                                        计划跟踪号
                                    </td>
                                    <td>
                                        制作班组
                                    </td>
                                    <td>
                                        计划模式
                                    </td>
                                    <td>
                                        标识号
                                    </td>
                                    <td>
                                        张数
                                    </td>
                                    <td>
                                        备注
                                    </td>
                                    <td>
                                        表头备注
                                    </td>
                                    <%--<td>
                                        合同号
                                    </td>--%>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelTrueCode" runat="server" Text='<%#Eval("TrueCode")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("Dep")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("TSAID")%>
                                    </td>
                                    <td>
                                        <%#Eval("MaterialCode")%>
                                    </td>
                                    <td>
                                        <%#Eval("MaterialName")%>
                                    </td>
                                    <td>
                                        <%#Eval("MaterialStandard")%>
                                    </td>
                                    <td>
                                        <%#Eval("Attribute")%>
                                    </td>
                                    <td>
                                        <%#Eval("LotNumber")%>
                                    </td>
                                    <td>
                                        <%#Eval("Length")%>
                                    </td>
                                    <td>
                                        <%#Eval("Width")%>
                                    </td>
                                    <td>
                                        <%#Eval("Unit")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelFRN" runat="server" Text='<%#Eval("RealSupportNumber")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("UnitPrice")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("Sender")%>
                                    </td>
                                    <td>
                                        <%#Eval("Doc")%>
                                    </td>
                                    <td>
                                        <%#Eval("Date")%>
                                    </td>
                                    <td>
                                        <%#Eval("Verifier")%>
                                    </td>
                                    <td>
                                        <%#Eval("ApprovedDate")%>
                                    </td>
                                    <td>
                                        <%#convertState((string)Eval("State"))%>
                                    </td>
                                    <td>
                                        <%#Eval("Warehouse")%>
                                    </td>
                                    <td>
                                        <%#Eval("PositionOut")%>
                                    </td>
                                    <td>
                                        <%#Eval("PTC")%>
                                    </td>
                                    <td>
                                        <%#Eval("ZZBZNM")%>
                                    </td>
                                    <td>
                                        <%#Eval("PlanMode")%>
                                    </td>
                                    <td>
                                        <%#Eval("BSH")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelPageNum" runat="server" Text='<%#Eval("OP_PAGENUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("Note")%>
                                    </td>
                                    <td>
                                        <%#Eval("OP_NOTE1")%>
                                    </td>
                                    <%--<td>
                                        <%#Eval("TSA_ContractCode")%>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        总计:
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="TotalRN" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TotalFRN" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="TotalAmount" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="TotalPageNum" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
