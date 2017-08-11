<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YS_IN_Detail_WW.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.master" Inherits="ZCZJ_DPF.YS_Data.YS_IN_Detail_WW" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    委外入库明细
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
                        <td align="right">
                            <asp:HiddenField ID="hfdTotalNum" runat="server" />
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                            <asp:Button ID="btnInputExcel" runat="server" Width="50" Text="导 出"  onclick="btnEXCEL_Click" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="overflow: auto">
                <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                    没有相关记录!</asp:Panel>
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class=" nowrap toptable grid"
                    border="1" frame="border">
                    <asp:Repeater ID="RepeaterWG" runat="server" OnItemDataBound="RepeaterWG_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <td>
                                    序号
                                </td>
                                <td>
                                    外协单号
                                </td>
                                <td>
                                    批号
                                </td>
                                <td>
                                    任务号
                                </td>
                                <td>
                                    合同号
                                </td>
                                <td>
                                    项目名称
                                </td>
                                <td>
                                    供应商
                                </td>
                                <td>
                                    构件名称
                                </td>
                                <td>
                                    图号/标识号
                                </td>
                                <td>
                                    图中序号
                                </td>
                                <td>
                                    外协类型
                                </td>
                                <td>
                                    外协工序
                                </td>
                                <td>
                                    类型
                                </td>
                                <td>
                                    单重
                                </td>
                                <td>
                                    数量
                                </td>
                                <td>
                                    最终报价
                                </td>
                                
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("MS_WSID")%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("ptcode")%>
                                </td>
                                <td>
                                    <%#Eval("PIC_ENGID")%>
                                </td>
                                <td>
                                    <%#Eval("CM_CONTR")%>
                                </td>
                                <td>
                                    <%#Eval("CM_PROJ")%>
                                </td>
                                <td>
                                    <%#Eval("supplierresnm")%>
                                </td>
                                <td>
                                    <%#Eval("marnm")%>
                                </td>
                                <td>
                                    <asp:Label ID="LabelRN" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("PIC_ZONGXU")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("ICL_WXTYPE")%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("MS_PROCESS")%>
                                </td>
                                <td>
                                    <%#Eval("PIC_MASHAPE")%>
                                </td>
                                <td>
                                    <%#Eval("MS_UWGHT")%>
                                </td>
                                <td>
                                    <%#Eval("marzxnum")%>
                                </td>
                                <td>
                                    <%#Eval("detamount")%>
                                </td>
                               
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
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
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="TotalNum" runat="server"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="TotalAmount" runat="server"></asp:Label>
                                </td>
                               
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
