<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_YFHS.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YFHS" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>运费核算</strong>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Panel ID="Panel1" runat="server">
 <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                 <table style="width: 100%;">
                        <tr>
                            <td style="width: 40%;">
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
                            <td style="width: 30%;">
                                
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Button ID="btnHS" runat="server" Text="核算" OnClick="btnHS_Click"/>                            
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Button ID="btnFHS" runat="server" Visible="false" Text="反核算" OnClick="btnFHS_Click"/>           
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
                        结算单号
                        </th>
                        <th>
                        发票编号
                        </th>
                        <th>
                        供应商
                        </th>
                        <th>
                        图号
                        </th>
                        <th>
                        设备名称
                        </th>
                        <th>
                        重量
                        </th>
                        <th>
                        数量
                        </th>
                        <th>
                        金额
                        </th>
                        <th>
                        含税金额
                        </th>
                        <th>
                        勾稽日期
                        </th>
                        <th>
                        勾稽人
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
                                <asp:Label ID="lbjsdh" runat="server" Enabled="false" Text='<%#Eval("YFGI_JSDID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbfpbh" runat="server" Enabled="false" Text='<%#Eval("YFGJ_FPID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgys" runat="server" Enabled="false" Text='<%#Eval("YFGI_GYSNAME")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbtuhao" runat="server" Enabled="false" Text='<%#Eval("YFGJ_CPBH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsbmc" runat="server" Enabled="false" Text='<%#Eval("YFGJ_CPMC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzl" runat="server" Enabled="false" Text='<%#Eval("YFGJ_WGHT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsl" runat="server" Enabled="false" Text='<%#Eval("YFGJ_NUM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbje" runat="server" Enabled="false" Text='<%#Eval("YFGJ_JE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsje" runat="server" Enabled="false" Text='<%#Eval("YFGJ_HSJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgjdate" runat="server" Enabled="false" Text='<%#Eval("YFGJ_GJDATE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgjr" runat="server" Enabled="false" Text='<%#Eval("YFGJ_GJRNAME")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="2" align="right">
                            合计:
                            </th>
                            <th colspan="6">
                            
                            </th>
                            <th>
                               <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                            <th>
                            
                            </th>
                            <th>
                            
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！</asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            </div>
 </div>
         </asp:Panel>
</asp:Content>
