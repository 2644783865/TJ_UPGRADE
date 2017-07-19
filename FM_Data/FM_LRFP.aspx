<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_LRFP.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_LRFP" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
利润及利润分配表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script> 
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
   <div class="box-wrapper">
      <div class="box-inner">
        <div class="box-title">
        <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%;">
                            <strong>时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                         </td>
                         <td>
                         <asp:FileUpload runat="server" ID="FileUpload1" Width="200px" />
                         </td>
                         <td>
                             <asp:Button ID="btn_Import_Click" runat="server" Text="导入" 
                                 onclick="btn_Import_Click_Click" />
                         </td>
                         <td>
                            <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_Click" />
                         </td>
                         <td align="right" style="width: 358px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_LRFP_Detail.aspx?FLAG=ADD','','dialogWidth=650px;dialogHeight=400px');" runat="server">
                          <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             添加</asp:HyperLink>&nbsp;&nbsp;
                         </td>
                         <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？');" onclick="btnSC_Click"  />
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
                            <td colspan="4" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                选项
                            </td>
                           <td>
                                
                            </td>
                            <td colspan="14" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                营业收入
                            </td>
                            <td colspan="4" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                营业利润
                            </td>
                            <td colspan="2" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                利润总额
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="7" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                可供分配的利润
                            </td>
                            
                            <td colspan="5" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                可供投资者分配的利润
                            </td>
                            <td>
                                
                            </td>
                           
                            
                        </tr>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                删除/导出
                            </td>
                            <td>
                                日期编号
                            </td>
                            <td>
                                修改
                            </td>
                            <td>
                                查看
                            </td>
                            <td>
                                数据类型
                            </td>
                            <td>
                                一.营业收入
                            </td>
                            <td>
                                其中：主营业务收入
                            </td>
                            <td>
                                其他业务收入
                            </td>
                            <td>
                                减：营业成本
                            </td>
                            <td>
                                其中：主营业务成本
                            </td>
                            <td>
                                其他业务成本
                            </td>
                            <td>
                                营业税金及附加
                            </td>
                            <td>
                                销售费用
                            </td>
                            <td>
                                管理费用
                            </td>
                            <td>
                                财务费用
                            </td>
                            <td>
                                资产减值损失
                            </td>
                            <td>
                                加：公允价值变动收益（损失为负）
                            </td>
                            <td>
                                投资收益（损失为负）
                            </td>
                            <td>
                                其中：对联营企业和合营企业的投资收益
                            </td>
                            <td>
                                二.营业利润（损失为负）
                            </td>
                            <td>
                                加：营业外收入
                            </td>
                            <td>
                                减：营业外收入
                            </td>
                            <td>
                                其中：非流动资产处置损失
                            </td>
                            <td>
                                三.利润总额
                            </td>
                            <td>
                                减：所得税费用
                            </td>
                            <td>
                                四.净利润
                            </td>
                            <td>
                                加：年初未分配利润
                            </td>
                            <td>
                                其他转入
                            </td>
                            <td>
                                六.可供分配的利润
                            </td>
                            <td>
                                减：提取法定盈余公积
                            </td>
                            <td>
                                提取法定公益金
                            </td>
                            <td>
                                提取职工奖励及福利基金
                            </td>
                            <td>
                                提取储备基金
                            </td>
                            <td>
                                提取企业发展基金
                            </td>
                            <td>
                                利润归还投资
                            </td>
                            <td>
                                七.可供投资者分配的利润
                            </td>
                            <td>
                                减：应付优先股股利
                            </td>
                            <td>
                                提取任意盈余公积
                            </td>
                            <td>
                                应付普通股股利
                            </td>
                            <td>
                                转作资本（或股本）的普通股股利
                            </td>
                            <td>
                                八.未分配利润
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <asp:Label ID="lblID" runat="server" visible="false" Text='<%#Eval("ID")%>'></asp:Label>
                            <td>
                                 <asp:CheckBox ID="chkDel" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                 Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                 <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                             </td>
                             <td align="center">
                                <asp:Label ID="RQBH" runat="server" Enabled="false" Text='<%#Eval("RQBH")%>'></asp:Label>
                            </td>
                            <td><asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("RQBH").ToString()) %>'  runat="server" >
                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                修改</asp:HyperLink>
                            </td>
                            <td id="zcView" runat="server">
                                <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%# viewDq(Eval("RQBH").ToString()) %>'  runat="server" >
                                <asp:Image ID="Image3" ImageUrl="~/assets/images/search.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            </asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_TYPE" runat="server" Enabled="false" Text='<%#Eval("LRFP_TYPE")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="LRFP_YYSR" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_ZYSR" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_ZYSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_QTSR" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_QTSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_JYCB" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_JYCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_ZYCB" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_ZYCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_QTCB" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_QTCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_SJFJ" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_SJFJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_XSFY" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_XSFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_GLFY" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_GLFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_CWFY" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_CWFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_JZSS" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_JZSS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_JZBD" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_JZBD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_TZSY" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_TZSY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYSR_LYHY" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYSR_LYHY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYLR" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYLR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYLR_YWSR" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYLR_YWSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYLR_YWZC" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYLR_YWZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_YYLR_FLDSS" runat="server" Enabled="false" Text='<%#Eval("LRFP_YYLR_FLDSS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_LRZE" runat="server" Enabled="false" Text='<%#Eval("LRFP_LRZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_LRZE_SDSF" runat="server" Enabled="false" Text='<%#Eval("LRFP_LRZE_SDSF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_JLR" runat="server" Enabled="false" Text='<%#Eval("LRFP_JLR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_NCWFP" runat="server" Enabled="false" Text='<%#Eval("LRFP_NCWFP")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_QTZR" runat="server" Enabled="false" Text='<%#Eval("LRFP_QTZR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_FDYYGJ" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_FDYYGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_FDGY" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_FDGY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_JLFL" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_JLFL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_CBJJ" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_CBJJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_QYFZ" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_QYFZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGFP_LRGH" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGFP_LRGH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGTZFP" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGTZFP")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGTZFP_YFYXG" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGTZFP_YFYXG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGTZFP_RYYY" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGTZFP_RYYY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGTZFP_YFPTG" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGTZFP_YFPTG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_KGTZFP_ZZZB" runat="server" Enabled="false" Text='<%#Eval("LRFP_KGTZFP_ZZZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="LRFP_WFPLR" runat="server" Enabled="false" Text='<%#Eval("LRFP_WFPLR")%>'></asp:Label>
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
