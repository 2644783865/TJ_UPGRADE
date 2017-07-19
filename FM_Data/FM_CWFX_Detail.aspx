<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_CWFX_Detail.aspx.cs"
    Inherits="ZCZJ_DPF.FM_Data.FM_CWFX_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务分析结果</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <div class="RightContent" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small;
            font-weight: bold; font-style: normal; font-variant: normal; text-transform: none;
            color: #000000">
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td>
                                    财务分析结论<asp:Label ID="Label1" runat="server" Text=""></asp:Label>(带<span class="Error">*</span>号的为必填项)
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
           <div class="box-outer" align="center">
            <div style=" overflow:scroll">
                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                            日期编号：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="RQBH" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            &nbsp;<font color="#ff0000">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           销售净利率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XSJL" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           资产净利率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_ZCJL" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           销售毛利率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XSML" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">盈利能力分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_YLNLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                     </tr>
                     
                     <tr>
                        <td align="right" style="height:25px; width:20%">
                         全部资产现金回收率：  
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_QBHS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          盈利现金比率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_YLXJ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          销售收现比率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XSSX" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">盈利质量分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_YLZLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           净营运资本：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_JYY" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          流动比率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_LD" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          速动比率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_SD" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          现金比率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XJ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                         现金给付比率：  
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XJGF" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                         现金流量比率：  
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XJLL" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">偿债能力分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_CZNLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td align="right" style="height:25px; width:20%">
                           现金周转率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XJZZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                         应收账款周转率：  
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            周转次数：
                            <asp:TextBox ID="CWFX_YSZZ_CS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            周转天数：
                            <asp:TextBox ID="CWFX_YSZZ_TS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            与收入比：
                            <asp:TextBox ID="CWFX_YSZZ_BZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          存货周转率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            周转次数：
                            <asp:TextBox ID="CWFX_CHZZ_CS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            周转天数：
                            <asp:TextBox ID="CWFX_CHZZ_TS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            与收入比：
                            <asp:TextBox ID="CWFX_CHZZ_BZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          总资产周转率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            周转次数：
                            <asp:TextBox ID="CWFX_Z_CS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            周转天数：
                            <asp:TextBox ID="CWFX_Z_TS" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                            与收入比：
                            <asp:TextBox ID="CWFX_Z_BZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">营运能力分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_YYNLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           资产增长率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_ZCZZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          销售增长率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_XSZZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          净利润增长率： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_JLZZ" runat="server" Text="" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">发展能力分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_FZNLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           单位工资薪酬支出：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_GZZC" runat="server" Text="" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           三包费用成本率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_SBCB" runat="server" Text="" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           钢材利用率：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_GCLY" runat="server" Text="" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                           单位耗电量：
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_DWHD" runat="server" Text="" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height:25px; width:20%">
                          单位折旧额： 
                        </td>
                        <td align="left" style="height:25px; width:60%">
                            <asp:TextBox ID="CWFX_DWZJ" runat="server" Text="" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:20%">
                            <font color="Blue" 
                            style="font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000; font-family: 宋体, Arial, Helvetica, sans-serif">生产能力分析：</font>
                        </td>
                        <td align="left" style="width:60%">
                            <asp:TextBox ID="CWFX_SCNLFX" runat="server" TextMode="MultiLine" Height="100px" Width="370px"></asp:TextBox>
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
