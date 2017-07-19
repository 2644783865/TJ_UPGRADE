<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_JJZBTJ_Detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_JJZBTJ_Detail" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
经济指标信息<asp:Label ID="lbbtbz" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <asp:Panel ID="Panel1" runat="server">
     <div>
         <table width="100%"  style="text-align:center" border="1">
               <tr style="height:30px;">
                  <td colspan="6" align="left">
                  <strong>时间：</strong>
                        <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;*
                  </td>
               </tr>
               <tr style="height:30px;">
                  <td colspan="6">
                      <strong>利润指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                      <strong>净利润</strong>
                  </td>
                  <td>
                      计划指标
                  </td>
                  <td>
                     <asp:TextBox id="JLR_JH" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      已完成指标
                  </td>
                  <td>
                     <asp:TextBox id="JLR_YWC" runat="server"></asp:TextBox>
                  </td>
                  <td>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                  <strong>合同执行情况</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     
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
               </tr>
               <tr>
                  <td>
                     装备集团内
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZB_NEWZB" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZB_ZZX" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZB_NEWHTE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZB_QRSR" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZB_JZE" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr>
                  <td>
                     装备集团外
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZC_NEWZB" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZC_ZZX" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZC_NEWHTE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZC_QRSR" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZC_JZE" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr>
                  <td>
                     自营合同
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZY_NEWZB" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZY_ZZX" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZY_NEWHTE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZY_QRSR" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_ZY_JZE" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr>
                  <td>
                     在线堆焊合同
                  </td>
                  <td>
                     <asp:TextBox id="HT_DH_NEWZB" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_DH_ZZX" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_DH_NEWHTE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_DH_QRSR" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="HT_DH_JZE" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>营业额指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                      <strong>装备集团内</strong>
                  </td>
                  <td>
                      全年计划指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZB_QN" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      累计完成指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZB_LJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                  </td>
               </tr>
               <tr>
                  <td>
                      <strong>装备集团外</strong>
                  </td>
                  <td>
                      全年计划指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZC_QN" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      累计完成指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZC_LJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                  </td>
               </tr>
               <tr>
                  <td>
                      <strong>自营合同</strong>
                  </td>
                  <td>
                      全年计划指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZY_QN" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      累计完成指标
                  </td>
                  <td>
                     <asp:TextBox id="YYE_ZY_LJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>现金流量指标(本年累计数)</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     经营活动收到的现金
                  </td>
                  <td>
                     <asp:TextBox id="XJL_JYSD_BNLJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     其中：销售商品收到的现金
                  </td>
                  <td>
                     <asp:TextBox id="XJL_QZ_BNLJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     经营活动支付的现金
                  </td>
                  <td>
                     <asp:TextBox id="XJL_JYZF_BNLJ" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr>
                  <td>
                     经营活动现金净流量
                  </td>
                  <td>
                     <asp:TextBox id="XJL_JYJLL_BNLJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     货币资金期末数
                  </td>
                  <td>
                     <asp:TextBox id="XJL_HBQM_BNLJ" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     货币资金期初数
                  </td>
                  <td>
                     <asp:TextBox id="XJL_HBQC_BNLJ" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>应收账款指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     
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
                     
                  </td>
                  <td>
                     
                  </td>
               </tr>
               <tr>
                  <td>
                     装备集团内
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBN_NCYS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBN_BYHS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBN_LJHS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     
                  </td>
                  <td>
                     
                  </td>
               </tr>
               <tr>
                  <td>
                     装备集团外
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBW_NCYS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBW_BYHS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="YSZK_ZBW_LJHS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     
                  </td>
                  <td>
                     
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>人员指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                      人员入职率
                  </td>
                  <td>
                      人员离职率
                  </td>
                  <td>
                      培训率
                  </td>
                  <td>
                      工资总额
                  </td>
                  <td>
                      
                  </td>
                  <td>
                      
                  </td>
               </tr>
               <tr>
                  <td>
                      <asp:TextBox id="RYZB_RZL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      <asp:TextBox id="RYZB_LZL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      <asp:TextBox id="RYZB_PXL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      <asp:TextBox id="RYZB_GZZE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                      
                  </td>
                  <td>
                      
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>生产指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     已完成产量
                  </td>
                  <td>
                     全年计划产量
                  </td>
                  <td>
                     在制品剩余产量
                  </td>
                  <td>
                     下月计划产量
                  </td>
                  <td>
                     工时完成率
                  </td>
                  <td>
                     余料利用率
                  </td>
               </tr>
               <tr>
                  <td>
                     <asp:TextBox id="SCZB_YWC" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="SCZB_QNJH" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="SCZB_ZZPSY" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="SCZB_XYJH" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="SCZB_GSWCL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="SCZB_YLLYL" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>费用指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     水费
                  </td>
                  <td>
                     电费
                  </td>
                  <td>
                     办公费
                  </td>
                  <td>
                     业务招待费
                  </td>
                  <td>
                     交通费
                  </td>
                  <td>
                     其他
                  </td>
               </tr>
               <tr>
                  <td>
                     <asp:TextBox id="FYZB_SF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="FYZB_DF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="FYZB_BGF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="FYZB_YWZDF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="FYZB_JTF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="FYZB_QT" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>安全指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     安全事故次数
                  </td>
                  <td>
                     设备报检次数
                  </td>
                  <td>
                     保险次数
                  </td>
                  <td>
                     保险赔付金额
                  </td>
                  <td>
                     设备维修费
                  </td>
                  <td>
                     
                  </td>
               </tr>
               <tr>
                  <td>
                     <asp:TextBox id="AQ_SGCS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="AQ_SBBJCS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="AQ_BXCS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="AQ_BXPCJE" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="AQ_SBWXF" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     
                  </td>
               </tr>
               <tr style="height:30px">
                  <td colspan="6">
                     <strong>质量指标</strong>
                  </td>
               </tr>
               <tr>
                  <td>
                     一次送检合格率
                  </td>
                  <td>
                     机加工综合废品率
                  </td>
                  <td>
                     质量赔偿金额
                  </td>
                  <td>
                     一次交检合格率
                  </td>
                  <td>
                     不合格成品数量
                  </td>
                  <td>
                     采购不合格数
                  </td>
               </tr>
               <tr>
                  <td>
                     <asp:TextBox id="ZL_YCSJHGL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="ZL_JJZHFPL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="ZL_ZLPC" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="ZL_YCJJHGL" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="ZL_BHGCPS" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     <asp:TextBox id="ZL_CGBHGS" runat="server"></asp:TextBox>
                  </td>
               </tr>
               <tr>
                  <td>
                     外协不合格数
                  </td>
                  <td>
                     <asp:TextBox id="ZL_WXBHGS" runat="server"></asp:TextBox>
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
               <tr>
                  <td>
                     <strong>备注</strong>
                  </td>
                  <td colspan="5">
                     <asp:TextBox id="JJZB_NOTE" style="width:80%; " runat="server"></asp:TextBox>
                  </td>
               </tr>
         </table>
     </div>
  </asp:Panel>
     <div style="height:30px; padding-top:5px;" align="center">
                    <asp:Button ID="btnConfirm" runat="server" Text="确定" 
                        onclick="btnConfirm_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small"
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" 
                            onclick="btnCancel_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small" />
    </div>
</asp:Content>
