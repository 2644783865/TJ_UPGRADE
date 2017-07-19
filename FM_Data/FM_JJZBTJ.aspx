<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_JJZBTJ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_JJZBTJ" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   经济指标统计
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
                        <td style="width: 75%;">
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
                         <td align="right" style="width: 100px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_JJZBTJ_Detail.aspx?action=add','','dialogWidth=800px;dialogHeight=600px');" runat="server">
                          <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         </td>
                         <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？');" onclick="btnSC_Click"  />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                            <td colspan="2" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                利润指标
                            </td>
                            <td colspan="20" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                合同执行情况
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                营业额指标
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                现金流指标
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                应收账款指标
                            </td>
                            <td colspan="4" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                人员指标
                            </td>
                            <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                生产指标
                            </td>
                           <td colspan="6" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                费用指标
                            </td>
                            <td colspan="5" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                安全指标
                            </td>
                            <td colspan="7" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                质量指标
                            </td>
                            <td colspan="1" rowspan="3" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                备注
                            </td>
                            
                        </tr>
                        <tr align="center" class="tableTitle headcolor">
                            <td rowspan="2">
                                序号
                            </td>
                            <td rowspan="2">
                                日期
                            </td>
                            <td rowspan="2">
                                修改
                            </td>
                            <td rowspan="2">
                                查看
                            </td>
                            <td colspan="2">
                                净利润
                            </td>
                            <td colspan="5">
                                装备集团内
                            </td>
                            <td colspan="5">
                                装备集团外
                            </td>
                            <td colspan="5">
                                自营合同
                            </td>
                            <td colspan="5">
                                在线堆焊合同
                            </td>
                            <td colspan="2">
                                装备集团内
                            </td>
                            <td colspan="2">
                                装备集团外
                            </td>
                            <td colspan="2">
                                自营合同
                            </td>
                            <td rowspan="2">
                                经营活动收到的现金
                            </td>
                            <td rowspan="2">
                                其中：销售商品收到的现金
                            </td>
                            <td rowspan="2">
                                经营活动支付的现金
                            </td>
                            <td rowspan="2">
                                经营活动现金净流量
                            </td>
                            <td rowspan="2">
                                货币资金期末数
                            </td>
                            <td rowspan="2">
                                货币资金期初数
                            </td>
                            <td colspan="3">
                                装备集团内
                            </td>
                            <td colspan="3">
                                装备集团外
                            </td>
                            <td rowspan="2">
                                人员入职率
                            </td>
                            <td rowspan="2">
                                人员离职率
                            </td>
                            <td rowspan="2">
                                培训率
                            </td>
                            <td rowspan="2">
                                工资总额
                            </td>
                            <td rowspan="2">
                                已完成产量
                            </td>
                            <td rowspan="2">
                                全年计划产量
                            </td>
                            <td rowspan="2">
                                在制品剩余产量
                            </td>
                            <td rowspan="2">
                                下月计划产量
                            </td>
                            <td rowspan="2">
                                工时完成率
                            </td>
                            <td rowspan="2">
                                余料利用率
                            </td>
                            <td rowspan="2">
                                水费
                            </td>
                            <td rowspan="2">
                                电费
                            </td>
                            <td rowspan="2">
                                办公费
                            </td>
                            <td rowspan="2">
                                业务招待费
                            </td>
                            <td rowspan="2">
                                交通费
                            </td>
                            <td rowspan="2">
                                 其他
                            </td>
                            <td rowspan="2">
                                安全事故次数
                            </td>
                            <td rowspan="2">
                                设备报检次数
                            </td>
                            <td rowspan="2">
                                保险次数
                            </td>
                            <td rowspan="2">
                                保险赔付金额
                            </td>
                            <td rowspan="2">
                                设备维修费
                            </td>
                            <td rowspan="2">
                                一次送检合格率
                            </td>
                            <td rowspan="2">
                                机加工综合废品率
                            </td>
                            <td rowspan="2">
                                质量赔偿金额
                            </td>
                            <td rowspan="2">
                                一次交检合格率
                            </td>
                            <td rowspan="2">
                                不合格成品数量
                            </td>
                            <td rowspan="2">
                                采购不合格数
                            </td>
                            <td rowspan="2">
                                外协不合格数
                            </td>
                         </tr>
                         <tr align="center" class="tableTitle headcolor">
                            <td>
                                计划指标
                            </td>
                            <td>
                                已完成指标
                            </td>
                            <td>
                                新签指标
                            </td>
                            <td>
                                在执行合同
                            </td>
                            <td>
                                新签合同额
                            </td>
                            <td>
                                确认收入合同
                            </td>
                            <td>
                                结转合同额
                            </td>
                            <td>
                                新签指标
                            </td>
                            <td>
                                在执行合同
                            </td>
                            <td>
                                新签合同额
                            </td>
                            <td>
                                确认收入合同
                            </td>
                            <td>
                                结转合同额
                            </td>   
                            <td>
                                新签指标
                            </td>
                            <td>
                                在执行合同
                            </td>
                            <td>
                                新签合同额
                            </td>
                            <td>
                                确认收入合同
                            </td>
                            <td>
                                结转合同额
                            </td>
                            <td>
                                新签指标
                            </td>
                            <td>
                                在执行合同
                            </td>
                            <td>
                                新签合同额
                            </td>
                            <td>
                                确认收入合同
                            </td>
                            <td>
                                结转合同额
                            </td>
                            <td>
                                全年计划指标
                            </td>
                            <td>
                                累计完成指标
                            </td>
                            <td>
                                全年计划指标
                            </td>
                            <td>
                                累计完成指标
                            </td>
                            <td>
                                全年计划指标
                            </td>
                            <td>
                                累计完成指标
                            </td>
                            <td>
                               年初应收余额 
                            </td>    
                            <td>
                               本月回收额 
                            </td>
                            <td>
                               累计回收额 
                            </td>
                            <td>
                               年初应收余额 
                            </td>    
                            <td>
                               本月回收额 
                            </td>
                            <td>
                               累计回收额 
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
                            <td><asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("ID").ToString()) %>'  runat="server" >
                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                修改</asp:HyperLink>
                            </td>
                            <td id="zcView" runat="server">
                                <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%# viewDq(Eval("ID").ToString()) %>'  runat="server" >
                                <asp:Image ID="Image3" ImageUrl="~/assets/images/search.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            </asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:Label ID="JLR_JH" runat="server" Enabled="false" Text='<%#Eval("JLR_JH")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="JLR_YWC" runat="server" Enabled="false" Text='<%#Eval("JLR_YWC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZB_NEWZB" runat="server" Enabled="false" Text='<%#Eval("HT_ZB_NEWZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZB_ZZX" runat="server" Enabled="false" Text='<%#Eval("HT_ZB_ZZX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZB_NEWHTE" runat="server" Enabled="false" Text='<%#Eval("HT_ZB_NEWHTE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZB_QRSR" runat="server" Enabled="false" Text='<%#Eval("HT_ZB_QRSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZB_JZE" runat="server" Enabled="false" Text='<%#Eval("HT_ZB_JZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZC_NEWZB" runat="server" Enabled="false" Text='<%#Eval("HT_ZC_NEWZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZC_ZZX" runat="server" Enabled="false" Text='<%#Eval("HT_ZC_ZZX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZC_NEWHTE" runat="server" Enabled="false" Text='<%#Eval("HT_ZC_NEWHTE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZC_QRSR" runat="server" Enabled="false" Text='<%#Eval("HT_ZC_QRSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZC_JZE" runat="server" Enabled="false" Text='<%#Eval("HT_ZC_JZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZY_NEWZB" runat="server" Enabled="false" Text='<%#Eval("HT_ZY_NEWZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZY_ZZX" runat="server" Enabled="false" Text='<%#Eval("HT_ZY_ZZX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZY_NEWHTE" runat="server" Enabled="false" Text='<%#Eval("HT_ZY_NEWHTE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZY_QRSR" runat="server" Enabled="false" Text='<%#Eval("HT_ZY_QRSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_ZY_JZE" runat="server" Enabled="false" Text='<%#Eval("HT_ZY_JZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_DH_NEWZB" runat="server" Enabled="false" Text='<%#Eval("HT_DH_NEWZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_DH_ZZX" runat="server" Enabled="false" Text='<%#Eval("HT_DH_ZZX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_DH_NEWHTE" runat="server" Enabled="false" Text='<%#Eval("HT_DH_NEWHTE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_DH_QRSR" runat="server" Enabled="false" Text='<%#Eval("HT_DH_QRSR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="HT_DH_JZE" runat="server" Enabled="false" Text='<%#Eval("HT_DH_JZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZB_QN" runat="server" Enabled="false" Text='<%#Eval("YYE_ZB_QN")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZB_LJ" runat="server" Enabled="false" Text='<%#Eval("YYE_ZB_LJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZC_QN" runat="server" Enabled="false" Text='<%#Eval("YYE_ZC_QN")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZC_LJ" runat="server" Enabled="false" Text='<%#Eval("YYE_ZC_LJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZY_QN" runat="server" Enabled="false" Text='<%#Eval("YYE_ZY_QN")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YYE_ZY_LJ" runat="server" Enabled="false" Text='<%#Eval("YYE_ZY_LJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_JYSD_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_JYSD_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_QZ_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_QZ_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_JYZF_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_JYZF_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_JYJLL_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_JYJLL_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_HBQM_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_HBQM_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="XJL_HBQC_BNLJ" runat="server" Enabled="false" Text='<%#Eval("XJL_HBQC_BNLJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBN_NCYS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBN_NCYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBN_BYHS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBN_BYHS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBN_LJHS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBN_LJHS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBW_NCYS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBW_NCYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBW_BYHS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBW_BYHS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="YSZK_ZBW_LJHS" runat="server" Enabled="false" Text='<%#Eval("YSZK_ZBW_LJHS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="RYZB_RZL" runat="server" Enabled="false" Text='<%#Eval("RYZB_RZL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="RYZB_LZL" runat="server" Enabled="false" Text='<%#Eval("RYZB_LZL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="RYZB_PXL" runat="server" Enabled="false" Text='<%#Eval("RYZB_PXL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="RYZB_GZZE" runat="server" Enabled="false" Text='<%#Eval("RYZB_GZZE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_YWC" runat="server" Enabled="false" Text='<%#Eval("SCZB_YWC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_QNJH" runat="server" Enabled="false" Text='<%#Eval("SCZB_QNJH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_ZZPSY" runat="server" Enabled="false" Text='<%#Eval("SCZB_ZZPSY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_XYJH" runat="server" Enabled="false" Text='<%#Eval("SCZB_XYJH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_GSWCL" runat="server" Enabled="false" Text='<%#Eval("SCZB_GSWCL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="SCZB_YLLYL" runat="server" Enabled="false" Text='<%#Eval("SCZB_YLLYL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_SF" runat="server" Enabled="false" Text='<%#Eval("FYZB_SF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_DF" runat="server" Enabled="false" Text='<%#Eval("FYZB_DF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_BGF" runat="server" Enabled="false" Text='<%#Eval("FYZB_BGF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_YWZDF" runat="server" Enabled="false" Text='<%#Eval("FYZB_YWZDF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_JTF" runat="server" Enabled="false" Text='<%#Eval("FYZB_JTF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FYZB_QT" runat="server" Enabled="false" Text='<%#Eval("FYZB_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="AQ_SGCS" runat="server" Enabled="false" Text='<%#Eval("AQ_SGCS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="AQ_SBBJCS" runat="server" Enabled="false" Text='<%#Eval("AQ_SBBJCS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="AQ_BXCS" runat="server" Enabled="false" Text='<%#Eval("AQ_BXCS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="AQ_BXPCJE" runat="server" Enabled="false" Text='<%#Eval("AQ_BXPCJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="AQ_SBWXF" runat="server" Enabled="false" Text='<%#Eval("AQ_SBWXF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_YCSJHGL" runat="server" Enabled="false" Text='<%#Eval("ZL_YCSJHGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_JJZHFPL" runat="server" Enabled="false" Text='<%#Eval("ZL_JJZHFPL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_ZLPC" runat="server" Enabled="false" Text='<%#Eval("ZL_ZLPC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_YCJJHGL" runat="server" Enabled="false" Text='<%#Eval("ZL_YCJJHGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_BHGCPS" runat="server" Enabled="false" Text='<%#Eval("ZL_BHGCPS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_CGBHGS" runat="server" Enabled="false" Text='<%#Eval("ZL_CGBHGS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZL_WXBHGS" runat="server" Enabled="false" Text='<%#Eval("ZL_WXBHGS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="JJZB_NOTE" runat="server" Enabled="false" Text='<%#Eval("JJZB_NOTE")%>' ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;text-align: center" ToolTip='<%#Eval("JJZB_NOTE")%>'></asp:TextBox>
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
