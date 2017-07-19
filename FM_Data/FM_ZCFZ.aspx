<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_ZCFZ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ZCFZ" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    资产负债表 
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
                         <td align="right" style="width: 358px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_ZCFZ_Detail.aspx?FLAG=ADD','','dialogWidth=650px;dialogHeight=400px');" runat="server">
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
                                流动资产
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="19" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                非流动资产
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="13" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                流动负债
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="8" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                非流动负债
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="7" style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                所有者权益
                            </td>
                            <td>
                                
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
                                货币资金
                            </td>
                            <td>
                                其中结算中心存款
                            </td>
                            <td>
                                交易性金融资产
                            </td>
                            <td>
                                应收票据
                            </td>
                            <td>
                                应收账款原值
                            </td>
                            <td>
                                减坏账准备
                            </td>
                            <td>
                                应收账款净值
                            </td>
                            <td>
                                预付款项
                            </td>
                            <td>
                                应收利息
                            </td>
                            <td>
                                应收股利
                            </td>
                            <td>
                                其他应收款
                            </td>
                            <td>
                                存货
                            </td>
                            <td>
                                一年内到期的非流动资产
                            </td>
                            <td>
                                其他流动资产
                            </td>
                            <td>
                                流动资产合计
                            </td>
                            <td>
                                可供出售金融资产
                            </td>
                            <td>
                                持有至到期投资
                            </td>
                            <td>
                                长期应收款
                            </td>
                            <td>
                                长期股权投资
                            </td>
                            <td>
                                投资性房地产
                            </td>
                            <td>
                                固定资产原值
                            </td>
                            <td>
                                减累计折旧
                            </td>
                            <td>
                                固定资产净值
                            </td>
                            <td>
                                减固定资产减值准备
                            </td>
                            <td>
                                固定资产净额
                            </td>
                            <td>
                                在建工程
                            </td>
                            <td>
                                工程物资
                            </td>
                            <td>
                                固定资产清理
                            </td>
                            <td>
                                无形资产
                            </td>
                            <td>
                                开发支出
                            </td>
                            <td>
                                商誉 
                            </td>
                            <td>
                                长期待摊费用
                            </td>
                            <td>
                                递延所得税资产
                            </td>
                            <td>
                                其他非流动资产
                            </td>
                            <td>
                                非流动资产合计
                            </td>
                            <td>
                                资产总计
                            </td>
                            <td>
                                短期借款
                            </td>
                            <td>
                                其中结算中心贷款
                            </td>
                            <td>
                                交易性金融负债
                            </td>
                            <td>
                                应付票据
                            </td>
                            <td>
                                应付账款
                            </td>
                            <td>
                                预收款项
                            </td>
                            <td>
                                应付职工薪酬
                            </td>
                            <td>
                                应交税费
                            </td>
                            <td>
                                应付利息
                            </td>
                            <td>
                                应付股利
                            </td>
                            <td>
                                其他应付款
                            </td>
                            <td>
                                一年内到期的非流动负债
                            </td>
                            <td>
                                其他流动负债
                            </td>
                            <td>
                                流动负债合计
                            </td>
                            <td>
                                长期借款
                            </td>    
                            <td>
                                其中结算中心贷款
                            </td>
                            <td>
                                应付债券
                            </td>
                            <td>
                                长期应付款
                            </td>
                            <td>
                                专项应付款
                            </td>
                            <td>
                                预计负债
                            </td>
                            <td>
                                递延所得税负债
                            </td>
                            <td>
                                其他非流动负债
                            </td>
                            <td>
                                非流动负债合计
                            </td>
                            <td>
                                负债合计
                            </td>
                            <td>
                                实收资本
                            </td>
                            <td>
                                减已归还投资
                            </td>
                            <td>
                                资本公积
                            </td>
                            <td>
                                库存股
                            </td>
                            <td>
                                盈余公积
                            </td>
                            <td>
                                专项储备
                            </td>
                            <td>
                                未分配利润
                            </td>
                            <td>
                                所有者权益合计
                            </td>
                            <td>
                                负债及所有者权益总计
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
                                <asp:Label ID="ZCFZ_TYPE" runat="server" Enabled="false" Text='<%#Eval("ZCFZ_TYPE")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="ZC_LD_HBZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HBZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_HBZJ_JS" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HBZJ_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_JYJR" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_JYJR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSPJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSPJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSZKYZ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSZKYZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_JH" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_JH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSZKJZ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSZKJZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YFKX" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YFKX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSLX" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSLX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSGL" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_QTYS" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_QTYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_CH" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_CH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YNFLD" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YNFLD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_QT" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_HJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_KJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_KJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CDT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CDT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQYS" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQGQT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQGQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_TF" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_TF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZY" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_JL" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_JL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZJZ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZJZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_JG" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_JG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZJE" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_ZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_ZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GCWZ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GCWZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZQL" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZQL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_WXZC" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_WXZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_KFZC" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_KFZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_SY" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_SY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQDT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQDT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_DYSD" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_DYSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_QT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_HJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_ZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_ZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_DQJK" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_DQJK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_DQJK_JS" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_DQJK_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_JYJR" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_JYJR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFPJ" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFPJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFZK" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFZK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YSKX" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YSKX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFXC" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFXC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YJSF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YJSF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFLX" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFLX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFGL" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_QTYF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_QTYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YNDF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YNDF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_QT" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQJK" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQJK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQJK_JS" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQJK_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_YFZJ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_YFZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQYF" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_ZXYF" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_ZXYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_YJFZ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_YJFZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_DYSD" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_DYSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_QT" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_SSZB" runat="server" Enabled="false" Text='<%#Eval("QY_SSZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_JY" runat="server" Enabled="false" Text='<%#Eval("QY_JY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_ZBGJ" runat="server" Enabled="false" Text='<%#Eval("QY_ZBGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_JK" runat="server" Enabled="false" Text='<%#Eval("QY_JK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_YYGJ" runat="server" Enabled="false" Text='<%#Eval("QY_YYGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_ZXCB" runat="server" Enabled="false" Text='<%#Eval("QY_ZXCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_WFP" runat="server" Enabled="false" Text='<%#Eval("QY_WFP")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_HJ" runat="server" Enabled="false" Text='<%#Eval("QY_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZQY_ZJ" runat="server" Enabled="false" Text='<%#Eval("FZQY_ZJ")%>'></asp:Label>
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
