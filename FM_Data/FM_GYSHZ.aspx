<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_GYSHZ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_GYSHZ" Title="未勾稽按供应商汇总" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>未勾稽按供应商汇总</strong>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
<asp:Panel ID="Panel1" runat="server">
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                 <table style="width: 100%;">
                        <tr>
                            <td style="width: 22%;">
                                  时间：
                                <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;供应商：<asp:TextBox ID="gys" runat="server" Width="80px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_OnClick" />
                            </td>
                            <td style="width: 5%;">
                                
                            </td>
                        </tr>
                    </table>
              </div>
         </div>
   <div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <th>
                            序号
                            </th>
                            <th>
                            供应商
                            </th>
                            <th>
                            金额
                            </th>
                            <th>
                            含税金额
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
                                <asp:Label ID="lbgys" runat="server" Enabled="false" Text='<%#Eval("SupplierName")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbje" runat="server" Enabled="false" Text='<%#Eval("WG_AMOUNT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsje" runat="server" Enabled="false" Text='<%#Eval("WG_CTAMTMNY")%>'></asp:Label>
                            </td>
                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="2" align="right">
                            合计:
                            </th>
                            <th>
                               <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！</asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            </div>
 </div>
         </asp:Panel>
</asp:Content>
