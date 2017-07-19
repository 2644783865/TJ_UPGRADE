<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CWFX.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CWFX" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
财务分析指标
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <div class="box-wrapper">
      <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
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
                             <asp:Button ID="btnupdate" runat="server" Text="数据同步" 
                                 onclick="btnupdate_Click" />
                         </td>
                         <td>
                            <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_Click" />
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
                            <td colspan="3" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                盈利能力分析
                            </td>
                            <td colspan="3" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                盈利质量分析
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                偿债能力分析
                            </td>
                            <td colspan="10" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                营运能力分析
                            </td>
                            <td colspan="3" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                发展能力分析
                            </td>
                            <td colspan="5" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                生产能力分析
                            </td> 
                        </tr>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                序号
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
                                销售净利率
                            </td>
                            <td>
                                资产净利率
                            </td>
                            <td>
                                销售毛利率
                            </td>
                            <td>
                                全部资产现金回收率
                            </td>
                            <td>
                                盈利现金比率
                            </td>
                            <td>
                                销售收现比率
                            </td>
                            <td>
                                净营运资本
                            </td>
                            <td>
                                流动比率
                            </td>
                            <td>
                                速动比率
                            </td>
                            <td>
                                现金比率
                            </td>
                            <td>
                                现金给付比率
                            </td>
                            <td>
                                现金流量比率
                            </td>
                            <td>
                                现金周转率
                            </td>
                            <td colspan="3">
                                应收账款周转率
                            </td>
                            <td colspan="3">
                                存货周转率
                            </td>
                            <td colspan="3">
                                总资产周转率
                            </td>
                            <td>
                                资产增长率
                            </td>
                            <td>
                                销售增长率
                            </td>
                            <td>
                                净利润增长率
                            </td>
                            <td>
                                单位工资薪酬支出
                            </td>
                            <td>
                                三包费用成本率
                            </td>
                            <td>
                                钢材利用率
                            </td>
                            <td>
                                单位耗电量
                            </td>
                            <td>
                                单位折旧额
                            </td>         
                        </tr>
                        <tr align="center" class="tableTitle headcolor">
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
                              应收账款周转次数
                            </td>
                            <td>
                              应收账款周转天数
                            </td>
                            <td>
                              应收账款与收入比
                            </td>
                            <td>
                              存货周转次数
                            </td>
                            <td>
                              存货周转天数
                            </td>
                            <td>
                              存货与收入比
                            </td>
                            <td>
                              总资产周转次数
                            </td>
                            <td>
                              总资产周转天数
                            </td>
                            <td>
                              总资产与收入比
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
                                <asp:Label ID="CWFX_XSJL" runat="server" Enabled="false" Text='<%#Eval("CWFX_XSJL")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="CWFX_ZCJL" runat="server" Enabled="false" Text='<%#Eval("CWFX_ZCJL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XSML" runat="server" Enabled="false" Text='<%#Eval("CWFX_XSML")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_QBHS" runat="server" Enabled="false" Text='<%#Eval("CWFX_QBHS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_YLXJ" runat="server" Enabled="false" Text='<%#Eval("CWFX_YLXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XSSX" runat="server" Enabled="false" Text='<%#Eval("CWFX_XSSX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_JYY" runat="server" Enabled="false" Text='<%#Eval("CWFX_JYY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_LD" runat="server" Enabled="false" Text='<%#Eval("CWFX_LD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_SD" runat="server" Enabled="false" Text='<%#Eval("CWFX_SD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XJ" runat="server" Enabled="false" Text='<%#Eval("CWFX_XJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XJGF" runat="server" Enabled="false" Text='<%#Eval("CWFX_XJGF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XJLL" runat="server" Enabled="false" Text='<%#Eval("CWFX_XJLL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XJZZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_XJZZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_YSZZ_CS" runat="server" Enabled="false" Text='<%#Eval("CWFX_YSZZ_CS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_YSZZ_TS" runat="server" Enabled="false" Text='<%#Eval("CWFX_YSZZ_TS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_YSZZ_BZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_YSZZ_BZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_CHZZ_CS" runat="server" Enabled="false" Text='<%#Eval("CWFX_CHZZ_CS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_CHZZ_TS" runat="server" Enabled="false" Text='<%#Eval("CWFX_CHZZ_TS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_CHZZ_BZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_CHZZ_BZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_Z_CS" runat="server" Enabled="false" Text='<%#Eval("CWFX_Z_CS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_Z_TS" runat="server" Enabled="false" Text='<%#Eval("CWFX_Z_TS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_Z_BZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_Z_BZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_ZCZZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_ZCZZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_XSZZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_XSZZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_JLZZ" runat="server" Enabled="false" Text='<%#Eval("CWFX_JLZZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_GZZC" runat="server" Enabled="false" Text='<%#Eval("CWFX_GZZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_SBCB" runat="server" Enabled="false" Text='<%#Eval("CWFX_SBCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_GCLY" runat="server" Enabled="false" Text='<%#Eval("CWFX_GCLY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_DWHD" runat="server" Enabled="false" Text='<%#Eval("CWFX_DWHD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CWFX_DWZJ" runat="server" Enabled="false" Text='<%#Eval("CWFX_DWZJ")%>'></asp:Label>
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
