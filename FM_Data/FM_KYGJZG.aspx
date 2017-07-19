<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_KYGJZG.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_KYGJZG" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>跨月勾稽暂估单汇总</strong>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" language="javascript">
function gyshz() {
        window.open("FM_KYGJGYSHZ.aspx?FLAG=to");
    }
</script>
<asp:Panel ID="Panel1" runat="server">
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                 <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                  时间：
                                <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                物料类型：
                                    <asp:DropDownList ID="dplwltype" runat="server" Width="100px">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="0">钢材</asp:ListItem>
                                                        <asp:ListItem Value="1">五金库</asp:ListItem>
                                   </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td align="right">
                                <input id="gyshz" type="button" value="按供应商汇总" onclick="gyshz()" runat="server" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_OnClick" />&nbsp;&nbsp;&nbsp;
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
                        入库单号
                        </th>
                        <th>
                        发票编号
                        </th>
                        <th>
                        勾稽日期
                        </th>
                        <th>
                        入库单日期
                        </th>
                        <th>
                        供应商
                        </th>
                        <th>
                        物料编码
                        </th>
                        <th>
                        物料名称
                        </th>
                        <th>
                        规格
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
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <td>
                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbrkdh" runat="server" Enabled="false" Text='<%#Eval("GI_INCOED")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbfpbh" runat="server" Enabled="false" Text='<%#Eval("GI_CODE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbfpdate" runat="server" Enabled="false" Text='<%#Eval("GI_DATE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbrkddate" runat="server" Enabled="false" Text='<%#Eval("WG_VERIFYDATE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbgys" runat="server" Enabled="false" Text='<%#Eval("GI_SUPPLIERNM")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbwlbm" runat="server" Enabled="false" Text='<%#Eval("GI_MATCODE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbwlname" runat="server" Enabled="false" Text='<%#Eval("GI_NAME")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbguige" runat="server" Enabled="false" Text='<%#Eval("GI_GUIGE")%>'></asp:Label>
                            </td>
                            

                            <td align="center">
                                <asp:Label ID="lbsl" runat="server" Enabled="false" Text='<%#Eval("GI_NUM")%>'></asp:Label>
                            </td>
                            
                            
                            <td align="center">
                                <asp:Label ID="lbje" runat="server" Enabled="false" Text='<%#Eval("GI_INAMTMNY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsje" runat="server" Enabled="false" Text='<%#Eval("GI_INCATAMTMNY")%>'></asp:Label>
                            </td>
                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="2" align="right">
                            合计:
                            </th>
                            <th colspan="8">
                            
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
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！</asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            </div>
 </div>
         </asp:Panel>
</asp:Content>
