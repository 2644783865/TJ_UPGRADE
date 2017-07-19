<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PM_BANZUJS.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_BANZUJS" Title="班组结算物料" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>班组低值易耗品结算</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div>
       <div>
           <div>
               <table width="100%">
                        <tr>
                            <td style="width: 23%;" align="left">
                                &nbsp;&nbsp;
                                <strong>时间：</strong>
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplyearmonth_changed_click">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplyearmonth_changed_click">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td>
                                <strong>物料名称</strong><asp:TextBox ID="tbname" runat="server"></asp:TextBox>  
                                <strong>规格</strong><asp:TextBox ID="tbguige" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <strong>班组</strong><asp:TextBox ID="tbbanzu" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <strong>任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                                <asp:Button ID="btnscsj" Text="生成当月数据" runat="server" OnClick="btnscsj_click" />
                                <asp:Button ID="brnsjdc" Text="导出" runat="server" OnClick="brnsjdc_click" />
                            </td>
                          </tr> 
                          <tr></tr>
                          <tr>
                            <td align="right" colspan="4">
                                <asp:HyperLink ID="HyLHZ" runat="server" NavigateUrl="PM_BANZUHZ.aspx"><strong>按班组物料汇总</strong></asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                          </tr> 
                          <tr></tr> 
                    </table>
           </div>
       </div>
    </div>
    <div>
       <div>
           <div  style="overflow: scroll;height: 400px;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1"
                    border="1" width="100%" >
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" style="text-overflow:ellipsis;white-space:nowrap;">
                                 <td>
                                     序号
                                 </td>
                                 <td>
                                     年月
                                 </td>
                                 <td>
                                     物料编码
                                 </td>
                                 <td>
                                     物料名称
                                 </td>
                                 <td>
                                     规格
                                 </td>
                                 <td>
                                     任务号
                                 </td>
                                 <td>
                                     项目名称
                                 </td>
                                 <td>
                                     单位
                                 </td>
                                 <td>
                                     数量
                                 </td>
                                 <td>
                                     单价
                                 </td>
                                 <td>
                                     含税单价(单价*1.17)
                                 </td>
                                 <td>
                                     金额
                                 </td>
                                 <td>
                                     含税金额(金额*1.17)
                                 </td>
                                 <td>
                                     制单人
                                 </td>
                                 <td>
                                     领用班组
                                 </td>
                                 <td>
                                     领用日期
                                 </td>
                                 <td>
                                     备注
                                 </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'" style="text-overflow:ellipsis;white-space:nowrap;">
                                <td>
                                     <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                     <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                 </td>
                                 <td>
                                    <asp:Label ID="lbBZ_YEARMONTH" runat="server" Text='<%#Eval("BZ_YEARMONTH")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_MARID" runat="server" Text='<%#Eval("BZ_MARID")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_MARNAME" runat="server" Text='<%#Eval("BZ_MARNAME")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_GUIGE" runat="server" Text='<%#Eval("BZ_GUIGE")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_RWH" runat="server" Text='<%#Eval("BZ_RWH")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_ENG" runat="server" Text='<%#Eval("BZ_ENG")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_UNIT" runat="server" Text='<%#Eval("BZ_UNIT")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_NUM" runat="server" Text='<%#Eval("BZ_NUM")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_PRICE" runat="server" Text='<%#Eval("BZ_PRICE")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbHSBZ_PRICE" runat="server" Text='<%#Eval("BZ_HSPRICE")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_CTAMOUNT" runat="server" Text='<%#Eval("BZ_CTAMOUNT")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbHSBZ_CTAMOUNT" runat="server" Text='<%#Eval("BZ_HSCTAMOUNT")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_ZDRNAME" runat="server" Text='<%#Eval("BZ_ZDRNAME")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_LYBZ" runat="server" Text='<%#Eval("BZ_LYBZ")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_DATE" runat="server" Text='<%#Eval("BZ_DATE")%>'></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lbBZ_NOTE" runat="server" Text='<%#Eval("BZ_NOTE")%>'></asp:Label>
                                 </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                           <tr>
                              <td colspan="5" align="right">
                                    合计：
                              </td>
                              <td colspan="3" align="center">
                                   
                              </td>
                              <td>
                                  <asp:Label ID="lbtotalnum" runat="server" Text=""></asp:Label>
                              </td>
                              <td colspan="2">
                                  
                              </td>
                              <td align="center">
                                  <asp:Label ID="lbtotalmny" runat="server" Text=""></asp:Label>
                              </td>
                              <td align="center">
                                  <asp:Label ID="lbtotalhsmny" runat="server" Text=""></asp:Label>
                              </td>
                              <td colspan="4">
                                  
                              </td>
                           </tr>
                        </FooterTemplate>
                    </asp:Repeater>
               </table>
           </div>
           <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
       </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
