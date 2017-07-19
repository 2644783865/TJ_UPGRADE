<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_XJLL.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_XJLL" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   现金流量表
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
                         <td align="right" style="width: 358px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_XJLL_Detail.aspx?FLAG=ADD','','dialogWidth=650px;dialogHeight=400px');" runat="server">
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
                            <td colspan="11" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                经营活动产生的现金流量
                            </td>
                            <td colspan="13" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                投资活动产生的现金流量
                            </td>
                            <td colspan="16" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                筹资活动产生的现金流量
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="2" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                现金及现金等价物净增加额
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="19" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                将净利润调节为经营活动现金流量
                            </td>
                            <td colspan="5" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                不涉及现金收支的投资和筹资活动
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                现金及现金等价物净增加情况
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
                                一.经营活动产生的现金流量:
                            </td>
                            <td>
                                销售商品、提供劳务收到的现金
                            </td>
                            <td>
                                收到的税费返还
                            </td>
                            <td>
                                收到的其他与经营活动有关的现金
                            </td>
                            <td>
                                现金流入小计
                            </td>
                            <td>
                                购买商品、接受劳务支付的现金
                            </td>
                            <td>
                                支付给职工以及为职工支付的现金
                            </td>
                            <td>
                                支付的各项税费
                            </td>
                            <td>
                                支付的其他与经营活动有关的现金
                            </td>
                            <td>
                                现金流出小计
                            </td>
                            <td>
                                经营活动产生的现金流量净额
                            </td>
                            <td>
                                二.投资活动产生的现金流量:
                            </td>
                            <td>
                                收回投资所收到的现金
                            </td>
                            <td>
                                取得投资收益所收到的现金
                            </td>
                            <td>
                                处置固定资产、无形资产和其他长期资产所收回的现金净额
                            </td>
                            <td>
                                处置子公司及其他营业单位收到的现金净额
                            </td>
                            <td>
                                收到的其他与投资活动有关的现金
                            </td>
                            <td>
                                现金流入小计
                            </td>
                            <td>
                                购建固定资产、无形资产和其他长期资产所支付的现金
                            </td>
                            <td>
                                投资所支付的现金
                            </td>
                            <td>
                                取得子公司及其他营业单位支付的现金净额
                            </td>
                            <td>
                                支付的其他与投资活动有关的现金
                            </td>
                            <td>
                                现金流出小计
                            </td>
                            <td>
                                投资活动产生的现金流量净额
                            </td>
                            <td>
                                三.筹资活动产生的现金流量:
                            </td>
                            <td>
                                吸收投资所收到的现金
                            </td>
                            <td>
                                其中：子公司吸收少数股东投资收到的现金*
                            </td>
                            <td>
                                借款所收到的现金
                            </td>
                            <td>
                                其中：中材国际结算中心借到的现金
                            </td>
                            <td>
                                中材装备集团内借到的现金
                            </td>
                            <td>
                                收到的其他与筹资活动有关的现金 
                            </td>
                            <td>
                                现金流入小计
                            </td>
                            <td>
                                偿还债务所支付的现金
                            </td>
                            <td>
                                其中：中材国际结算中心偿还的现金
                            </td>
                            <td>
                                中材装备集团内偿还的现金
                            </td>
                            <td>
                                分配股利、利润或偿付利息所支付的现金
                            </td>
                            <td>
                                其中：子公司支付给少数股东的股利、利润*
                            </td>
                            <td>
                                支付的其他与筹资活动有关的现金
                            </td>
                            <td>
                                现金流出小计
                            </td>
                            <td>
                                筹资活动产生的现金流量净额
                            </td>
                            <td>
                                四.汇率变动对现金的影响
                            </td>
                            <td>
                                五.现金及现金等价物净增加额
                            </td>
                            <td>
                                加：年初现金及现金等价物余额
                            </td>
                            <td>
                                六.年末现金及现金等价物余额
                            </td>
                            <td>
                                一.将净利润调节为经营活动现金流量：
                            </td>
                            <td>
                                净利润
                            </td>
                            <td>
                                加：*少数股东损益
                            </td>
                            <td>
                                减：*未确认的投资损失
                            </td>
                            <td>
                                加：计提的资产减值准备
                            </td>
                            <td>
                                固定资产折旧
                            </td>
                            <td>
                                无形资产摊销
                            </td>    
                            <td>
                                长期待摊费用摊销
                            </td>
                            <td>
                                处置固定资产、无形资产和其他长期资产的损失（减：收益）
                            </td>
                            <td>
                                固定资产报废损失
                            </td>
                            <td>
                                公允价值变动损失（收益为负值）
                            </td>
                            <td>
                                财务费用
                            </td>
                            <td>
                                投资损失（减：收益）
                            </td>
                            <td>
                                递延税款贷项（减：借项）
                            </td>
                            <td>
                                存货的减少（减：增加）
                            </td>
                            <td>
                                经营性应收项目的减少（减：增加）
                            </td>
                            <td>
                                经营性应付项目的增加（减：减少）
                            </td>
                            <td>
                                其他
                            </td>
                            <td>
                                经营活动产生的现金流量净额
                            </td>
                            <td>
                                二.不涉及现金收支的投资和筹资活动：
                            </td>
                            <td>
                                债务转为资本
                            </td>
                            <td>
                                一年内到期的可转换公司债券
                            </td>
                            <td>
                                融资租入固定资产
                            </td>
                            <td>
                                其他
                            </td>
                            <td>
                                三.现金及现金等价物净增加情况：
                            </td>
                            <td>
                                现金的期末余额
                            </td>
                             <td>
                                减：现金的期初余额
                            </td>
                            <td>
                                加：现金等价物的期末余额
                            </td>
                            <td>
                                减：现金等价物的期初余额
                            </td>
                            <td>
                                现金及现金等价物净增加额
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
                                <asp:Label ID="XJLL_TYPE" runat="server" Enabled="false" Text='<%#Eval("XJLL_TYPE")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="XJLL_JYHD" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_XSTG" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_XSTG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_FH" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_FH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_QT" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_LRXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_LRXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_GMJS" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_GMJS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_ZFZG" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_ZFZG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_ZFSF" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_ZFSF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_ZFQT" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_ZFQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_LCXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_LCXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_JYHD_JE" runat="server" Enabled="false" Text='<%#Eval("XJLL_JYHD_JE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_SHTZ" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_SHTZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_QDTZ" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_QDTZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_CZZCJE" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_CZZCJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_CZQT" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_CZQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_QT" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_LRXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_LRXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_GJZF" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_GJZF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_TZZF" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_TZZF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_QDJE" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_QDJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_ZFQT" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_ZFQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_LCXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_LCXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_TZHD_JE" runat="server" Enabled="false" Text='<%#Eval("XJLL_TZHD_JE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_XSTZ" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_XSTZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZGS" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZGS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_JKSD" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_JKSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZCGJJD" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZCGJJD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZCZBJD" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZCZBJD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_SDQT" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_SDQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_LRXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_LRXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_CHZW" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_CHZW")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZCGJCH" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZCGJCH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZCZBCH" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZCZBCH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_FPZF" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_FPZF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_QZZF" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_QZZF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_ZFQT" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_ZFQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_LCXJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_LCXJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_CZHD_JE" runat="server" Enabled="false" Text='<%#Eval("XJLL_CZHD_JE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_HLBD" runat="server" Enabled="false" Text='<%#Eval("XJLL_HLBD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_DJJZE" runat="server" Enabled="false" Text='<%#Eval("XJLL_DJJZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_DJJZE_JNCYE" runat="server" Enabled="false" Text='<%#Eval("XJLL_DJJZE_JNCYE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_NMYE" runat="server" Enabled="false" Text='<%#Eval("XJLL_NMYE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_JL" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_JL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_JSY" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_JSY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_JWQRSS" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_JWQRSS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_JJT" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_JJT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_GDZJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_GDZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_WXTX" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_WXTX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_CQDT" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_CQDT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_CZSS" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_CZSS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_GDBF" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_GDBF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_GYBD" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_GYBD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_CWFY" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_CWFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_TZSS" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_TZSS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_DYSD" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_DYSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_CHJS" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_CHJS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_YSJS" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_YSJS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_YFZJ" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_YFZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_QT" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JLTJJY_JE" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JLTJJY_JE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_BSTC" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_BSTC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_BSTC_ZZZB" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_BSTC_ZZZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_BSTC_YNKZZQ" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_BSTC_YNKZZQ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_BSTC_RZZR" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_BSTC_RZZR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_BSTC_QT" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_BSTC_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK")%>'></asp:Label>
                            </td> 
                           <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK_QMYE" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK_QMYE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK_JXQC" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK_JXQC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK_JDQM" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK_JDQM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK__JDQC" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK_JDQC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJLL_BC_JZQK_JZE" runat="server" Enabled="false" Text='<%#Eval("XJLL_BC_JZQK_JZE")%>'></asp:Label>
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
