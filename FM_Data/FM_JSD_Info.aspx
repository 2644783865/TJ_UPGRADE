<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_JSD_Info.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_JSD_Info" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   原始结算单信息
   <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Panel ID="Panel1" runat="server">
   <div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                        <th>
                        序号
                        </th>
                         <th>
                        结算单号
                        </th>
                        <th>
                        订单号
                        </th>
                        <th>
                        计划跟踪号
                        </th>
                        <th>
                        供应商编号
                        </th>
                        <th>
                        供应商名称
                        </th>
                        <th>
                        制单人编号
                        </th>
                        <th>
                        制单人姓名
                        </th>
                        <th>
                        制单日期
                        </th>
                        <th>
                        数量
                        </th>
                        <th>
                        单价
                        </th>
                        <th>
                        重量
                        </th>
                        <th>
                        金额
                        </th>
                        <th>
                        结算单金额
                        </th>
                        <th>
                        结算单总重
                        </th>
                        <th>
                        外协类型
                        </th>
                        <th>
                        材质
                        </th>
                        <th>
                        图号
                        </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                        <td>
                        <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                        </td>
                        <td align="center">
                                <asp:Label ID="lbjsdh" runat="server" Enabled="false" Text='<%#Eval("TA_DOCNUM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbddh" runat="server" Enabled="false" Text='<%#Eval("TA_ORDERNUM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjhgzh" runat="server" Enabled="false" Text='<%#Eval("TA_PTC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgysbh" runat="server" Enabled="false" Text='<%#Eval("TA_SUPPLYID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgysmc" runat="server" Enabled="false" Text='<%#Eval("TA_SUPPLYNAME")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzdrbh" runat="server" Enabled="false" Text='<%#Eval("TA_ZDR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzdrxm" runat="server" Enabled="false" Text='<%#Eval("TA_ZDRNAME")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzddate" runat="server" Enabled="false" Text='<%#Eval("TA_ZDDATE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsl" runat="server" Enabled="false" Text='<%#Eval("TA_NUM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbdj" runat="server" Enabled="false" Text='<%#Eval("TA_PRICE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzl" runat="server" Enabled="false" Text='<%#Eval("TA_WGHT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbje" runat="server" Enabled="false" Text='<%#Eval("TA_MONEY")%>'></asp:Label>
                            </td> 
                            <td align="center">
                                <asp:Label ID="lbjsdje" runat="server" Enabled="false" Text='<%#Eval("TA_AMOUNT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjsdzzl" runat="server" Enabled="false" Text='<%#Eval("TA_TOTALWGHT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwxlx" runat="server" Enabled="false" Text='<%#Eval("TA_WXTYPE")%>'></asp:Label>
                            </td> 
                            <td align="center">
                                <asp:Label ID="lbcz" runat="server" Enabled="false" Text='<%#Eval("TA_CAIZHI")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbth" runat="server" Enabled="false" Text='<%#Eval("TA_TUHAO")%>'></asp:Label>
                            </td> 
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                        
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！
            </asp:Panel>
            </div>
         </asp:Panel>
</asp:Content>
