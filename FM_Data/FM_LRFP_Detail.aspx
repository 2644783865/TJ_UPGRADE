<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_LRFP_Detail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_LRFP_Detail" %>

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
               <tr><td>利润分配信息<asp:Label ID="Label1" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)</td></tr>
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
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">本期数</font>
                    </td>
                    <td style="width:17%; height:25px">
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">本年累计数</font>
                    </td>
              </tr>
               <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">一.营业收入：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：主营业务收入：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_ZYSR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_ZYSR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他业务收入：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_QTSR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_QTSR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
               <tr>
                    <td>减：营业成本：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JYCB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JYCB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：主营业务成本：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_ZYCB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_ZYCB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他业务成本：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_QTCB1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_QTCB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>营业税金及附加：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_SJFJ1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_SJFJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>销售费用：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_XSFY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_XSFY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>管理费用：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_GLFY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_GLFY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>财务费用：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_CWFY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_CWFY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>资产减值损失：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JZSS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JZSS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>加：公允价值变动收益（损失为负）：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JZBD1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_JZBD2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>投资收益（损失为负）：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_TZSY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_TZSY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：对联营企业和合营企业的投资收益：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_LYHY1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYSR_LYHY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">二.营业利润（亏损为负)：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>加：营业外收入：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_YWSR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_YWSR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：营业外支出：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_YWZC1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_YWZC2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其中：非流动资产处置损失：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_FLDSS1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_YYLR_FLDSS2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">三.利润总额：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_LRZE1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_LRZE2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：所得税费用：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_LRZE_SDSF1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_LRZE_SDSF2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">四.净利润：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_JLR1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_JLR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>
              </tr>
              <tr>
                    <td>加：年初未分配利润：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_NCWFP1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_NCWFP2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>其他转入：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_QTZR1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_QTZR2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">六.可供分配的利润：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              
              <tr>
                    <td>减：提取法定盈余公积：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_FDYYGJ1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_FDYYGJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>提取法定公益金：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_FDGY1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_FDGY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>提取职工奖励及福利基金：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_JLFL1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_JLFL2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>提取储备基金：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_CBJJ1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_CBJJ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>提取企业发展基金：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_QYFZ1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_QYFZ2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>利润归还投资：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_LRGH1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGFP_LRGH2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">七.可供投资者分配的利润：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>减：应付优先股股利：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_YFYXG1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_YFYXG2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>提取任意盈余公积：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_RYYY1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_RYYY2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>应付普通股股利：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_YFPTG1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_YFPTG2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>转作资本（或股本）的普通股股利：
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_ZZZB1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_KGTZFP_ZZZB2" runat="server"></asp:TextBox>
                    </td>
              </tr>
              <tr>
                    <td>
                    <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">八.未分配利润：</font>
                    </td>
                    <td>
                        <asp:TextBox ID="LRFP_WFPLR1" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="left">
                        <asp:TextBox ID="LRFP_WFPLR2" runat="server"></asp:TextBox>
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
