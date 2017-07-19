<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FM_ZCFZ_Detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ZCFZ_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                   <tr><td>资产负债信息<asp:Label ID="Laddmessage" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)</td></tr>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RQBH"  
                            ErrorMessage="日期编号不能为空！" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
              </tr>
              <tr>
                    <td style="width:16%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">资产</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">年初数</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">期末数</font>
                    </td>
                    <td style="width:16%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">负债及所有者权益</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">年初数</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">期末数</font>
                    </td>
              </tr>
              <tr>
                    <td style="width:16%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">流动资产：</font>
                    </td>
                    <td style="width:17%; height:25px">
                    
                    </td>
                    <td style="width:17%; height:25px">
                    
                    </td>
                    <td style="width:16%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">流动负债：</font>
                    </td>
                    <td style="width:17%; height:25px">

                    </td>
                    <td style="width:17%; height:25px">

                    </td>
              </tr>
               <tr>
                    <td>货币资金：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HBZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HBZJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>短期借款：
                 
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_DQJK1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_DQJK2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中结算中心存款：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HBZJ_JS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HBZJ_JS2" runat="server"></asp:TextBox>
                    </td>
                    <td>其中结算中心贷款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_DQJK_JS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_DQJK_JS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>交易性金融资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_JYJR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_JYJR2" runat="server"></asp:TextBox>
                    </td>
                    <td>交易性金融负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_JYJR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_JYJR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
               <tr>
                    <td>应收票据：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSPJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSPJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付票据：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFPJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFPJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>应收账款原值：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSZKYZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSZKYZ2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付账款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFZK1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFZK2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减坏账准备：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_JH1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_JH2" runat="server"></asp:TextBox>
                    </td>
                    <td>预收款项：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YSKX1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YSKX2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>应收账款净值：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSZKJZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSZKJZ2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付职工薪酬：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFXC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFXC2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>预付款项：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YFKX1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YFKX2" runat="server"></asp:TextBox>
                    </td>
                    <td>应交税费：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YJSF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YJSF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>应收利息：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSLX1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSLX2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付利息：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFLX1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFLX2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>应收股利：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSGL1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YSGL2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付股利 
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFGL1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YFGL2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他应收款：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_QTYS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_QTYS2" runat="server"></asp:TextBox>
                    </td>
                    <td>其他应付款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_QTYF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_QTYF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>存货：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_CH1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_CH2" runat="server"></asp:TextBox>
                    </td>
                    <td>一年内到期的非流动负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YNDF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_YNDF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>一年内到期的非流动资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YNFLD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_YNFLD2" runat="server"></asp:TextBox>
                    </td>
                    <td>其他流动负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他流动资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_QT2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    
                    </td>
                    <td>

                    </td>
                    <td>
          
                    </td>
              </tr>
              <tr>
                    <td>流动资产合计：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_LD_HJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>流动负债合计：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_LD_HJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td 
                        style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">非流动资产：
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
                    <td
                        style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">非流动负债：
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
              </tr>
              <tr>
                    <td>可供出售金融资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_KJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_KJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>长期借款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQJK1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQJK2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>持有至到期投资：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CDT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CDT2" runat="server"></asp:TextBox>
                    </td>
                    <td>其中结算中心贷款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQJK_JS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQJK_JS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>长期应收款：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQYS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQYS2" runat="server"></asp:TextBox>
                    </td>
                    <td>应付债券：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_YFZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_YFZJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>长期股权投资：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQGQT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQGQT2" runat="server"></asp:TextBox>
                    </td>
                    <td>长期应付款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQYF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_CQYF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>投资性房地产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_TF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_TF2" runat="server"></asp:TextBox>
                    </td>
                    <td>专项应付款：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_ZXYF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_ZXYF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>固定资产原值：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZY2" runat="server"></asp:TextBox>
                    </td>
                    <td>预计负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_YJFZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_YJFZ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减累计折旧：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_JL1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_JL2" runat="server"></asp:TextBox>
                    </td>
                    <td>递延所得税负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_DYSD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_DYSD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>固定资产净值：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZJZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZJZ2" runat="server"></asp:TextBox>
                    </td>
                    <td>其他非流动负债：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_QT2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减固定资产减值准备：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_JG1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_JG2" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
              </tr>
              <tr>
                    <td>固定资产净额：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZJE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZJE2" runat="server"></asp:TextBox>
                    </td>
                    <td>非流动负债合计：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_FLD_HJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>在建工程：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_ZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_ZJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>负债合计：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZ_HJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>工程物资：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GCWZ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GCWZ2" runat="server"></asp:TextBox>
                    </td>
                    <td style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">所有者权益：
                    
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
              </tr>
              <tr>
                    <td>固定资产清理：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZQL1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_GZQL2" runat="server"></asp:TextBox>
                    </td>
                    <td>实收资本：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_SSZB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_SSZB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>无形资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_WXZC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_WXZC2" runat="server"></asp:TextBox>
                    </td>
                    <td>减已归还投资：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_JY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_JY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>开发支出：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_KFZC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_KFZC2" runat="server"></asp:TextBox>
                    </td>
                    <td>资本公积：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_ZBGJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_ZBGJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>商誉：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_SY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_SY2" runat="server"></asp:TextBox>
                    </td>
                    <td>减库存股
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_JK1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_JK2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>长期待摊费用：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQDT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_CQDT2" runat="server"></asp:TextBox>
                    </td>
                    <td>盈余公积：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_YYGJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_YYGJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>递延所得税资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_DYSD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_DYSD2" runat="server"></asp:TextBox>
                    </td>
                    <td>专项储备：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_ZXCB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_ZXCB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他非流动资产：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_QT1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_QT2" runat="server"></asp:TextBox>
                    </td>
                    <td>未分配利润：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_WFP1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_WFP2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>非流动资产合计：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_FLD_HJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>所有者权益合计：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="QY_HJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="QY_HJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>资产总计：
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_ZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ZC_ZJ2" runat="server"></asp:TextBox>
                    </td>
                    <td>负债及所有者权益总计：
                    
                    </td>
                    <td>
                        <asp:TextBox ID="FZQY_ZJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FZQY_ZJ2" runat="server"></asp:TextBox>
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
<%--                    <asp:Label ID="message" Visible="false" runat="server" ForeColor="Red"></asp:Label>--%>
            </div>
        </div>
    </div>  
 </div>
</asp:Panel>
    </form>
</body>
</html>
