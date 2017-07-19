<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_XJLL_Detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_XJLL_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    <div class="RightContent" 
            style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
       <div class="box-inner">
          <div class="box_right">
             <div class="box-title">
               <table width="100%" >             
               <tr><td>现金流量信息<asp:Label ID="Label1" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)</td></tr>
               </table>
            </div> 
         </div>
     </div>
     <div class="box-wrapper">
        <div class="box-outer" align="center">
        <div style=" overflow:scroll">
              <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
              <tr>
                     <td>日期编号：</td>
                    <td><asp:TextBox ID="RQBH" runat="server" Text="" Width="125px" 
                            Enabled="true"></asp:TextBox>
                    &nbsp;<font color="#ff0000">*</font>
                    </td>
                    <td>  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RQBH"  
                            ErrorMessage="日期编号不能为空！" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
              </tr>
              <tr>
                    <td style="width:16%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">项目</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">上月累计</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">本年累计</font>
                    </td>
              </tr>
               <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">一.经营活动产生的现金流量：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>销售商品、提供劳务收到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_XSTG1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_XSTG2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>收到的税费返还：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_FH1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_FH2" runat="server"></asp:TextBox>
                    </td>
              </tr>
               <tr>
                    <td>收到的其他与经营活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流入小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_LRXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_LRXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>购买商品、接受劳务支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_GMJS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_GMJS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>支付给职工以及为职工支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFZG1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFZG2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>支付的各项税费：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFSF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFSF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>支付的其他与经营活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_ZFQT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流出小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_LCXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_LCXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">经营活动产生的现金流量净额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_JE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_JYHD_JE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">二.投资活动产生的现金流量：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td class="right">收回投资所收到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_SHTZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_SHTZ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>取得投资收益所收到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QDTZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QDTZ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>处置固定资产、无形资产和其他长期资产所收回的现金净额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_CZZCJE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_CZZCJE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>处置子公司及其他营业单位收到的现金净额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_CZQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_CZQT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>收到的其他与投资活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流入小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_LRXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_LRXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>购建固定资产、无形资产和其他长期资产所支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_GJZF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_GJZF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>投资所支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_TZZF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_TZZF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>取得子公司及其他营业单位支付的现金净额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QDJE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_QDJE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>支付的其他与投资活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_ZFQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_ZFQT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流出小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_LCXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_LCXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">投资活动产生的现金流量净额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_JE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_TZHD_JE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">三、筹资活动产生的现金流量：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>吸收投资所收到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_XSTZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_XSTZ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：子公司吸收少数股东投资收到的现金 *：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZGS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZGS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>借款所收到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_JKSD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_JKSD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：中材国际结算中心借到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCGJJD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCGJJD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>中材装备集团内借到的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCZBJD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCZBJD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>收到的其他与筹资活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_SDQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_SDQT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流入小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_LRXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_LRXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>偿还债务所支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_CHZW1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_CHZW2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：中材国际结算中心偿还的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCGJCH1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCGJCH2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>中材装备集团内偿还的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCZBCH1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZCZBCH2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>分配股利、利润或偿付利息所支付的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_FPZF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_FPZF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：子公司支付给少数股东的股利、利润 *：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_QZZF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>支付的其他与筹资活动有关的现金：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_ZFQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_ZFQT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金流出小计：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_LCXJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_LCXJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">筹资活动产生的现金流量净额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_CZHD_JE1" runat="server"></asp:TextBox>
                    </td>
                    <td class="left">
                        <asp:TextBox ID="XJLL_CZHD_JE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">四、汇率变动对现金的影响：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_HLBD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_HLBD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">五、现金及现金等价物净增加额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_DJJZE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_DJJZE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">加：年初现金及现金等价物余额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_DJJZE_JNCYE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_DJJZE_JNCYE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">六、年末现金及现金等价物余额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_NMYE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_NMYE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">补充资料：</font>

                    </td>
                    <td>

                    </td>
                    <td >

                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">一、将净利润调节为经营活动现金流量：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>净利润：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JL1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JL2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>加：* 少数股东损益：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JSY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JSY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：* 未确认的投资损失：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JWQRSS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JWQRSS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>加：计提的资产减值准备：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JJT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JJT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>固定资产折旧：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GDZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GDZJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>无形资产摊销：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_WXTX1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_WXTX2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>长期待摊费用摊销：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CQDT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CQDT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>处置固定资产、无形资产和其他长期资产的损失（减：收益）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CZSS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CZSS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>固定资产报废损失：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GDBF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GDBF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>公允价值变动损失（收益以“－”号填列）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GYBD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_GYBD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>财务费用：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CWFY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CWFY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>投资损失（减:收益）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_TZSS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_TZSS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>递延税款贷项（减:借项）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_DYSD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_DYSD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>存货的减少（减:增加）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CHJS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_CHJS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>经营性应收项目的减少（减:增加）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_YSJS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_YSJS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>经营性应付项目的增加（减:减少）：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_YFZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_YFZJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">经营活动产生的现金流量净额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JLTJJY_JE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">二、不涉及现金收支的投资和筹资活动：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>债务转为资本：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_ZZZB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_ZZZB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>一年内到期的可转换公司债券：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_YNKZZQ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_YNKZZQ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>融资租入固定资产：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_RZZR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_RZZR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_BSTC_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">三、现金及现金等价物净增加情况：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>现金的期末余额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_QMYE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_QMYE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：现金的期初余额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JXQC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JXQC2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>加：现金等价物的期末余额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JDQM1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JDQM2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：现金等价物的期初余额：
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JDQC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JDQC2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">现金及现金等价物净增加额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JZE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="XJLL_BC_JZQK_JZE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
          </table>
          </div>       
            <div class="box_right" style="height:20px; padding-top:5px;" >
                    <asp:Button ID="btnConfirm" runat="server" Text="确定" 
                        onclick="btnConfirm_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small"
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" 
                            onclick="btnCancel_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small" />
            </div>
        </div>
    </div>  
 </div>
</asp:Panel>
    </form>
</body>
</html>
