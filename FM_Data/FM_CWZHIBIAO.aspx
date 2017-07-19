<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CWZHIBIAO.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CWZHIBIAO" Title="财务指标" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   财务指标
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    
    <div class="box-wrapper">
      <div class="box-inner">
        <div class="box-title">
           <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            &nbsp;&nbsp;&nbsp;
                            年月从:<input type="text" style="width:80px" id="yearmonthstart" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                            到:<input type="text" style="width:80px" id="yearmonthend" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                            <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
                         </td>
                         <td align="right" style="width: 100px">
                             <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="~/FM_Data/FM_CWZHIBIAODetail.aspx?action=add" Target="_blank" runat="server">
                              <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                 添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         </td>
                   </tr>
           </table>
        </div>
      </div>
      
      
       <div class="box-outer">
        <div style=" overflow:scroll">
         <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                序号
                            </td>
                            <td>
                                年月 
                            </td>
                            <td>
                                制单人
                            </td>
                            <td>
                                制单时间
                            </td>
                            <td>
                                营业成本
                            </td>
                            <%--<td>
                                营业成本预算
                            </td>--%>
                            <td>
                                销售费用
                            </td>
                            <%--<td>
                                销售费用预算
                            </td>--%>
                            <td>
                                管理费用
                            </td>
                            <%--<td>
                                管理费用预算
                            </td>--%>
                            <td>
                                财务费用
                            </td>
                            <%--<td>
                                财务费用预算
                            </td>--%>
                            <td>
                                成本费用合计
                            </td>
                            <%--<td>
                                成本费用预算
                            </td>--%>
                            <td>
                                营业收入
                            </td>
                            <%--<td>
                                营业收入预算
                            </td>--%>
                            <td>
                                利润总额
                            </td>
                            <%--<td>
                                利润总额预算
                            </td>--%>
                            <td>
                                备注
                            </td>
                            <td>
                                查看
                            </td>
                            <td>
                                修改
                            </td>
                            <td>
                                删除
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <asp:Label ID="id_cwzb" runat="server" visible="false" Text='<%#Eval("id_cwzb")%>'></asp:Label>
                            <td>
                                 <asp:CheckBox ID="chkDel" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                 Checked="false"></asp:CheckBox>&nbsp;
                                 <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="cw_yearmonth" runat="server" Text='<%#Eval("cw_yearmonth")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="cw_zdrname" runat="server" Text='<%#Eval("cw_zdrname")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="cw_zdtime" runat="server" Text='<%#Eval("cw_zdtime")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="yychengben" runat="server" Text='<%#Eval("yychengben")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="yychengbenys" runat="server" ForeColor="red" Text='<%#Eval("yychengbenys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="xsfeiyong" runat="server" Text='<%#Eval("xsfeiyong")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="xsfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("xsfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="glfeiyong" runat="server" Text='<%#Eval("glfeiyong")%>'></asp:Label>
                            </td>
                             <%--<td align="center">
                                <asp:Label ID="glfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("glfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="cwfeiyong" runat="server" Text='<%#Eval("cwfeiyong")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="cwfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("cwfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="cbfeiyonghj" runat="server" Text='<%#Eval("cbfeiyonghj")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="cbfeiyonghjys" runat="server" ForeColor="red" Text='<%#Eval("cbfeiyonghjys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="yyshouru" runat="server" Text='<%#Eval("yyshouru")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="yyshouruys" runat="server" ForeColor="red" Text='<%#Eval("yyshouruys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="lrzonge" runat="server" Text='<%#Eval("lrzonge")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="lrzongeys" runat="server" ForeColor="red" Text='<%#Eval("lrzongeys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:TextBox ID="cw_note" runat="server" Enabled="false" Text='<%#Eval("cw_note")%>' ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;text-align: center" Width="100px" ToolTip='<%#Eval("cw_note")%>'></asp:TextBox>
                            </td> 
                                               
                            <td id="Td1" runat="server" align="center">
                                <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%#"~/FM_Data/FM_CWZHIBIAODetail.aspx?action=view&id_cwzb="+Eval("id_cwzb")%>' Target="_blank" runat="server">
                                    <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />查看</asp:HyperLink>
                            </td>
                            <td id="Td2" runat="server" align="center">
                                <asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%#"~/FM_Data/FM_CWZHIBIAODetail.aspx?action=edit&id_cwzb="+Eval("id_cwzb")%>' Target="_blank" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />修改</asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButtonSC" OnClick="btndelete_OnClick" CommandArgument='<%# Eval("id_cwzb")%>'  OnClientClick="return confirm('确认删除吗?')" runat="server">
                                                <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录!<br />
                <br />
            </asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
          </div>    
        </div>
    </div>
</asp:Content>
